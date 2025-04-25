using Microsoft.Extensions.Logging;
using SimpleTracker.BLL;


namespace SimpleTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Utility.Logger.Log.LogInformation("Simple Tracker UI started");

            IApi api = new Api();

            var message = new List<string>()
            {
                "post",
                "activity",
                "ReadingNonFiction",
                "1"
            };

            var response = api.Request(message);

            foreach (var responseMessage in response)
            {
                Console.WriteLine(responseMessage);
            }

            Utility.Logger.Log.LogInformation("Simple Tracker UI stopped");
        }
    }
}
