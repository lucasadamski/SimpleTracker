namespace SimpleTracker.UnitTests.Helper
{
    public static class ListGenerator
    {
        public static List<T> Generate<T>(int elements) where T : new()
        {

            var result = new List<T>();

            if (elements > 0)
            {
                for (int i = 0; i < elements; i++)
                {
                    result.Add(new T());
                }
            }

            return result;
        }


        public static List<string> Generate(int elements)
        {

            var result = new List<string>();

            if (elements > 0)
            {
                for (int i = 0; i < elements; i++)
                {
                    result.Add(string.Empty);
                }
            }

            return result;
        }
    }
}
