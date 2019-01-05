using System.Collections.Generic;

namespace DominionWeb.Game.Utils
{
    public static class ListExtensions
    {
        public static void RemoveFrom<T>(this List<T> lst, int from)
        {
            lst.RemoveRange(from, lst.Count - from);
        }
    }
}