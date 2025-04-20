namespace SimpleTracker.BLL.DTO
{
    internal class RequestResult
    {
        public List<string> Messages { get; set; } = new List<string>();
        public bool Success { get; set; } = true;
        public bool IsGet { get; set; } = false;
        public bool IsPost { get; set; } = false;
    }
}
