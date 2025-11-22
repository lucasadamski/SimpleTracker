using System.Globalization;

namespace SimpleTracker.Utility
{
    public static class Temporal
    {
        public static DateTime? ParseToDateTime(string date)
        {
            var result = new DateTime();
            var isParseSuccess = DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowInnerWhite, out result);
            return (isParseSuccess ? result : null);
        }
    }
}
