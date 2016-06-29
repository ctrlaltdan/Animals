namespace Animals.Application.Petting
{
    public interface IPettingService
    {
        void PetAnimal(string animalId, string userId);
    }
}
