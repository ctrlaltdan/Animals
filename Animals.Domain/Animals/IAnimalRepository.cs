using System.Collections.Generic;

namespace Animals.Domain.Animals
{
    public interface IAnimalRepository
    {
        Animal GetById(string animalId);
        Cat CreateCat(string animalId, string userId);
        Mouse CreateMouse(string animalId, string userId);
    }
}
