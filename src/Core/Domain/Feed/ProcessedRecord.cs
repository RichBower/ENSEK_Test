namespace interview.test.ensek.Core.Domain.Feed;

/// <summary>
/// I was intending using OneOf, but I forgot about until it was too late - old age.
/// This acts as a simple method of informing consumer the status of processing a record
/// </summary>
/// <typeparam name="T"></typeparam>
public  sealed class ProcessedRecord<T>
    where T: notnull
{
    public bool IsSuccess { get; init; }
    public int RowNumber { get; init; }

    public FeedException? FailureReason { get; init; }

    public T? Result { get; init; }

    private ProcessedRecord()
    {
        
    }

    private ProcessedRecord(bool isSuccess, int rowNumber)
        :this()
    {
        IsSuccess = isSuccess;
        RowNumber = rowNumber;
    }

    private ProcessedRecord(int rowNumber, FeedException ex) 
        :this(false, rowNumber)
    {
        FailureReason = ex;
    }

    private ProcessedRecord(int rowNumber, T result)
        : this(true, rowNumber)
    {
        Result = result;
    }

    public static ProcessedRecord<T> WithFailure(int rowNumber, FeedException ex) => new ProcessedRecord<T>(rowNumber, ex);

    public static ProcessedRecord<T> WithSuccess(int rowNumber, T source) => new ProcessedRecord<T>(rowNumber, source);
}
