
using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Feeds;
public sealed class MeterReadingHeaderAndDataTests : MeterReadingsFeedBase
{
    public MeterReadingHeaderAndDataTests() : base()
    {
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void When_header_and_many_data_rows(string input, List<ProcessedRecord<MeterReading>> expected)
    {
        Runner(input, expected);
    }

    public static IEnumerable<object[]> Data()
    {
        yield return new object[]
            {
            @"
               AccountId,MeterReadingDateTime,MeterReadValue,
234422/04/2019 09:24
 2344,22/04/2019 09:24,1002
            ",
            new List<ProcessedRecord<MeterReading>> {
                ProcessedRecord<MeterReading>.WithFailure(3, FeedException.InsufficientFields()),
                ProcessedRecord<MeterReading>.WithSuccess(4, new MeterReading(new AccountId("2344"), new MeterReadingDateTime("22/04/2019 09:24"), new MeterReadValue( "1002")))
            }
            };

//        yield return new object[]
//            {
//            @"
//               AccountId,MeterReadingDateTime,MeterReadValue,
               

//                2344,22/04/2019 09:24,1002,
//               2233,22/04/2019 12:25,323,


//            ",
//            new List<ProcessedRecord<MeterReading>> {
//                ProcessedRecord<MeterReading>.WithSuccess(3, new MeterReading(new AccountId("2344"), new MeterReadingDateTime("22/04/2019 09:24"), new MeterReadValue( "1002"))),
//                ProcessedRecord<MeterReading>.WithSuccess(4, new MeterReading(new AccountId("2233"), new MeterReadingDateTime("22/04/2019 12:25"), new MeterReadValue( "323")))
//            }
//            };
//        yield return new object[]
//            {
//            @"
//               AccountId,MeterReadingDateTime,MeterReadValue,

//                2344,22/04/2019 09:24,1002,
//               2233,22/04/2019 12:25,323,

//234422/04/2019 09:24


//              t

//            1248,26/05/2019 09:24,3467,",
//            new List<ProcessedRecord<MeterReading>> {
//                ProcessedRecord<MeterReading>.WithSuccess(2, new MeterReading(new AccountId("2344"), new MeterReadingDateTime("22/04/2019 09:24"), new MeterReadValue( "1002"))),
//                ProcessedRecord<MeterReading>.WithSuccess(3, new MeterReading(new AccountId("2233"), new MeterReadingDateTime("22/04/2019 12:25"), new MeterReadValue( "323"))),
//                ProcessedRecord<MeterReading>.WithFailure(5, FeedException.MeterReadingDateTimeCannotBeNull()),
//                ProcessedRecord<MeterReading>.WithFailure(8, FeedException.MeterReadingValueCannotBeNull()),
//                ProcessedRecord<MeterReading>.WithSuccess(10, new MeterReading(new AccountId("1248"), new MeterReadingDateTime("26/05/2019 09:24"), new MeterReadValue( "3467")))
//            }
//            };
    }
}

