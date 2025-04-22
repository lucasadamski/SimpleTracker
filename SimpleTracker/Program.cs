using SimpleTracker.BLL;

namespace SimpleTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IApi api = new Api();

            var message = new List<string>()
            {
                "post",
                "entry",
                "running",
                "30"
            };

            var response = api.Request(message);

            foreach (var responseMessage in response)
            {
                Console.WriteLine(responseMessage);
            }
        }
    }
}
