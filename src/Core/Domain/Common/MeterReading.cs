namespace interview.test.ensek.Core.Domain.Common;

public sealed class MeterReading : ValueObject
{
    public AccountId AccountId { get; init; }

    public MeterReadingDateTime MeterReadingDateTime { get; init; }

    public MeterReadValue MeterReadValue { get; init; }

    public MeterReading(AccountId accountId, MeterReadingDateTime meterReadingDateTime, MeterReadValue meterReadValue)
    {
        AccountId = accountId;
        MeterReadingDateTime = meterReadingDateTime;
        MeterReadValue = meterReadValue;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return AccountId;
        yield return MeterReadingDateTime;
        yield return MeterReadValue;
    }
}
