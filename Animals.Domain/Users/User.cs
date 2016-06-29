using System.Collections.Generic;
using System.Linq;
using Animals.Domain.Animals;

namespace Animals.Domain.Users
{
    public class User
    {
        public readonly string UserId;

        public User(string userId)
        {
            UserId = userId;
            Animals = new List<Animal>();
        }

        public IList<Animal> Animals { get; private set; }

        public void AdoptAnimal(Animal animal)
        {
            if (Animals.Any(a => a.AnimalId == animal.AnimalId))
                return;

            Animals.Add(animal);
        }
    }
}
