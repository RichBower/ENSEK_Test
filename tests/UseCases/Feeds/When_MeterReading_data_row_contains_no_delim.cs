

using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Feeds;
public sealed class MeterReadingRowContainsNoDelimeterTests : MeterReadingsFeedBase
{
    public MeterReadingRowContainsNoDelimeterTests()
        : base()
    {
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void When_data_row_contains_no_delim(string input, List<ProcessedRecord<MeterReading>> expected)
    {
        Runner(input, expected);
    }

    public static IEnumerable<object[]> Data()
    {
        yield return new object[]
              {
            @"AccountId,MeterReadingDateTime,MeterReadValue,
               234422/04/2019 09:24
               2233,22/04/2019 12:25;323

            ",
            new List<ProcessedRecord<MeterReading>> {
                ProcessedRecord<MeterReading>.WithFailure(2, FeedException.InsufficientFields()),
                                ProcessedRecord<MeterReading>.WithFailure(3, FeedException.InsufficientFields()),

            }
              };
        yield return new object[]
        {
            @"AccountId,MeterReading,DateTime,MeterReadValue,
               234422/04/2019 09:24


              t




               2233;22/04/2019 12:25;323",
            new List<ProcessedRecord<MeterReading>> {
               ProcessedRecord<MeterReading>.WithFailure(2, FeedException.InsufficientFields()),
               ProcessedRecord<MeterReading>.WithFailure(5, FeedException.InsufficientFields()),
                ProcessedRecord<MeterReading>.WithFailure(10, FeedException.InsufficientFields()),
            }
        };
    }
}
