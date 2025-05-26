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

            string message = string.Empty;
            string[] result = new string[10];

            Console.WriteLine(" 3 ways to access API\r\n * 1) post entry <name> <value> // eg. post entry Reading 30 \r\n * 2) post activity <name> <unit> // eg. post activity Reading Minutes\r\n * 3) get summary all-time // returns Reading 450 Minutes, Running 3 times, Gym 5 times etc. ");

            while (true)
            {
                message = Console.ReadLine();
                if (message == null || message.ToLower() == "exit" || message.ToLower() == "quit") break;
                result = message.Split(' ');

                var response = api.Request(result);

                foreach (var responseMessage in response)
                {
                    Console.WriteLine(responseMessage);
                }

            }

            Utility.Logger.Log.LogInformation("Simple Tracker UI stopped");
        }

    }
}
