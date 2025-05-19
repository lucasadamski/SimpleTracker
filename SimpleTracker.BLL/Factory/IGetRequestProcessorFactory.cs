using SimpleTracker.BLL.DTO;
using SimpleTracker.BLL.Interface;

namespace SimpleTracker.BLL.Factory
{
    public interface IGetRequestProcessorFactory
    {
        IGetRequestProcessor ReturnGetRequestProcessor(Response response);
    }
}