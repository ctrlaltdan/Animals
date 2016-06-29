using System.Net;
using System.Web.Http;
using Animals.Application;
using Animals.Application.Feeding;
using Animals.Domain.Foods;

namespace Animals.Api
{
    public class FeedAnimalController : ApiController
    {
        private readonly IFeedingService _feedingService;

        public FeedAnimalController(IFeedingService feedingService)
        {
            _feedingService = feedingService;
        }

        [HttpPut]
        [Route("v1/animal/{animalId}/feed/tuna")]
        public IHttpActionResult FeedAnimalTuna(FeedAnimalRequest request, string animalId)
        {
            var tuna = new Tuna();

            _feedingService.FeedAnimal(request.AnimalId, tuna);

            return StatusCode(HttpStatusCode.Accepted);
        }

        [HttpPut]
        [Route("v1/animal/{animalId}/feed/cheese")]
        public IHttpActionResult FeedAnimalCheese(FeedAnimalRequest request, string animalId)
        {
            var cheese = new Cheese();

            _feedingService.FeedAnimal(request.AnimalId, cheese);

            return StatusCode(HttpStatusCode.Accepted);
        }

        public class FeedAnimalRequest
        {
            public string AnimalId { get; private set; }
            public string UserId { get; private set; }

            public FeedAnimalRequest(string animalId, string userId)
            {
                UserId = userId;
                AnimalId = animalId;
            }
        }
    }
}