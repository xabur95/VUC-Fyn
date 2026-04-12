using System;

namespace Semesterprojekt1PBA.Domain.Helpers
{
    /// <summary>
    /// A domain-level exception type that carries an error code and an optional user-facing message.
    /// Use this type when you want to differentiate domain errors from other exceptions and
    /// provide structured information for error handling, logging and presentation.
    /// </summary>
    public class ErrorException : Exception
    {
        /// <summary>
        /// A short machine-readable error code (e.g. "SCHOOL_NOT_FOUND").
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Optional message that is safe to show to end users.
        /// </summary>
        public string? UserMessage { get; }

        /// <summary>
        /// The time the exception instance was created (UTC).
        /// </summary>
        public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow;

        public ErrorException(string message, string errorCode = "DOMAIN_ERROR", string? userMessage = null)
            : base(message)
        {
            ErrorCode = errorCode ?? "DOMAIN_ERROR";
            UserMessage = userMessage;
        }

        public ErrorException(string message, Exception innerException, string errorCode = "DOMAIN_ERROR", string? userMessage = null)
            : base(message, innerException)
        {
            ErrorCode = errorCode ?? "DOMAIN_ERROR";
            UserMessage = userMessage;
        }

        // Serialization is intentionally omitted: prefer explicit mapping to DTOs or
        // modern serializers (e.g. System.Text.Json) for transferring error data.

        public override string ToString()
        {
            return $"{base.ToString()}\nErrorCode: {ErrorCode}\nUserMessage: {UserMessage}\nOccurredAt: {OccurredAt:O}";
        }

        /// <summary>
        /// Wrap an arbitrary exception in an <see cref="ErrorException"/>, preserving an existing
        /// ErrorException if already present.
        /// </summary>
        public static ErrorException Wrap(Exception ex, string errorCode = "DOMAIN_ERROR", string? userMessage = null)
        {
            if (ex is ErrorException ee)
                return ee;

            return new ErrorException(ex.Message, ex, errorCode, userMessage);
        }
    }
}
