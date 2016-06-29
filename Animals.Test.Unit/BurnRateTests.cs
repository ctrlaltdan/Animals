using Animals.Domain.Animals;
using NUnit.Framework;

namespace Animals.Test.Unit
{
    [TestFixture]
    public class BurnRateTests
    {
        [Test]
        public void Animals_should_have_different_happiness_burn_rates()
        {
            var userId = "user_3_burn_rate_tests";

            var catId = string.Format("{0}|cat_1", userId);
            var mouseId = string.Format("{0}|mouse_1", userId);

            var cat = new Cat(catId, userId);
            var mouse = new Mouse(mouseId, userId);

            Assert.AreNotEqual(cat.HappinessBurnRate, mouse.HappinessBurnRate);
        }

        [Test]
        public void Animals_should_have_different_fullness_burn_rates()
        {
            var userId = "user_3_burn_rate_tests";

            var catId = string.Format("{0}|cat_2", userId);
            var mouseId = string.Format("{0}|mouse_2", userId);

            var cat = new Cat(catId, userId);
            var mouse = new Mouse(mouseId, userId);

            Assert.AreNotEqual(cat.FullnessBurnRate, mouse.FullnessBurnRate);
        }
    }
}
