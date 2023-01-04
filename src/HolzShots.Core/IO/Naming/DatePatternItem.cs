using System.Globalization;

namespace HolzShots.IO.Naming;

class DatePatternItem : PatternItem
{
    public DatePatternItem(string? propertyName) : base(propertyName) { }

    public override string Keyword => "date";
    public override string TextRepresentation => string.IsNullOrWhiteSpace(PropertyName) ? $"<{Keyword}:ISO>" : $"<{Keyword}:{PropertyName}>";
    public override bool IsValid => string.IsNullOrWhiteSpace(PropertyName)
                                    || PropertyName.ToUpperInvariant() == "ISO"
                                    || IsValidDateFormat(PropertyName);

    public override string FormatMetadata(FileMetadata metadata)
    {
        var invalidFileName = FormatMetadataInternal(metadata);
        return invalidFileName.Replace(':', '-'); // TODO: Evaluate if there can be more invalid path chars in a date
    }

    // PropertyName should be a valid time string. This is made sure by the IsValid property.
    // It can also be empty or ISO. In this case, we default to sortable date times:
    // https://docs.microsoft.com/en-us/dotnet/api/system.globalization.datetimeformatinfo.sortabledatetimepattern
    private string FormatMetadataInternal(FileMetadata metadata)
    {
        if (string.IsNullOrWhiteSpace(PropertyName) || PropertyName.ToUpperInvariant() == "ISO")
        {
            // SortableDateTimePattern is the same for all cultures. We can use the invariant culture.
            return metadata.Timestamp.ToString(DateTimeFormatInfo.InvariantInfo.SortableDateTimePattern);
        }
        return metadata.Timestamp.ToString(PropertyName);
    }

    private static bool IsValidDateFormat(string candidate)
    {
        // Dem solution
        try
        {
            DateTime.Now.ToString(candidate);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
