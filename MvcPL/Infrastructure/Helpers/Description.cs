using System.Linq;

namespace MvcPL.Infrastructure.Helpers
{
    public static class Description
    {
        public static string GetDescription(int wordsInDescription, string str)
        {
            return str.Split(' ').Take(wordsInDescription).Aggregate((x, y) => x + " " + y) + "...";
        }
    }
}