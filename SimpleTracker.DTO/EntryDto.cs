namespace SimpleTracker.DTO
{
    public class EntryDto : EntityBase
    {
        public int Value { get; set; }
        public string Unit { get; set; }
        public string Activity { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
