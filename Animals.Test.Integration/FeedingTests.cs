using System;
using System.Net.Http;
using Animals.Api;
using Animals.Contract;
using Animals.Domain;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace Animals.Test.Integration
{
    [TestFixture]
    public class FeedingTests
    {
        private const decimal Neutral = 0.0m;

        private readonly HttpService _httpService;
        private readonly DateTime _startTime = new DateTime(2010, 01, 01, 10, 0, 0);

        public FeedingTests()
        {
            _httpService = new HttpService(TestServer.Create<Startup>().HttpClient);
        }

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
        public void A_newly_adopted_animal_starts_with_a_neutral_fullness_level()
        {
            var userId = "user_2_adopt_cat";
            var animalId = string.Format("{0}|mouse_3", userId);

            var url = string.Format("http://localhost/v1/user/{0}/adopt/mouse", userId);

            var response = _httpService.Put(
                url,
                new
                {
                    UserId = userId,
                    AnimalId = animalId
                });

            Assert.IsTrue(response.IsSuccessStatusCode);

            var animal = GetAnimal(animalId);

            Assert.AreEqual(Neutral, animal.Fullness);
        }

        [Test]
        public void Feeding_an_animal_decreases_its_hunger_level()
        {
            var userId = "user_3_feed_animal";
            var animalId = string.Format("{0}|mouse_1", userId);

            AdoptMouse(userId, animalId);

            var url = string.Format("http://localhost/v1/animal/{0}/feed/tuna", animalId);
            var response = _httpService.Put(
                url,
                new
                {
                    AnimalId = animalId
                });
            
            Assert.True(response.IsSuccessStatusCode);

            var animal = GetAnimal(animalId);

            Assert.Greater(animal.Fullness, Neutral);
        }

        [TestCase("tuna")]
        [TestCase("cheese")]
        public void Feeding_an_animal_x_food_decreases_its_hunger_level(string foodType)
        {
            var userId = "user_5_food_tests";
            var animalId = string.Format("{0}|cat_{1}", userId, foodType);

            AdoptMouse(userId, animalId);

            var url = string.Format("http://localhost/v1/animal/{0}/feed/{1}", animalId, foodType);
            var response = _httpService.Put(
                url,
                new
                {
                    UserId = userId,
                    AnimalId = animalId
                });

            Assert.IsTrue(response.IsSuccessStatusCode);

            var animal = GetAnimal(animalId);

            Assert.Greater(animal.Fullness, Neutral);
        }

        [Test]
        public void An_animals_hunger_increases_over_time()
        {
            var userId = "user_3_feed_animal";
            var animalId = string.Format("{0}|mouse_2", userId);

            AdoptMouse(userId, animalId);

            var thirtyMinutesLater = _startTime.AddMinutes(30);
            DateTimeProvider.Instance = new MockDateTimeProvider(thirtyMinutesLater);

            var animal = GetAnimal(animalId);

            Assert.Less(animal.Fullness, Neutral);
        }

        private AnimalDtoV1 GetAnimal(string animalId)
        {
            var url = string.Format("http://localhost/v1/animal/{0}", animalId);

            var response = _httpService.Get(url);

            Assert.IsTrue(response.IsSuccessStatusCode);

            return response.Content.ReadAsAsync<AnimalDtoV1>().Result;
        }

        private void AdoptMouse(string userId, string animalId)
        {
            var url = string.Format("http://localhost/v1/user/{0}/adopt/mouse", userId);
            var response = _httpService.Put(
                url,
                new
                {
                    UserId = userId,
                    AnimalId = animalId
                });

            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}