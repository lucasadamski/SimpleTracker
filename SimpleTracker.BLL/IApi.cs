namespace SimpleTracker.BLL
{
    public interface IApi
    {
        List<string> Read(List<string> data);
        bool Write(List<string> data);
    }
}