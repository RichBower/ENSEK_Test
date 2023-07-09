using System.Globalization;
using interview.test.ensek.Core.Domain.Feed;

namespace interview.test.ensek.Core.Domain.Common;

public sealed class MeterReadingDateTime : ValueObject
{
    public const string DateTimeFormat = "dd/MM/yyyy HH:mm";

    public MeterReadingDateTime(string? value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        {
            throw FeedException.MeterReadingDateTimeCannotBeNull();
        }

        if (false == DateTime.TryParseExact(value, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            throw FeedException.MeterReadingDateTimeFormatIsInvalid(value, DateTimeFormat);
        }

        Value = date;
    }

    public DateTime Value { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}