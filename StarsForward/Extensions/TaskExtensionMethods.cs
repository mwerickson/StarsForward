using System.Threading.Tasks;

namespace StarsForward.Extensions
{
    public static class TaskExtensionMethods
    {
        public static void RunForget(this Task t)
        {
            t.ContinueWith((tResult) =>
                {
                    //Console.WriteLine(t.Exception)
                    //TODO: Log to Xamarin insights
                },
                TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}