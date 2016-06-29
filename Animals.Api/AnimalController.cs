using System.Web.Http;
using Animals.Contract;
using Animals.Domain.Animals;

namespace Animals.Api
{
    public class AnimalController : ApiController
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        [HttpGet]
        [Route("v1/animal/{animalId}")]
        public IHttpActionResult GetAnimal(string animalId)
        {
            var animal = _animalRepository.GetById(animalId);

            var response = new AnimalDtoV1(
                animal.AnimalId,
                animal.UserId,
                animal.GetHappiness(), 
                animal.GetFullness());

            return Ok(response);
        }
    }
}
