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
        public static string servicePort = "80";
        public static string serviceHost;

        [SetUp]
        public void Setup()
        {
            servicePort = System.Environment.GetEnvironmentVariable("EXTERNAL_PORT") ?? "80";
            serviceHost = $"http://localhost:{servicePort}";
            System.Console.WriteLine($"Test Address: {serviceHost}");
        }

        [Test]
        public static void TestPet1()
        {
            string expected = "{\"id\":1,\"name\":\"Zoey\",\"type\":\"Cat\"}";
            string responseString = "";
            using (var client = new HttpClient())
            {
                var serviceURI = $"{serviceHost}/pets/1";
                var response = client.GetAsync(serviceURI).Result;

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
