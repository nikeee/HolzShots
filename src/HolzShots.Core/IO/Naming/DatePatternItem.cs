using System;

namespace HolzShots.IO.Naming
{
    class DatePatternItem : PatternItem
    {
        public DatePatternItem(string propertyName)
            : base(propertyName)
        { }


        public override string Keyword => "Date";
        public override string TextRepresentation => $"<{Keyword}:{PropertyName}>";
        public override bool IsValid
        {
            get
            {
                // Dem solution
                try
                {
                    DateTime.Now.ToString(PropertyName);
                }
                catch (FormatException)
                {
                    return false;
                }
                return true;
            }
        }

        // PropertyName should be a valid time string. This is made sure by the IsValid property.
        public override string FormatMetadata(FileMetadata metadata) => metadata.Timestamp.ToString(PropertyName);
    }
}
