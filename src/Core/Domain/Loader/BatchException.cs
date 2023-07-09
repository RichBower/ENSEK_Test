using System.Runtime.Serialization;
using interview.test.ensek.Core.Domain.Common;
using interview.test.ensek.Core.Domain.Feed;

namespace interview.test.ensek.Core.Domain.Loader
{
    public sealed class BatchException : Exception
    {
        public BatchException()
        {
        }

        public BatchException(string? message) : base(message)
        {
        }

        public BatchException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static BatchException AccountIdDoesNotExist(AccountId accountId) => new BatchException($"AccountId {accountId.Value} does not exist");

        public static BatchException MeterReadingAlreadyExists(AccountId accountId, MeterReadingDateTime meterReadingDateTime) => new BatchException($"Meter Reading with AccountId {accountId.Value} and Date {meterReadingDateTime.Value} already exists");

        public static BatchException MeterReadingIsOld(AccountId accountId, MeterReadingDateTime meterReadingDateTime) => new BatchException($"Meter Reading with AccountId {accountId.Value} and Date {meterReadingDateTime.Value} is old");
    }
}
