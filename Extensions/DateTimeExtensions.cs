using System;

namespace MWTest.Extensions
{
    public static class DateTimeExtensions
    {
        /// <returns>DateTime converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        public static long ToUnixEpoch(this DateTime date)
        {
            return (long)Math.Round((
                date.ToUniversalTime() -
                new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)
            ).TotalSeconds);
        }
    }
}
