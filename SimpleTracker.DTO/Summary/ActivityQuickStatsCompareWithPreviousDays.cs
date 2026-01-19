namespace SimpleTracker.DTO.Summary
{
    public class ActivityQuickStatsCompareWithPreviousDays : ActivityQuickStats
    {
        public int YesterdayValue { get; set; }
        public int LastWeekValue { get; set; }
        public int LastMonthValue { get; set; }
    }
}
