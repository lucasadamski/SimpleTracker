namespace SimpleTracker.BLL.Api.Interface
{
    public interface IApi
    {
        IEnumerable<string> Request(IEnumerable<string> data);
    }
}