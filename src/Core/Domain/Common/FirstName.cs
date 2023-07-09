using interview.test.ensek.Core.Domain.Feed;

namespace interview.test.ensek.Core.Domain.Common;

public sealed class FirstName : ValueObject
{
    public FirstName(string? value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        {
            throw FeedException.FirstNameCannotBeNull();
        }

        Value = value;
    }

    public string Value { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}