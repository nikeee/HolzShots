using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace HolzShots.Net.Custom;

// TODO: Proper escaping mechanism for tokens. We should probably rewrite this parser
public class UrlTemplateSpec
{
    private readonly IReadOnlyList<TemplateSyntaxNode> _nodes;
    private readonly ResponseParser _responseParser;
    private UrlTemplateSpec(ResponseParser responseParser, IReadOnlyList<TemplateSyntaxNode> nodes)
    {
        _responseParser = responseParser;
        _nodes = nodes;
    }

    public static UrlTemplateSpec? Parse(ResponseParser responseParser, ReadOnlySpan<char> value)
    {
        if (value == null)
            return null;

        var nodes = ImmutableList.CreateBuilder<TemplateSyntaxNode>();

        var i = 0;
        while (i < value.Length)
        {
            var currentChar = value[i];
            switch (currentChar)
            {
                case '<':
                    ++i;

                    var nodeKind = ExpressionSyntaxNode.ReadExpressionNodeKind(value, ref i);
                    switch (nodeKind)
                    {
                        case RegExSyntaxNode.NodeKind:
                            nodes.Add(RegExSyntaxNode.Parse(value, ref i));
                            break;
                        case JsonSyntaxNode.NodeKind:
                            nodes.Add(JsonSyntaxNode.Parse(value, ref i));
                            break;
                    }
                    break;
                default:
                    nodes.Add(TextSyntaxNode.Parse(value, ref i));
                    break;
            }
        }
        return new UrlTemplateSpec(responseParser, nodes.ToImmutable());
    }

    public string Evaluate(string content)
    {
        var sb = new StringBuilder();
        foreach (var n in _nodes)
            sb.Append(n.Evaluate(_responseParser, content));
        return sb.ToString();
    }
}

abstract class TemplateSyntaxNode
{
    // '<' and '>' are not allowed in URLs, giving us the opportinity to use them as tokens for easier parsing
    protected const char ExpressionStartBoundary = '<';
    protected const char ExpressionEndBoundary = '>';

    public abstract string Evaluate(ResponseParser responseParser, string content);
}

abstract class ExpressionSyntaxNode : TemplateSyntaxNode
{
    // TODO: Support escaping
    protected const char ExpressionParameterBoundary = ':';

    public static string ReadExpressionNodeKind(ReadOnlySpan<char> value, ref int index)
    {
        var start = index;
        var currentChar = value[index];
        while (index < value.Length && currentChar != ExpressionStartBoundary && currentChar != ExpressionParameterBoundary)
            currentChar = value[++index];

        return value[start..index].ToString().ToLowerInvariant();
    }
}

class RegExSyntaxNode : ExpressionSyntaxNode
{
    public const string NodeKind = "regex";

    int PatternIndex { get; }
    string? GroupName { get; }
    int? MatchIndex { get; }
    private RegExSyntaxNode(int patternIndex, string? groupName, int? matchIndex)
    {
        PatternIndex = patternIndex;
        GroupName = groupName;
        MatchIndex = matchIndex;
    }

    public static RegExSyntaxNode Parse(ReadOnlySpan<char> value, ref int index)
    {
        // Consume ':'
        Debug.Assert(value[index] == ExpressionParameterBoundary);
        ++index;
        Debug.Assert(index < value.Length);

        var start = index;
        var currentChar = value[index];
        while (index < value.Length && currentChar != ExpressionEndBoundary)
            currentChar = value[++index];

        var contents = value[start..index].ToString();

        var parameters = contents.Split(':');

        var patternIndex = 0;
        string? groupName = null;
        int? matchIndex = null;
        if (parameters.Length >= 1)
            patternIndex = int.Parse(parameters[0]);
        if (parameters.Length >= 2)
            groupName = parameters[1];
        if (parameters.Length >= 3)
            matchIndex = int.Parse(parameters[2]);

        ++index; // Consume $

        return new RegExSyntaxNode(patternIndex, groupName, matchIndex);
    }

