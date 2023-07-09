using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace interview.test.ensek.Core.Domain.Loader;

public sealed class MeterReadingEntity
{
    public int MeterReadingEntityID { get; set; }

    public int AccountEntityID { get; set; }
    
    public DateTime? MeterReadingDateTime { get; set; }

    public int? MeterReadValue { get; set; }

    public AccountEntity Account { get; set; }
}
