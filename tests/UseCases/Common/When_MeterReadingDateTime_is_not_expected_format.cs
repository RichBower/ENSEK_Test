using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Common
{
    public sealed class MeterReadingDateTimeIncorrectFormatTests
    {
        [Theory]
        [InlineData("3/july/1975 12:10")]
        [InlineData("03071975 12:10")]
        [InlineData("03/07/1975")]
        [InlineData("03/1975 12:10")]
        [InlineData("03/07/1975 12")]
        [InlineData("03/07/1975 12.10")]
        public void When_MeterReadingDateTime_is_not_expected_format(string input)
        {
            var act = () => new MeterReadingDateTime(input);

            var expectedException = FeedException.MeterReadingDateTimeFormatIsInvalid(input, MeterReadingDateTime.DateTimeFormat);

            act.Should()
                .Throw<FeedException>()
                .WithMessage(expectedException.Message);
        }
    }
}
