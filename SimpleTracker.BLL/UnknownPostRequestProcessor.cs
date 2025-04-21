
namespace SimpleTracker.BLL
{
    public class UnknownPostRequestProcessor : IPostRequestProcessor
    {
        public List<string> Process(List<string> data)
        {
            return new List<string>() { "Item type unknown. Must be activity or entry" };
        }
    }
}
