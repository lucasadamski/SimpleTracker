namespace SimpleTracker.BLL
{
    public class Api : IApi
    {
        public bool Write(List<string> data)
        {
            return true;
        }

        public List<string> Read(List<string> data)
        {
            return new List<string>();
        }
    }

}
