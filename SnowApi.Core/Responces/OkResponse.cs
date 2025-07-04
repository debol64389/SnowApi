namespace SnowApi.Core.Responces;

public class OkResponse<T>
{
    public T Data { get; set; }

    public OkResponse(T data)
    {
        Data = data;
    }
}