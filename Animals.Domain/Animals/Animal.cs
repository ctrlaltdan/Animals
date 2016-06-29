using System;
using Animals.Domain.Foods;

namespace Animals.Domain.Animals
{
    public abstract class Animal
    {
        private const decimal Neutral = 0.0m;

        private const decimal MaximumHappiness = 100.0m;
        private const decimal MinimumHappiness = -100.0m;

        private const decimal MaximumFullness = 100.0m;
        private const decimal MinimumFullness = -100.0m;

        protected Animal(string animalId, string userId)
        {
            AnimalId = animalId;
            UserId = userId;

            Happiness = Neutral;
            LastPetted = DateTimeProvider.Instance.UtcNow;

            Fullness = Neutral;
            LastFed = DateTimeProvider.Instance.UtcNow;
        }

        public string AnimalId { get; private set; }
        public string UserId { get; private set; }

        #region Happiness ------------------------------------------------------------------------
        private decimal Happiness { get; set; }
        private DateTime LastPetted { get; set; }
        public abstract decimal HappinessBurnRate { get; }

        public decimal GetHappiness()
        {
            var now = DateTimeProvider.Instance.UtcNow;
            var timespan = now.Subtract(LastPetted);
            var minutesSinceLastInteraction = (decimal)timespan.TotalMinutes;

            var happinessReducedOverTime = minutesSinceLastInteraction * HappinessBurnRate;

            var rebalancedHappinessLevel = Happiness - happinessReducedOverTime;

            return Math.Max(MinimumHappiness, rebalancedHappinessLevel);
        }
        public void Pet()
        {
            const decimal positivity = 20.0m;

            IncreaseHappiness(positivity);
            LastPetted = DateTimeProvider.Instance.UtcNow;
        }
        private void IncreaseHappiness(decimal increment)
        {
            var happiness = GetHappiness();
            happiness += increment;
            Happiness = Math.Min(MaximumHappiness, happiness);
        }
        #endregion

        #region Fullness -------------------------------------------------------------------------
        public decimal GetFullness()
        {
            var now = DateTimeProvider.Instance.UtcNow;
            var timespan = now.Subtract(LastFed);
            var minutesSinceLastFed = (decimal)timespan.TotalMinutes;

            var fullnessReducedOverTime = minutesSinceLastFed * FullnessBurnRate;

            var rebalancedFullness = Fullness - fullnessReducedOverTime;

            return rebalancedFullness < MinimumFullness
                ? MinimumFullness
                : rebalancedFullness;
        }
        private decimal Fullness { get; set; }
        private DateTime LastFed { get; set; }
        public abstract decimal FullnessBurnRate { get; }

        public abstract decimal CarbohydrateEffectiveness { get; }
        public abstract decimal ProteinEffectiveness { get; }
        public abstract decimal FatEffectiveness { get; }

        public void Feed(IFood food)
        {
            var carbs = food.Carbohydrates * CarbohydrateEffectiveness;
            var proteins = food.Proteins * ProteinEffectiveness;
            var fats = food.Fats * FatEffectiveness;

            var satiety = carbs + proteins + fats;

            IncreaseFullness(satiety);
            LastFed = DateTimeProvider.Instance.UtcNow;
        }

        protected void IncreaseFullness(decimal increment)
        {
            var fullness = GetFullness();
            fullness += increment;
            Fullness = Math.Min(MaximumFullness, fullness);
        }
        #endregion
    }
}
