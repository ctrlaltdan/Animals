using Animals.Domain.Animals;
using Animals.Domain.Users;

namespace Animals.Application.Adoption
{
    public sealed class AdoptionService : IAdoptionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAnimalRepository _animalRepository;

        public AdoptionService(IUserRepository userRepository, IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
            _userRepository = userRepository;
        }

        void IAdoptionService.AdoptCat(string animalId, string userId)
        {
            var user = _userRepository.GetById(userId);
            var cat = _animalRepository.CreateCat(animalId, userId);
            user.AdoptAnimal(cat);
        }

        void IAdoptionService.AdoptMouse(string animalId, string userId)
        {
            var user = _userRepository.GetById(userId);
            var mouse = _animalRepository.CreateMouse(animalId, userId);
            user.AdoptAnimal(mouse);
        }
    }
}
