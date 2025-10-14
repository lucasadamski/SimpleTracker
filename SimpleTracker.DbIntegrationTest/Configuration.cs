namespace SimpleTracker.DbIntegrationTest
{
    public static class Configuration
    {
        public static string TestDbConnectionString = @"Data Source=localhost;Initial Catalog=SimpleTrackerTest;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public static string UserId1 = "testUser";

        public static string ActivityName1 = "push-ups";
        public static string ActivityName2 = "running";
        public static string ActivityName3 = "reading";
        
        public static int ActivityUnitId1 = 1;
        public static int ActivityUnitId2 = 2;
        public static int ActivityUnitId3 = 3;

        public static DateTime EntryDateAdded1 = new DateTime(2025, 5, 8, 13, 0, 0);
        public static DateTime EntryDateAdded2 = new DateTime(2025, 5, 9, 14, 0, 0);
        public static DateTime EntryDateAdded3 = new DateTime(2025, 5, 10, 15, 0, 0);

        // todo put values into tests

    }
}
