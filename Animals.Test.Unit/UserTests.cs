using System.Linq;
using Animals.Domain.Animals;
using Animals.Domain.Users;
using NUnit.Framework;

namespace Animals.Test.Unit
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void A_user_can_be_created()
        {
            var userId = "user_test_1";
            var user = new User(userId);

            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.UserId);
        }

        [Test]
        public void A_user_can_adopt_a_cat()
        {
            var userId = "user_test_2";
            var user = new User(userId);

            var animalId = string.Format("{0}|test_cat_1", userId);

            var cat = new Cat(animalId, userId);

            user.AdoptAnimal(cat);

            Assert.AreEqual(1, user.Animals.Count);
            Assert.IsInstanceOf<Cat>(user.Animals.Single());
        }

        [Test]
        public void A_user_can_adopt_a_mouse()
        {
            var userId = "user_test_3";
            var user = new User(userId);

            var animalId = string.Format("{0}|test_mouse_1", userId);

            var mouse = new Mouse(animalId, userId);

            user.AdoptAnimal(mouse);

            Assert.AreEqual(1, user.Animals.Count);
            Assert.IsInstanceOf<Mouse>(user.Animals.Single());
        }

        [Test]
        public void A_user_can_own_multiple_pets_of_different_types()
        {
            var userId = "user_test_4";
            var user = new User(userId);
            
            var catId = string.Format("{0}|test_cat_1", userId);
            var cat = new Cat(catId, userId);
            user.AdoptAnimal(cat);

            var mouseId = string.Format("{0}|test_mouse_1", userId);
            var mouse = new Mouse(mouseId, userId);
            user.AdoptAnimal(mouse);

            Assert.AreEqual(2, user.Animals.Count);
            Assert.IsInstanceOf<Cat>(user.Animals.First(a => a.AnimalId == catId));
            Assert.IsInstanceOf<Mouse>(user.Animals.First(a => a.AnimalId == mouseId));
        }
    }
}
