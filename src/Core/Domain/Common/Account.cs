namespace interview.test.ensek.Core.Domain.Common;

public sealed class Account : ValueObject
{
    public AccountId AccountId { get; init; }

    public FirstName FirstName { get; init; }

    public LastName LastName { get; init; }

    public Account(AccountId accountId, FirstName firstName, LastName lastName)
    {
        AccountId = accountId;
        FirstName = firstName;
        LastName = lastName;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return AccountId;
        yield return FirstName;
        yield return LastName;
    }
}
