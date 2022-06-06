namespace MeikadeDataCapture.Models;

public class Base<T>
{
    public string Message { get; set; }
    public T Result { get; set; }
}