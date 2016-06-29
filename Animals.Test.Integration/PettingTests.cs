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
    public class PettingTests
    {
        private const decimal Neutral = 0.0m;

        private readonly HttpService _httpService;
        private readonly DateTime _startTime = new DateTime(2010, 01, 01, 10, 0, 0);

        public PettingTests()
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
        public void A_newly_adopted_animal_starts_with_a_neutral_happiness_level()
        {
            var userId = "user_4_petting_animal";
            var animalId = string.Format("{0}|mouse_1", userId);

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

            Assert.AreEqual(Neutral, animal.Happiness);
        }

        [Test]
        public void Petting_an_animal_makes_the_animal_happier()
        {
            var userId = "user_4_petting_animal";
            var animalId = string.Format("{0}|mouse_2", userId);

            AdoptMouse(userId, animalId);

            var url = string.Format("http://localhost/v1/animal/{0}/pet", animalId);
            var response = _httpService.Put(
                url,
                new
                {
                    UserId = userId,
                    AnimalId = animalId
                });
            
            Assert.True(response.IsSuccessStatusCode);

            var animal = GetAnimal(animalId);

            Assert.Greater(animal.Happiness, Neutral);
        }

        [Test]
        public void An_animals_happiness_decreases_over_time()
        {
            var userId = "user_4_petting_animal";
            var animalId = string.Format("{0}|mouse_3", userId);

            AdoptMouse(userId, animalId);

            var thirtyMinutesLater = _startTime.AddMinutes(30);
            DateTimeProvider.Instance = new MockDateTimeProvider(thirtyMinutesLater);

            var animal = GetAnimal(animalId);

            Assert.Less(animal.Happiness, Neutral);
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
            var adoptMouseUrl = string.Format("http://localhost/v1/user/{0}/adopt/mouse", userId);
            var response = _httpService.Put(
                adoptMouseUrl,
                new
                {
                    UserId = userId,
                    AnimalId = animalId
                });

            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}