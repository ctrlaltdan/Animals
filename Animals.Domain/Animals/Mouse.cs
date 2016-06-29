namespace Animals.Domain.Animals
{
    public class Mouse : Animal
    {
        public Mouse(string animalId, string userId)
            : base(animalId, userId)
        {
        }

        public override decimal HappinessBurnRate { get { return 1.0m; } }
        public override decimal FullnessBurnRate { get { return 1.2m; } }

        public override decimal CarbohydrateEffectiveness { get { return 1.5m; } }
        public override decimal ProteinEffectiveness { get { return 0.5m; } }
        public override decimal FatEffectiveness { get { return 1.8m; } }
    }
}
