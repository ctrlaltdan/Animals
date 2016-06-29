namespace Animals.Domain.Foods
{
    public interface IFood
    {
        decimal Carbohydrates { get; }
        decimal Proteins { get; }
        decimal Fats { get; }
    }
}
