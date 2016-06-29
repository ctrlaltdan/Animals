namespace Animals.Domain.Foods
{
    public class Cheese : IFood
    {
        public decimal Carbohydrates { get { return 0.6m; } }
        public decimal Proteins { get { return 1.8m; } }
        public decimal Fats { get { return 2.2m; } }
    }
}
