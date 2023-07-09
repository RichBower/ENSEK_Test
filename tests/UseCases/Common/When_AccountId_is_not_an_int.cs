using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Common
{
    public sealed class AccountIdValueFormatTests
    {

        [Theory]
        [InlineData("1x")]
        [InlineData("x1")]
        [InlineData("1234x")]
        [InlineData("1x23x4")]
        public void When_AccountId_is_not_an_int(string input)
        {
            var act = () => new AccountId(input);

            var expectedException = FeedException.AccountIdIsNotAnInteger(input);

            act.Should()
                .Throw<FeedException>()
                .WithMessage(expectedException.Message);
        }
    }
}
