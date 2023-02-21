public class Result
{
    public Status Status
    {
        get;
        set;
    }
    public string Message
    {
        get;
        set;
    } = string.Empty;
    public string MessageDetail
    {
        get;
        set;
    } = string.Empty;
    public Result(Status status, string message)
    {
        Status = status;
        Message = message;
    }
    public Result(Status status, string message, string messageDetail) : this(status, message)
    {
        MessageDetail = messageDetail;
    }
}