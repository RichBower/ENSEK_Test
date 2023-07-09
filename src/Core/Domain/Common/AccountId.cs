﻿using interview.test.ensek.Core.Domain.Feed;

namespace interview.test.ensek.Core.Domain.Common;

public sealed class AccountId : ValueObject
{
    public AccountId(string? value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        {
            throw FeedException.AccountIdCannotBeNull();
        }

        Value = value;
    }

    public string Value { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
