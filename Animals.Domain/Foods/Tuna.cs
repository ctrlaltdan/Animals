namespace Animals.Domain.Foods
{
    public class Tuna : IFood
    {
        public decimal Carbohydrates { get { return 0.5m; } }
        public decimal Proteins { get { return 3m; } }
        public decimal Fats { get { return 0.2m; } }
    }
}
