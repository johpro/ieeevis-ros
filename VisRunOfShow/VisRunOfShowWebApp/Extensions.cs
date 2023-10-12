namespace IeeeVisRunOfShowWebApp
{
    public static class Extensions
    {
        //UTC-5
        private static readonly TimeZoneInfo Ok = TimeZoneInfo.CreateCustomTimeZone("ieee-oklahoma", TimeSpan.FromHours(-5), "US Central Daylight Time", "US Central Daylight Time");
        public static DateTime ToOklahoma(this DateTime dt)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dt.ToUniversalTime(), Ok);
        }
    }
}
