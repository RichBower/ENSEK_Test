using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Common
{
    public sealed class MeterReadingValueFormatTests
    {
        [Theory]
        [InlineData("1x")]
        [InlineData("x1")]
        [InlineData("1234x")]
        [InlineData("1x23x4")]
        public void When_MeterReadingValue_is_not_an_int(string input)
        {
            var act = () => new MeterReadValue(input);

            var expectedException = FeedException.MeterReadingIsNotnExpectedFormat(input);

            act.Should()
                .Throw<FeedException>()
                .WithMessage(expectedException.Message);
        }
    }
}