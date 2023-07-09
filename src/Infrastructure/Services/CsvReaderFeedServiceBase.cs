using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper;
using interview.test.ensek.Core.Domain.Feed;

namespace interview.test.ensek.Infrastructure.Services
{
    public abstract class CsvReaderFeedServiceBase<TRecord> 
        where TRecord : notnull
    {

        protected CsvReaderFeedServiceBase()
        {

        }

        protected abstract TRecord Map(CsvReader reader);

        protected abstract bool IsRowValid(CsvReader reader);

        protected IEnumerable<TRecord> Parse(Stream source)
        {
            if (source is null || source.CanRead == false || source.Length == 0)
            {
                yield break;
            }

            using var reader = new StreamReader(source);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,
                MissingFieldFound = null,
                HeaderValidated = null,
            });

            // Skip the header
            if (csv.Configuration.HasHeaderRecord == true)
            {
                csv.Read();
                csv.ReadHeader();
            }

            while (csv.Read())
            {
                if (IsRowValid(csv))
                {

                    yield return Map(csv);
                }
               
            }
        }

        public IEnumerable<TRecord> Read(Stream source)
        {
            return Parse(source);
        }
    }
}
