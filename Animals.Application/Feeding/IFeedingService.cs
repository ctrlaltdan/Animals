using Animals.Domain.Foods;

namespace Animals.Application.Feeding
{
    public interface IFeedingService
    {
        void FeedAnimal(string animalId, IFood food);
    }
}
