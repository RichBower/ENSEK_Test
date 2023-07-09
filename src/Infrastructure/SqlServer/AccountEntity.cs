using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Core.Domain.Loader;

public sealed class AccountEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AccountEntityID { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public ICollection<MeterReadingEntity>? Readings { get; set; }
}
