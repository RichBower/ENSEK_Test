using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace interview.test.ensek.Core.Domain.Feed
{
    public sealed class FeedException : Exception
    {
        public FeedException()
        {
        }

        public FeedException(string? message) : base(message)
        {
        }

        public FeedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FeedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static FeedException AccountIdIsNotAnInteger(string value) => new FeedException($"Account Id {value} not in expected format NNNNN");


        public static FeedException InsufficientFields() => new FeedException($"Insufficient fields");

        public static FeedException AccountIdCannotBeNull() => new FeedException($"AccountId cannot be null");

        public static FeedException FirstNameCannotBeNull() => new FeedException($"FirstName cannot be null");

        public static FeedException LastNameCannotBeNull() => new FeedException($"LastName cannot be null");

        public static FeedException MeterReadingDateTimeCannotBeNull() => new FeedException($"MeterReadingDateTime cannot be null");

        public static FeedException MeterReadingValueCannotBeNull() => new FeedException($"MeterReadingValue cannot be null");

        public static FeedException MeterReadingDateTimeFormatIsInvalid(string value, string expectedFormat) => new FeedException($"MeterReadingDateTime {value} not in expected format {expectedFormat}");

        public static FeedException MeterReadingIsNotnExpectedFormat(string value) => new FeedException($"MeterReadingDateTime {value} not in expected format NNNNN");

        public static FeedException MeterReadingValueIsNotInRange(int value, int min, int max) => new FeedException($"MeterReadValue value {value} must be between {min} and {max}");
    }
}
