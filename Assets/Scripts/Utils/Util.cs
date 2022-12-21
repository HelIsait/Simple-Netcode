#nullable enable
using System;


namespace Stanislav.Network.From.Nick
{
    public static class Util
    {
        public static T EnsureNotNull<T>(this T? value)
        {
            if (value == null)
            {
                throw new NullReferenceException(typeof(T).FullName);
            }

            return value;
        }
    }
}