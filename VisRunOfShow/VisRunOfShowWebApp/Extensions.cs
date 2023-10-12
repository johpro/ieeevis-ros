namespace IeeeVisRunOfShowWebApp
{
    public static class Extensions
    {
        //UTC-5
        private static readonly TimeZoneInfo Ok = TimeZoneInfo.CreateCustomTimeZone("ieee-oklahoma", TimeSpan.FromHours(-5), "US Central Daylight Time", "US Central Daylight Time");
        //UTC+11
        private static readonly TimeZoneInfo Mel = TimeZoneInfo.CreateCustomTimeZone("ieee-melbourne", TimeSpan.FromHours(11), "AEDT", "AEDT");
        public static DateTime ToOklahoma(this DateTime dt)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dt.ToUniversalTime(), Ok);
        }

        public static DateTime ToMelbourne(this DateTime dt)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dt.ToUniversalTime(), Mel);
        }
    }
}
