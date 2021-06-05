using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HolzShots.Net.Custom
{
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
                    case '$':
                        ++i;

                        var nodeKind = ExpressionSyntaxNode.ReadExpressionNodeKind(value, ref i);
                        switch (nodeKind)
                        {
                            case RegExSyntaxNode.NodeKind:
                                nodes.Add(RegExSyntaxNode.Parse(value, ref i));
                                break;
                                // case JsonPathSyntaxNode.NodeKind:
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
        protected const char ExpressionBoundary = '$';

        public abstract string Evaluate(ResponseParser responseParser, string content);
    }

    abstract class ExpressionSyntaxNode : TemplateSyntaxNode
    {
        protected const char ExpressionParameterBoundary = ':';

        public static string ReadExpressionNodeKind(ReadOnlySpan<char> value, ref int index)
        {
            var start = index;
            var currentChar = value[index];
            while (index < value.Length && currentChar != ExpressionBoundary && currentChar != ExpressionParameterBoundary)
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
            while (index < value.Length && currentChar != ExpressionBoundary)
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
            if (PatternIndex < 0 || PatternIndex >= responseParser.ParsedRegexPatterns.Count)
                throw new UnableToFillTemplateException($"Reference to non-existent entry in regex pattern list: {PatternIndex}");

            var pattern = responseParser.ParsedRegexPatterns[PatternIndex];

            var matches = pattern.Matches(content);
            if (matches.Count > 0)
            {
                if (MatchIndex != null && MatchIndex >= matches.Count)
                    throw new UnableToFillTemplateException($"Index of match ({MatchIndex}) exceeds the number of matches ({matches.Count})");

                var matchIndex = MatchIndex.HasValue ? (int)MatchIndex : 0;
                var match = matches[matchIndex];

                // If the match does not contain GroupName, no exception is thrown. Instead, we get an "empty" group.
                // This is ok for us.
                return GroupName == null
                    ? match.Value
                    : match.Groups[GroupName].Value;
            }
            throw new UnableToFillTemplateException("The pattern did not match");
        }
    }

    class JsonSyntaxNode : ExpressionSyntaxNode
    {
        public const string NodeKind = "regex";

        string JsonPath { get; }
        private JsonSyntaxNode(string jsonPath)
        {
            JsonPath = jsonPath;
        }

        public static JsonSyntaxNode Parse(ReadOnlySpan<char> value, ref int index)
        {
            // Consume ':'
            Debug.Assert(value[index] == ExpressionParameterBoundary);
            ++index;
            Debug.Assert(index < value.Length);

            var start = index;
            var currentChar = value[index];
            while (index < value.Length && currentChar != ExpressionBoundary)
                currentChar = value[++index];

            var jsonPath = value[start..index].ToString();
            Debug.Assert(jsonPath != null && jsonPath.Length > 0);

            ++index; // Consume $

            return new JsonSyntaxNode(jsonPath);
        }

        public override string Evaluate(ResponseParser responseParser, string content)
        {
            JObject contentJson;
            try
            {
                contentJson = JObject.Parse(content);
            }
            catch (JsonReaderException ex)
            {
                throw new UnableToFillTemplateException("Invalid JSON response", ex);
            }

            Debug.Assert(contentJson != null);
            // Get Success Link-Value

            var result = contentJson.SelectToken(JsonPath);
            if (result == null)
                throw new UnableToFillTemplateException($"Could not select JSON path: {JsonPath}");
            return result.ToString();
        }
    }

    class TextSyntaxNode : TemplateSyntaxNode
    {
        string Text { get; }
        private TextSyntaxNode(string text) => Text = text;

        public static TextSyntaxNode Parse(ReadOnlySpan<char> value, ref int index)
        {
            var start = index;
            var currentChar = value[index];
            while (index < value.Length && currentChar != ExpressionBoundary)
            {
                ++index;
                currentChar = value[index];
            }
            return new TextSyntaxNode(value[start..index].ToString());
        }

        public override string Evaluate(ResponseParser responseParser, string content) => Text;
    }

    public class UnableToFillTemplateException : Exception
    {
        public UnableToFillTemplateException(string message) : base(message) { }
        public UnableToFillTemplateException(string message, Exception innerException) : base(message, innerException) { }
    }
}
