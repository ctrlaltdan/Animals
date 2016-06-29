using System.Collections.Generic;

namespace Animals.Contract
{
    public class UserDtoV1
    {
        public UserDtoV1(string userId, IEnumerable<string> animalIds)
        {
            UserId = userId;
            AnimalIds = animalIds;
        }

        public string UserId { get; private set; }
        public IEnumerable<string> AnimalIds { get; private set; }
    }
}