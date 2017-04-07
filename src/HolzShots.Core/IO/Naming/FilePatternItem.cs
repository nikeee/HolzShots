using System.Diagnostics;

namespace HolzShots.IO.Naming
{
    class FilePatternItem : PatternItem
    {
        public FilePatternItem(string propertyName)
            : base(propertyName)
        {
            if (PropertyName == null)
            {
                InfoType = FileInfoType.Invalid;
                return;
            }
            switch (PropertyName.ToLowerInvariant())
            {
                case "seqnum":
                case "sequencenumber":
                case "seq":
                    InfoType = FileInfoType.SequenceNumber;
                    break;
                default:
                    InfoType = FileInfoType.Invalid;
                    break;
            }
        }

        public override string Keyword => "File";
        public override string TextRepresentation => $"<{Keyword}:{PropertyName}>";
        public FileInfoType InfoType { get; }
        public override bool IsValid => InfoType == FileInfoType.SequenceNumber;

        // Assuming the parameter is always the sequence number
        // ...because that is the only valid parameter.
        // This may change some day.
        public override string FormatMetadata(FileMetadata metadata)
        {
            Debug.Assert(InfoType == FileInfoType.SequenceNumber);
            return metadata.SequenceNumber.ToString();
        }

        public enum FileInfoType
        {
            Invalid,
            SequenceNumber,
        }
    }
}
