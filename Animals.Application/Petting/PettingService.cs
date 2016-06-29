using System;
using Animals.Domain.Animals;

namespace Animals.Application.Petting
{
    public sealed class PettingService : IPettingService
    {
        private readonly IAnimalRepository _animalRepository;

        public PettingService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        void IPettingService.PetAnimal(string animalId, string userId)
        {
            var animal = _animalRepository.GetById(animalId);

            if (animal.UserId != userId)
                throw new UnauthorizedAccessException();

            animal.Pet();
        }
    }
}
