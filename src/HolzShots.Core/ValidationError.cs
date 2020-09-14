using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace HolzShots
{
    public class ValidationError
    {
        public string Message { get; }
        public IReadOnlyList<string> AffectedProperties { get; }
        public Exception /* ? */ Exception { get; }

        public ValidationError(string message, string affectedProperty, Exception exception = null)
            : this(message, ImmutableList.Create(affectedProperty), exception) { }

        public ValidationError(string message, IReadOnlyList<string> affectedProperties, Exception exception = null)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            AffectedProperties = affectedProperties ?? ImmutableList<string>.Empty;
            Exception = exception;
        }

        public override string ToString() => $"{Message}\nAffected Properties:\n{string.Join("\n", AffectedProperties.Select(ap => " - " + ap))}";
    }
}
