namespace SimpleTracker.DTO.Summary
{
    public class ActivitySummaryTotal : ResponseBase
    {
        public IEnumerable<ActivitySummary> Activities { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
    }
}
