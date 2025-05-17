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

            var response = api.Request(args.ToList());

            foreach (var responseMessage in response)
            {
                Console.WriteLine(responseMessage);
            }

            Utility.Logger.Log.LogInformation("Simple Tracker UI stopped");
        }



    }
}
