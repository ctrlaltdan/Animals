using Animals.Domain.Animals;
using Animals.Domain.Foods;

namespace Animals.Application.Feeding
{
    public sealed class FeedingService : IFeedingService
    {
        private readonly IAnimalRepository _animalRepository;

        public FeedingService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        void IFeedingService.FeedAnimal(string animalId, IFood food)
        {
            var animal = _animalRepository.GetById(animalId);
            
            animal.Feed(food);
        }
    }
}
