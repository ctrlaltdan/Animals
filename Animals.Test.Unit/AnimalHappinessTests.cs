using System;
using Animals.Domain;
using Animals.Domain.Animals;
using NUnit.Framework;

namespace Animals.Test.Unit
{
    [TestFixture]
    public class AnimalHappinessTests
    {
        private readonly DateTime _startTime = new DateTime(2010, 01, 01, 10, 0, 0);

        private const decimal Neutral = 0.0m;
        private const decimal MinimumHappiness = -100.0m;
        private const decimal MaximumHappiness = 100.0m;

        [SetUp]
        public void Setup()
        {
            DateTimeProvider.Instance = new MockDateTimeProvider(_startTime);
        }

        [TearDown]
        public void TearDown()
        {
            DateTimeProvider.ResetInstance();
        }

        [Test]
        public void A_newly_adopted_mouse_starts_with_a_neutral_happiness_value()
        {
            var userId = "user_2_happiness_tests";
            var mouseId = string.Format("{0}|mouse_id_1", userId);
            var mouse = new Mouse(mouseId, userId);

            var happiness = mouse.GetHappiness();

            Assert.AreEqual(Neutral, happiness);
        }

        [Test]
        public void Petting_a_mouse_makes_the_mouse_happier()
        {
            var userId = "user_2_happiness_tests";
            var mouseId = string.Format("{0}|mouse_id_3", userId);
            var mouse = new Mouse(mouseId, userId);
            
            mouse.Pet();

            var happiness = mouse.GetHappiness();

            Assert.AreNotEqual(Neutral, happiness);
            Assert.Greater(happiness, Neutral);
        }

        [Test]
        public void A_mouses_happiness_decreases_over_time()
        {
            var userId = "user_2_happiness_tests";
            var mouseId = string.Format("{0}|mouse_id_4", userId);
            var mouse = new Mouse(mouseId, userId);

            var initialHappiness = mouse.GetHappiness();
            Assert.AreEqual(Neutral, initialHappiness);
            
            var thirtyMinutesLater = _startTime.AddMinutes(30);
            DateTimeProvider.Instance = new MockDateTimeProvider(thirtyMinutesLater);

            var happinessAfterThirtyMinutes = mouse.GetHappiness();
            Assert.AreNotEqual(Neutral, happinessAfterThirtyMinutes);
            Assert.Less(happinessAfterThirtyMinutes, Neutral);
        }

        [Test]
        public void A_mouses_happiness_cannot_decrease_below_the_minimum_amount()
        {
            var userId = "user_2_happiness_tests";
            var mouseId = string.Format("{0}|mouse_id_5", userId);
            var mouse = new Mouse(mouseId, userId);

            var sixMonthsLater = _startTime.AddMonths(6);
            DateTimeProvider.Instance = new MockDateTimeProvider(sixMonthsLater);

            var happinessAfterSixMonths = mouse.GetHappiness();
            Assert.AreEqual(MinimumHappiness, happinessAfterSixMonths);
        }

        [Test]
        public void A_mouses_happiness_cannot_exceed_the_maximum_amount()
        {
            var userId = "user_2_happiness_tests";
            var mouseId = string.Format("{0}|mouse_id_6", userId);
            var mouse = new Mouse(mouseId, userId);

            // pet the mouse 20 times
            for (var i = 0; i < 20; i++)
            {
                mouse.Pet();
            }

            var happiness = mouse.GetHappiness();
            Assert.AreEqual(MaximumHappiness, happiness);
        }
    }
}