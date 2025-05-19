using SimpleTracker.DTO;

namespace SimpleTracker.BLL.DTO
{
    public record Request
    {
        public RequestVerb RequestVerb { get; set; }
        public IEnumerable<string> Arguments { get; set; } = new List<string>();
        public Type Type { get; set; }
        public bool Success { get; set; } = true;
    }
}
