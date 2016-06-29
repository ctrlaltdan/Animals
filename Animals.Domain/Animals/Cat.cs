using Animals.Domain.Foods;

namespace Animals.Domain.Animals
{
    public class Cat : Animal
    {
        public Cat(string animalId, string userId)
            : base(animalId, userId)
        {
        }

        public override decimal HappinessBurnRate { get { return 2.0m; } }
        public override decimal FullnessBurnRate { get { return 2.4m; } }

        public override decimal CarbohydrateEffectiveness {get { return 1.0m; } } 
        public override decimal ProteinEffectiveness { get { return 1.0m; } }
        public override decimal FatEffectiveness { get { return 1.0m; } }
    }
}
