using System.Net;
using System.Web.Http;
using Animals.Application;
using Animals.Application.Petting;

namespace Animals.Api
{
    public class PetAnimalController : ApiController
    {
        private readonly IPettingService _pettingService;

        public PetAnimalController(IPettingService pettingService)
        {
            _pettingService = pettingService;
        }

        [HttpPut]
        [Route("v1/animal/{animalId}/pet")]
        public IHttpActionResult PetAnimal(PetAnimalRequest request, string animalId)
        {
            _pettingService.PetAnimal(request.AnimalId, request.UserId);
            return StatusCode(HttpStatusCode.Accepted);
        }

        public class PetAnimalRequest
        {
            public string UserId { get; set; }
            public string AnimalId { get; set; }
        }
    }
}