namespace CoreLayer.Utilities.Results;

public class DataResult<T> :Result, IDataResult<T>
{
	public DataResult(T data,bool success,string message) :base(success,message)	
	{
		Data = data;
		Message = message;
		Success = success;

	}
	public DataResult(T data,bool success ) :base(success)
	{
		Data = data;
		Success = success;
	}
	public T Data { get; }

    public bool Success { get; set;}

    public string Message { get; set;}
}
