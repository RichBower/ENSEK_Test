using interview.test.ensek.Core.Domain.Feed;

namespace interview.test.ensek.Core.Domain.Loader;

public sealed class AddToBatchResult
{
    public bool IsSuccess { get; init; }
    
    public BatchException? FailureReason { get; init; }

    private AddToBatchResult()
    {

    }
    private AddToBatchResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    private AddToBatchResult(BatchException ex)
        : this(false)
    {
        FailureReason = ex;
    }

    public static AddToBatchResult WithFailure(BatchException ex) => new AddToBatchResult(ex);

    public static AddToBatchResult WithSuccess() => new AddToBatchResult(true);
}
