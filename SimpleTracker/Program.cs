/* 3 ways to access API
 * 1) post entry <name> <value> // eg. post entry Reading 30 
 * 2) post activity <name> <unit> // eg. post activity Reading Minutes
 * 3) get summary all-time // returns Reading 450 Minutes, Running 3 times, Gym 5 times etc. 
 */

using Microsoft.Extensions.Logging;
using SimpleTracker.BLL;
using SimpleTracker.BLL.Interface;


namespace SimpleTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Utility.Logger.Log.LogInformation("Simple Tracker UI started");

            IApi api = new Api(Utility.Logger.Log);

            // test
            args = ["get", "summary", "all-time"];

            var response = api.Request(args);

            foreach (var responseMessage in response)
            {
                Console.WriteLine(responseMessage);
            }

            Utility.Logger.Log.LogInformation("Simple Tracker UI stopped");
        }


    }
}
