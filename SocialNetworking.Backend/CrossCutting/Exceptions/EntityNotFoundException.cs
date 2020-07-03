using System;
using System.Globalization;

namespace CrossCutting.Exceptions
{
    public class EntityNotFoundException<T> : Exception
    {
        public override string Message => "Search Error";

        public override string Source => string.Format("{0} not found.", ToTitleCase(nameof(T)));


        private static string ToTitleCase(string str) =>
            CultureInfo.InvariantCulture.TextInfo.ToTitleCase(str.ToLower());
    }
}
