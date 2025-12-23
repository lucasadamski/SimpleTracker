namespace SimpleTracker.DTO
{
    public class Entry : EntityBase
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int ActivityId { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
