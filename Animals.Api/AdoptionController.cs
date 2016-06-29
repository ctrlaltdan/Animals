using System.Net;
using System.Web.Http;
using Animals.Application.Adoption;

namespace Animals.Api
{
    public class AdoptAnimalController : ApiController
    {
        private readonly IAdoptionService _adoptionService;

        public AdoptAnimalController(IAdoptionService adoptionService)
        {
            _adoptionService = adoptionService;
        }

        [HttpPut]
        [Route("v1/user/{userId}/adopt/cat")]
        public IHttpActionResult AdoptCat(AdoptionRequest request, string userId)
        {
            _adoptionService.AdoptCat(request.AnimalId, request.UserId);
            return StatusCode(HttpStatusCode.Accepted);
        }

        [HttpPut]
        [Route("v1/user/{userId}/adopt/mouse")]
        public IHttpActionResult AdoptMouse(AdoptionRequest request, string userId)
        {
            _adoptionService.AdoptMouse(request.AnimalId, request.UserId);
            return StatusCode(HttpStatusCode.Accepted);
        }

        public class AdoptionRequest
        {
            public string UserId { get; set; }
            public string AnimalId { get; set; }
        }
    }
}
