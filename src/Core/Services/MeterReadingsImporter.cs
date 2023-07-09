using interview.test.ensek.Core.Contracts;
using interview.test.ensek.Core.Domain.Feed;
using interview.test.ensek.Core.Domain.Loader;
using interview.test.ensek.Core.Service.Abstractions;

namespace interview.test.ensek.Core.Services;

public sealed class MeterReadingsImporter : IMeterReadingsImporter
{
    private readonly IMeterReadingsFeedParserService _meterReadingsParser;
    private readonly IMeterReadingBatchBuilder _batchBuilder;


    public MeterReadingsImporter(
        IMeterReadingsFeedParserService meterReadingsParser,
        IMeterReadingBatchBuilder meterReadingsBatchBuilder)
    {
        _meterReadingsParser = meterReadingsParser;
        _batchBuilder = meterReadingsBatchBuilder;
    }


    public async Task<ImportResultDto> Import(Stream source, CancellationToken cancellationToken)
    {
        var successful = 0;
        var unSuccessful = 0;

        foreach (var meterReading in _meterReadingsParser.Read(source))
        { 
            if(meterReading is not null && meterReading.IsSuccess)
            {
                var addToBatchResult = await _batchBuilder.TryAddAsync(meterReading.Result!, cancellationToken);

                if(addToBatchResult.IsSuccess)
                {
                    successful++;
                    continue;
                }
            }

            unSuccessful++;
        }

        await _batchBuilder.SaveChangesAsync(cancellationToken);

        return new(successful, unSuccessful);
    }
}
