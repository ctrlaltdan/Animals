using System;
using Animals.Domain;

namespace Animals.Test.Unit
{
    public class MockDateTimeProvider : IDateTimeProvider
    {
        public MockDateTimeProvider(DateTime specificDateTime)
        {
            UtcNow = specificDateTime;
        }

        public DateTime UtcNow { get; private set; }
    }
}
