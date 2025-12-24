namespace SimpleTracker.DTO
{
    public class Activity : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int UnitId { get; set; }
        public int UserId { get; set; }

    }
}
