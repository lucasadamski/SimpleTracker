using SimpleTracker.BLL.DTO;
using SimpleTracker.BLL.Interface;

namespace SimpleTracker.BLL.Factory
{
    public interface IPostRequestProcessorFactory
    {
        IPostRequestProcessor ReturnPostRequestProcessor(Response response);
    }
}