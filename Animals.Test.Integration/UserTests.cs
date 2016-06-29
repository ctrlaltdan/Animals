using System.Linq;
using System.Net.Http;
using Animals.Api;
using Animals.Contract;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace Animals.Test.Integration
{
    [TestFixture]
    public class UserTests
    {
        private readonly HttpService _httpService;

        public UserTests()
        {
            _httpService = new HttpService(TestServer.Create<Startup>().HttpClient);
        }

        [Test]
        public void A_user_starts_with_no_animals()
        {
            var userId = "user_1_no_animals";

            var url = string.Format("http://localhost/v1/user/{0}", userId);

            var response = _httpService.Get(url);

            Assert.True(response.IsSuccessStatusCode);

            var user = response.Content.ReadAsAsync<UserDtoV1>().Result;
            
            Assert.IsEmpty(user.AnimalIds);
        }
    }
}