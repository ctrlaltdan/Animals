using System;

namespace Animals.Domain
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
