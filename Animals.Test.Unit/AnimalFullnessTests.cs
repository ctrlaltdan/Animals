using System;
using Animals.Domain;
using Animals.Domain.Animals;
using Animals.Domain.Foods;
using NUnit.Framework;

namespace Animals.Test.Unit
{
    [TestFixture]
    public class AnimalFullnessTests
    {
        private readonly DateTime _startTime = new DateTime(2010, 01, 01, 10, 0, 0);

        private const decimal Neutral = 0.0m;
        private const decimal MinimumFullness = -100.0m;
        private const decimal MaximumFullness = 100.0m;

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
        public void A_newly_adopted_mouse_starts_with_a_neutral_fullness_value()
        {
            var userId = "user_1_fullness_tests";
            var mouseId = string.Format("{0}|mouse_id_1", userId);
            var mouse = new Mouse(mouseId, userId);

            var fullness = mouse.GetFullness();

            Assert.AreEqual(Neutral, fullness);
        }

        [Test]
        public void Feeding_a_mouse_makes_the_mouse_less_hungry()
        {
            var userId = "user_1_fullness_tests";
            var mouseId = string.Format("{0}|mouse_id_2", userId);
            var mouse = new Mouse(mouseId, userId);

            var cheese = new Cheese();

            mouse.Feed(cheese);

            var fullness = mouse.GetFullness();

            Assert.AreNotEqual(Neutral, fullness);
            Assert.Greater(fullness, Neutral);
        }

        [Test]
        public void A_mouses_fullness_decreases_over_time()
        {
            var userId = "user_1_fullness_tests";
            var mouseId = string.Format("{0}|mouse_id_4", userId);
            var mouse = new Mouse(mouseId, userId);

            var initialFullness = mouse.GetFullness();
            Assert.AreEqual(Neutral, initialFullness);
            
            var thirtyMinutesLater = _startTime.AddMinutes(30);
            DateTimeProvider.Instance = new MockDateTimeProvider(thirtyMinutesLater);

            var fullnessAfterThirtyMinutes = mouse.GetFullness();
            Assert.AreNotEqual(Neutral, fullnessAfterThirtyMinutes);
            Assert.Less(fullnessAfterThirtyMinutes, Neutral);
        }

        [Test]
        public void A_mouses_fullness_cannot_decrease_below_the_minimum_amount()
        {
            var userId = "user_1_fullness_tests";
            var mouseId = string.Format("{0}|mouse_id_5", userId);
            var mouse = new Mouse(mouseId, userId);

            var sixMonthsLater = _startTime.AddMonths(6);
            DateTimeProvider.Instance = new MockDateTimeProvider(sixMonthsLater);

            var fullnessAfterSixMonths = mouse.GetFullness();
            Assert.AreEqual(MinimumFullness, fullnessAfterSixMonths);
        }

        [Test]
        public void A_mouses_fullness_cannot_exceed_the_maximum_amount()
        {
            var userId = "user_1_fullness_tests";
            var mouseId = string.Format("{0}|mouse_id_5", userId);
            var mouse = new Mouse(mouseId, userId);

            // feed the mouse 20 cheeses
            for (var i = 0; i < 20; i++)
            {
                mouse.Feed(new Cheese());
            }

            var fullness = mouse.GetFullness();
            Assert.AreEqual(MaximumFullness, fullness);
        }

        [Test]
        public void When_feeding_a_mouse_the_fullness_is_recalculated_before_applying_food()
        {
            var userId = "user_1_fullness_tests";
            var mouseId = string.Format("{0}|mouse_id_6", userId);
            var mouse = new Mouse(mouseId, userId);

            // feed the mouse 20 cheeses
            for (var i = 0; i < 20; i++)
            {
                mouse.Feed(new Cheese());
            }

            // the fullness should now be at its maximum
            var fullnessAfterInitialFeed = mouse.GetFullness();
            Assert.AreEqual(MaximumFullness, fullnessAfterInitialFeed);

            // the fullness should now be at its minimum
            var sixMonthsLater = _startTime.AddMonths(6);
            DateTimeProvider.Instance = new MockDateTimeProvider(sixMonthsLater);
            var fullnessAfterSixMonths = mouse.GetFullness();
            Assert.AreEqual(MinimumFullness, fullnessAfterSixMonths);

            // feed the mouse again to check their fullness has risen above the minimum level
            mouse.Feed(new Cheese());

            var fullnessAfterSecondFeed = mouse.GetFullness();
            Assert.Greater(fullnessAfterSecondFeed, fullnessAfterSixMonths);
        }
    }
}