using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.Api;
using SimpleTracker.BLL.Api.Interface;

namespace SimpleTracker
{
    internal class Program
    {
        public static string userCommand { get; set; } = string.Empty;
        public static string[] apiFormattedRequest { get; set; } = new string[10];
        public static IEnumerable<string> apiResponse { get; set; }

        static void Main(string[] args)
        {
            Utility.Logger.Log.LogInformation("Simple Tracker UI started");

            IApi api = new Api(Utility.Logger.Log);

            ShowHelpMessage();
            while (true)
            {
                ReadCommandFromUser();
                apiFormattedRequest = userCommand.Split(' ');
                apiResponse = api.Request(apiFormattedRequest);
                if (apiResponse == null) continue;
                WriteResponse(apiResponse);
            }
        }

        private static void ShowHelpMessage()
        {
            Console.WriteLine(" 3 ways to access API\r\n * 1) post entry <name> <value> // eg. post entry Reading 30 \r\n * 2) post activity <name> <unit> // eg. post activity Reading Minutes\r\n * 3) get summary all-time // returns Reading 450 Minutes, Running 3 times, Gym 5 times etc. ");
        }

        private static void ReadCommandFromUser()
        {
            while (true)
            {
                userCommand = Console.ReadLine();
                if (userCommand == null || userCommand.ToLower() == "exit" || userCommand.ToLower() == "quit")
                    ExitProgram();
                else
                    break;
            }
        }

        private static void ExitProgram()
        {
            System.Environment.Exit(1);
        }


        private static void WriteResponse(IEnumerable<string> response)
        {
            foreach (var responseMessage in response)
            {
                Console.WriteLine(responseMessage);
            }
        }
    }
}
