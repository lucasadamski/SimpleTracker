namespace SimpleTracker.DbIntegrationTest
{
    public static class Configuration
    {
        public static string TestDbConnectionString = @"Data Source=localhost;Initial Catalog=SimpleTrackerTest;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public static int UserId = 1;


        // Unit 
        public static string UnitName1 = "times";
        public static string UnitName2 = "minutes";
        public static string UnitName3 = "hours";
        public static string UnitName4 = "days";
        public static string UnitName5 = "weeks";
        public static string UnitName6 = "years";

        // Activity 
        public static string ActivityName1 = "push-ups";
        public static string ActivityName2 = "running";
        public static string ActivityName3 = "reading";

        public static int Activity1UnitId = 1;
        public static int Activity2UnitId = 2;
        public static int Activity3UnitId = 3;

        // Entry
        public static DateTime EntryDateAdded1 = new DateTime(2025, 5, 8, 13, 0, 0);
        public static DateTime EntryDateAdded2 = new DateTime(2025, 5, 9, 14, 0, 0);
        public static DateTime EntryDateAdded3 = new DateTime(2025, 5, 10, 15, 0, 0);

        public static readonly int EntryValue1 = 1;
        public static readonly int EntryValue2 = 5;
        public static readonly int EntryValue3 = 10;

        public static readonly int EntryActivityId1 = 1;
        public static readonly int EntryActivityId2 = 2;
        public static readonly int EntryActivityId3 = 3;
        
    }
}
