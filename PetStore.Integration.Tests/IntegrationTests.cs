using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using PetStore;


namespace Tests
{
    public class Tests
    {
        private static readonly HttpClient client = new HttpClient();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public static void TestPet1()
        {
            string expected = "{\"id\":1,\"name\":\"Zoey\",\"type\":\"Cat\"}";
            string responseString = "";
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://localhost/pet/1").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    responseString = responseContent.ReadAsStringAsync().Result;
                }
            }
            Assert.AreEqual(expected, responseString);
        }

    }
}
