using SimpleTracker.BLL.Interface;

namespace SimpleTracker.BLL
{
    public class UnknownGetRequestProcessor : IGetRequestProcessor
    {
        public List<string> Process(List<string> data)
        {
            return new List<string>() { "Item type unknown. Must be activity or entry" };
        }
    }
}
