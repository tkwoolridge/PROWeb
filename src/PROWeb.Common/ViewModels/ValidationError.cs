#nullable enable

namespace PROWeb.Common.ViewModels
{
    /// <summary>
    /// The severity of a validation error.
    /// </summary>
    public enum ValidationSeverity
    {
        Warning,
        Error
    }

    /// <summary>
    /// A validation error uniquely identifies an error that can happen during data validation.
    /// </summary>
    public readonly struct ValidationError : IEquatable<ValidationError>
    {
        public static readonly ValidationError None = new ValidationError("");

        public ValidationError(string message, ValidationSeverity severity = ValidationSeverity.Error)
        {
            Message = message;
            Severity = severity;
        }

        public string Message { get; }

        public ValidationSeverity Severity { get; }

        public override string ToString()
        {
            return Message;
        }

        public bool Equals(ValidationError other)
        {
            return Message.Equals(other.Message);
        }

        public override bool Equals(object? obj)
        {
            return obj is ValidationError other && Equals(other);
        }

        public static bool operator ==(in ValidationError a, in ValidationError b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(in ValidationError a, in ValidationError b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Message.GetHashCode();
        }
    }
}