using interview.test.ensek.Core.Domain.Feed;

namespace interview.test.ensek.Core.Domain.Common;

public sealed class MeterReadValue : ValueObject
{
    public const int MinValue = 0;
    public const int MaxValue = 99999;

    public MeterReadValue(string? value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        {
            throw FeedException.MeterReadingValueCannotBeNull();
        }

        if (false == int.TryParse(value, out var number))
        {
            throw FeedException.MeterReadingIsNotnExpectedFormat(value);
        }

        if (number < MinValue || number > MaxValue)
        {
            throw FeedException.MeterReadingValueIsNotInRange(number, MinValue, MaxValue);
        }

        Value = number;
    }

    public int Value { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}