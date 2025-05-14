using SimpleTracker.DTO;

namespace SimpleTracker.BLL.DTO
{
    public class Request
    {
        public Activity Activity { get; set; }
        public Entry Entry { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public bool Success { get; set; } = true;
    }
}
