using System;
using System.Linq;
using System.Net.Http;
using Animals.Api;
using Animals.Contract;
using Animals.Domain;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace Animals.Test.Integration
{
    [TestFixture]
    public class AdoptionTests
    {
        private readonly HttpService _httpService;
        private readonly DateTime _startTime = new DateTime(2010, 01, 01, 10, 0, 0);

        public AdoptionTests()
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

        [TestCase("cat")]
        [TestCase("mouse")]
        public void A_user_can_adopt_animal_of_x_types(string animalType)
        {
            var userId = "user_2_adoption";
            var animalId = string.Format("{0}|{1}_1", userId, animalType);

            var url = string.Format("http://localhost/v1/user/{0}/adopt/{1}", userId, animalType);

            var response = _httpService.Put(
                url,
                new
                {
                    UserId = userId,
                    AnimalId = animalId
                });

            Assert.IsTrue(response.IsSuccessStatusCode);

            var user = GetUser(userId);
            Assert.IsNotEmpty(user.AnimalIds);
            Assert.True(user.AnimalIds.Contains(animalId));
        }

        [Test]
        public void A_user_can_adopt_many_animals_of_different_types()
        {
            var userId = "user_2_adopt_animals";
            var catId = string.Format("{0}|cat_1", userId);
            var mouseId = string.Format("{0}|mouse_1", userId);

            var adoptCatUrl = string.Format("http://localhost/v1/user/{0}/adopt/cat", userId);
            var catAdoptionResponse = _httpService.Put(
                adoptCatUrl,
                new
                {
                    UserId = userId,
                    AnimalId = catId
                });
            
            Assert.IsTrue(catAdoptionResponse.IsSuccessStatusCode);

            var adoptmouseUrl = string.Format("http://localhost/v1/user/{0}/adopt/mouse", userId);
            var mouseAdoptionResponse = _httpService.Put(
                adoptmouseUrl,
                new
                {
                    UserId = userId,
                    AnimalId = mouseId
                });

            Assert.IsTrue(mouseAdoptionResponse.IsSuccessStatusCode);

            var user = GetUser(userId);
            Assert.IsNotEmpty(user.AnimalIds);
            Assert.AreEqual(2, user.AnimalIds.Count());
            Assert.True(user.AnimalIds.Contains(catId));
            Assert.True(user.AnimalIds.Contains(mouseId));
        }

        private UserDtoV1 GetUser(string userId)
        {
            var getUserUrl = string.Format("http://localhost/v1/user/{0}", userId);

            var response = _httpService.Get(getUserUrl);

            Assert.IsTrue(response.IsSuccessStatusCode);

            return response.Content.ReadAsAsync<UserDtoV1>().Result;
        }
    }
}