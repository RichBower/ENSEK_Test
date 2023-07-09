using interview.test.ensek.Core.Domain.Feed;

namespace interview.test.ensek.Core.Domain.Common;

public sealed class LastName : ValueObject
{
    public LastName(string? value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        {
            throw FeedException.LastNameCannotBeNull();
        }
    }

    public string Value { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}



