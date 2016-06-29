namespace Animals.Contract
{
    public class AnimalDtoV1
    {
        public string AnimalId { get; private set; }
        public string UserId { get; private set; }
        public decimal Happiness { get; private set; }
        public decimal Fullness { get; private set; }

        public AnimalDtoV1(string animalId, string userId, decimal happiness, decimal fullness)
        {
            AnimalId = animalId;
            UserId = userId;
            Happiness = happiness;
            Fullness = fullness;
        }
    }
}
