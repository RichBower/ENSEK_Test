using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Core.Domain.Loader
{
    public sealed class BatchError
    {
        public string Message { get; init; }

        public BatchError(string message) 
        {
            Message = message;
        }

        public static BatchError AccountIdDoesNotExist(AccountId accountId) => new BatchError($"AccountId {accountId.Value} does not exist");

        public static BatchError MeterReadingAlreadyExists(AccountId accountId, MeterReadingDateTime meterReadingDateTime) => new BatchError($"Meter Reading with AccountId {accountId.Value} and Date {meterReadingDateTime.Value} already exists");

        public static BatchError MeterReadingIsOld(AccountId accountId, MeterReadingDateTime meterReadingDateTime) => new BatchError($"Meter Reading with AccountId {accountId.Value} and Date {meterReadingDateTime.Value} is old");
    }
}
