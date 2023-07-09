
using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Feeds;

public sealed class MeterReadingContainsWhitespaceTests : MeterReadingsFeedBase
{

    public MeterReadingContainsWhitespaceTests() : base()
    {
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void When_column_in_data_row_contains_whitespace(string input, List<ProcessedRecord<MeterReading>> expected)
    {
        Runner(input, expected);
    }

    public static IEnumerable<object[]> Data()
    {
        yield return new object[]
       {
            @"
               AccountId,MeterReadingDateTime,MeterReadValue,
               2344,22/04/2019 09:24, ,
            ",
             new List<ProcessedRecord<MeterReading>> {
                ProcessedRecord<MeterReading>.WithFailure(3, FeedException.MeterReadingValueCannotBeNull())
            }
       };
        //yield return new object[]
        //{
        //    @"
        //       AccountId,MeterReadingDateTime,MeterReadValue,
        //       2344,22/04/2019 09:24,     ,


        //       ,22/04/2019 12:25,VOID,
        //    ",
        //     new List<ProcessedRecord<MeterReading>> {
        //        ProcessedRecord<MeterReading>.WithFailure(3, FeedException.MeterReadingValueCannotBeNull()),
        //        ProcessedRecord<MeterReading>.WithFailure(6, FeedException.AccountIdCannotBeNull())

        //    }
        //};
        //yield return new object[]
        // {
        //    @"
        //       AccountId,MeterReadingDateTime,MeterReadValue,
        //       2344,22/04/2019 09:24,,


        //       ,22/04/2019 12:25,VOID,
        //        ,  ,     ,",
        //    new List<ProcessedRecord<MeterReading>> {
        //        ProcessedRecord<MeterReading>.WithFailure(3, FeedException.MeterReadingValueCannotBeNull()),
        //        ProcessedRecord<MeterReading>.WithFailure(6, FeedException.AccountIdCannotBeNull()),
        //        ProcessedRecord<MeterReading>.WithFailure(7, FeedException.AccountIdCannotBeNull()),
        //    }
        // };
    }
}
