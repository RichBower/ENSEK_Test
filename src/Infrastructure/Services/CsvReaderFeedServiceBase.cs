using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using interview.test.ensek.Core.Domain.Feed;

namespace interview.test.ensek.Infrastructure.Services;

public abstract class CsvReaderFeedServiceBase<TRecord> 
    where TRecord : notnull
{

    protected CsvReaderFeedServiceBase()
    {

    }

    private readonly IReaderConfiguration _csvConfiguraton = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        Delimiter = ",",
        TrimOptions = TrimOptions.Trim,
        IgnoreBlankLines = true,
        MissingFieldFound = null,
        HeaderValidated = null,
    };

    protected abstract TRecord Map(CsvReader reader);

    protected abstract bool DoesRowContainSufficientFields(CsvReader reader);

    /// <summary>
    /// Attempt to read each line from the source stream.
    /// I've decided to use the CsvHelper library to help with the csv parsing.  It's not the best
    /// library, but it's better than me writing my own - a bit overkill
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public IEnumerable<ProcessedRecord<TRecord>> Read(Stream source)
    {
        if (source is null || source.CanRead == false || source.Length == 0)
        {
            yield break;
        }

        using var reader = new StreamReader(source);
        using var csv = new CsvReader(reader, _csvConfiguraton);

        // Skip the header
        if (csv.Configuration.HasHeaderRecord == true)
        {
            csv.Read();
            csv.ReadHeader();
        }

        /// Read line by line, yielding the process result.
        while (csv.Read())
        {
            // Skip white space - not sure why the parser doesn't do this!
            if(string.IsNullOrWhiteSpace(csv.Parser.RawRecord))
            {
                continue;
            }

            // Simple sanity check
            if (false == DoesRowContainSufficientFields(csv))
            {
                yield return ProcessedRecord<TRecord>.WithFailure(csv.Parser.Row, FeedException.InsufficientFields());
            }
            else
            {
                // Attempt to map the source line. We are only bothered about known failures at the moment.
                ProcessedRecord<TRecord>? res;

                try
                {
                    var mapped = Map(csv);
                    res = ProcessedRecord<TRecord>.WithSuccess(csv.Parser.Row, mapped);

                }
                catch(FeedException ex)
                {
                    res = ProcessedRecord<TRecord>.WithFailure(csv.Parser.Row, ex); ;
                }

                yield return res;
                
            }
           
        }
    }
}
