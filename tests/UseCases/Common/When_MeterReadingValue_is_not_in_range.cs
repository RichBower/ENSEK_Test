using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Common;

public sealed class MeterReadingValueRangeTests
{
    [Theory]
    [InlineData("-1")]
    [InlineData("100000")]
    public void When_MeterReadingValue_is_not_in_range(string input)
    {
        var act = () => new MeterReadValue(input);

        var expectedException = FeedException.MeterReadingValueIsNotInRange(int.Parse(input), MeterReadValue.MinValue, MeterReadValue.MaxValue);

        act.Should()
            .Throw<FeedException>()
            .WithMessage(expectedException.Message);
    }
}
   
