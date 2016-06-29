using System.Linq;
using System.Web.Http;
using Animals.Application.User;
using Animals.Contract;

namespace Animals.Api
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("v1/user/{userId}")]
        public IHttpActionResult GetUser(string userId)
        {
            var user = _userService.GetUserById(userId);

            var animalIds = user.Animals.Select(a => a.AnimalId);

            var response = new UserDtoV1(user.UserId, animalIds);

            return Ok(response);
        }
    }
}
