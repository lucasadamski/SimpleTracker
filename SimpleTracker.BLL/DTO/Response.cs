using SimpleTracker.DTO;

namespace SimpleTracker.BLL.DTO
{
    public record Response
    {
        public IEnumerable<string>? Arguments { get; set; }
        public IEnumerable<string>? Output { get; set; }
        public int Value { get; set; } = -1;
        public bool Success { get; set; }
        public Type Type { get; set; }
        public RequestVerb RequestVerb { get; set; }
        public IEnumerable<Activity>? Activity { get; set; }
        public IEnumerable<Entry>? Entry { get; set; }
    }
}
