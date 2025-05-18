namespace SimpleTracker.BLL.Interface
{
    public interface IApi
    {
        IEnumerable<string> Request(IEnumerable<string> data);
    }
}