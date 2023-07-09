using interview.test.ensek.Core.Contracts;

namespace interview.test.ensek.Core.Service.Abstractions;

public interface IMeterReadingsImporter
{
    Task<ImportResultDto> Import(Stream source, CancellationToken cancellationToken);
}
