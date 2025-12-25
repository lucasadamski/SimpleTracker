namespace SimpleTracker.DTO.Summary
{
    public class ActivityQuickStats
    {
        public string ActivityName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public int TodayValue { get; set; }
        public int ThisWeekValue { get; set; }
        public int ThisMonthValue { get; set; }
        public int AllTimeValue { get; set; }

    }
}
