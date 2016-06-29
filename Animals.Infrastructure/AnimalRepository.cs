using System.Linq;
using Animals.Domain.Animals;

namespace Animals.Infrastructure
{
    public sealed class AnimalRepository : IAnimalRepository
    {
        Animal IAnimalRepository.GetById(string animalId)
        {
            return Db.Animals.Single(a => a.AnimalId == animalId);
        }

        public Cat CreateCat(string animalId, string userId)
        {
            var cat = new Cat(animalId, userId);
            
            Db.Animals.Add(cat);

            return cat;
        }

        public Mouse CreateMouse(string animalId, string userId)
        {
            var mouse = new Mouse(animalId, userId);

            Db.Animals.Add(mouse);

            return mouse;
        }
    }
}