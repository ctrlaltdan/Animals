using System;

namespace Animals.Domain
{
    public class DateTimeProvider : IDateTimeProvider
    {
        static DateTimeProvider()
        {
            ResetInstance();
        }

        private DateTimeProvider()
        {
        }

        public static IDateTimeProvider Instance { get; set; }

        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }

        public static void ResetInstance()
        {
            Instance = new DateTimeProvider();
        }
    }
}