    public override string Evaluate(ResponseParser responseParser, string content)
    {
        var parsedPatterns = responseParser.ParsedRegexPatterns ?? throw new UnableToFillTemplateException(content, "No pattern to match.");

        if (PatternIndex < 0 || PatternIndex >= parsedPatterns.Count)
            throw new UnableToFillTemplateException(content, $"Reference to non-existent entry in regex pattern list: {PatternIndex}.");

        var pattern = parsedPatterns[PatternIndex];

        var matches = pattern.Matches(content);
        if (matches.Count > 0)
        {
            if (MatchIndex is not null && MatchIndex >= matches.Count)
                throw new UnableToFillTemplateException(content, $"Index of match ({MatchIndex}) exceeds the number of matches ({matches.Count}).");

            var matchIndex = MatchIndex ?? 0;
            var match = matches[matchIndex];

            // If the match does not contain GroupName, no exception is thrown. Instead, we get an "empty" group.
            // This is ok for us.
            return GroupName == null
                ? match.Value
                : match.Groups[GroupName].Value;
        }
        throw new UnableToFillTemplateException(content, "The pattern did not match.");
    }
}

class JsonSyntaxNode : ExpressionSyntaxNode
{
    public const string NodeKind = "jsonpath";

    string JsonPath { get; }
    private JsonSyntaxNode(string jsonPath) => JsonPath = jsonPath;

    public static JsonSyntaxNode Parse(ReadOnlySpan<char> value, ref int index)
    {
        // Consume ':'
        Debug.Assert(value[index] == ExpressionParameterBoundary);
        ++index;
        Debug.Assert(index < value.Length);

        var start = index;
        var currentChar = value[index];
        while (index < value.Length && currentChar != ExpressionEndBoundary)
            currentChar = value[++index];

        var jsonPath = value[start..index].ToString();
        Debug.Assert(jsonPath is not null && jsonPath.Length > 0);

        ++index; // Consume >

        return new JsonSyntaxNode(jsonPath);
    }

    public override string Evaluate(ResponseParser responseParser, string content)
    {
        JsonDocument contentJson;
        try
        {
            contentJson = JsonDocument.Parse(content);
        }
        catch (JsonException ex)
        {
            throw new UnableToFillTemplateException(content, "Invalid JSON response", ex);
        }

        Debug.Assert(contentJson is not null);
        // Get Success Link-Value

        // System.Text.Json doesn't have a built-in JSONPath implementation like Newtonsoft.Json's SelectToken
        // For simple paths like "data.url", we need to manually traverse
        var result = SelectJsonElement(contentJson.RootElement, JsonPath);
        return result.HasValue
            ? result.Value.ToString()
            : throw new UnableToFillTemplateException(content, $"Could not select JSON path: {JsonPath}");
    }

    private static JsonElement? SelectJsonElement(JsonElement root, string path)
    {
        var current = root;
        var parts = path.Split('.');

        foreach (var part in parts)
        {
            if (current.ValueKind != JsonValueKind.Object)
                return null;

            if (!current.TryGetProperty(part, out var next))
                return null;

            current = next;
        }

        return current;
    }
}

class TextSyntaxNode : TemplateSyntaxNode
{
    string Text { get; }
    private TextSyntaxNode(string text) => Text = text;

    public static TextSyntaxNode Parse(ReadOnlySpan<char> value, ref int index)
    {
        var start = index;
        for(; index < value.Length; ++index)
        {
            var currentChar = value[index];
            if (currentChar == ExpressionStartBoundary)
                break;
        }

        return new TextSyntaxNode(value[start..index].ToString());
    }

    public override string Evaluate(ResponseParser responseParser, string content) => Text;
}

public class UnableToFillTemplateException(string serverResponse, string message, Exception? innerException) : Exception(message, innerException)
{
    public string ServerResponse { get; } = serverResponse;
    public UnableToFillTemplateException(string serverResponse, string message) : this(serverResponse, message, null) { }
}
