using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using PetStore;

namespace Tests
{
    public class PetControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PetControllerReturnsFeynman()
        {
            Pet expected = new Pet(1, "Feynman", "Dog");
            var controller = new PetController();
            var result = controller.Get(expected.Id);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            var pet = (Pet)okResult.Value;
            Assert.AreEqual(expected.Id, pet.Id);
            Assert.AreEqual(expected.Name, pet.Name);
            Assert.AreEqual(expected.Type, pet.Type);
        }

        [Test]
        public void PetControllerAllowsPut()
        {
            var controller = new PetController();
            var result = controller.Put();
            Assert.Pass();
        }
    }
}