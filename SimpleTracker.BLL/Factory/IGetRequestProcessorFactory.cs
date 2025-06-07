using SimpleTracker.BLL.DTO;
using SimpleTracker.BLL.RequestProcessor.Interface;

namespace SimpleTracker.BLL.Factory
{
    public interface IGetRequestProcessorFactory
    {
        IGetRequestProcessor ReturnGetRequestProcessor(Response response);
    }
}