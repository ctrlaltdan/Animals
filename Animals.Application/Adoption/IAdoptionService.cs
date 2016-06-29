namespace Animals.Application.Adoption
{
    public interface IAdoptionService
    {
        void AdoptCat(string animalId, string userId);
        void AdoptMouse(string animalId, string userId);
    }
}
