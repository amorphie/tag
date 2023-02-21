using amorphie.core.IBase;

public class Response : IResponse
{
    public Result Result
    {
        get;
        set;
    }
    public Response()
    {
        Result = new(Status.Error, "");
    }
    public object Data
    {
        get;
        set;
    } =
    default;
}
public class Response<T> : IResponse<T>
{
    public Response()
    {
        Result = new(Status.Error, "");
    }
    public Result Result
    {
        get;
        set;
    }
    public T Data
    {
        get;
        set;
    } =
    default;
}
public class NoDataResponse : IResponse
{
    public NoDataResponse()
    {
        Result = new(Status.Error, "");
    }
    public Result Result
    {
        get;
        set;
    }
}