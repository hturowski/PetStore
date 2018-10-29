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
        public void PetControllerAddsZoey()
        {
            Pet expected = new Pet(100, "Feynman", "Dog");
            var controller = new PetController();
            var postResult = controller.Post(expected);
            Assert.IsInstanceOf<OkObjectResult>(postResult);
            var okResult = (OkObjectResult)postResult;

            var getResult = controller.Get(expected.Id);
            Assert.IsInstanceOf<OkObjectResult>(getResult);
            okResult = (OkObjectResult)getResult;

            var pet = (Pet)okResult.Value;
            Assert.AreEqual(expected.Id, pet.Id);
            Assert.AreEqual(expected.Name, pet.Name);
            Assert.AreEqual(expected.Type, pet.Type);
        }

        [Test]
        public void PetControllerDeletesZoey()
        {
            Pet expected = new Pet(100, "Feynman", "Dog");
            var controller = new PetController();
            var deleteResult = controller.Delete(expected.Id);
            Assert.IsInstanceOf<OkResult>(deleteResult);
        }

        [Test]
        public void PetControllerReturnsZoey()
        {
            Pet expected = new Pet(1, "Zoey", "Cat");
            var controller = new PetController();
            var result = controller.Get(expected.Id);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            var pet = (Pet)okResult.Value;
            Assert.AreEqual(expected.Id, pet.Id);
            Assert.AreEqual(expected.Name, pet.Name);
            Assert.AreEqual(expected.Type, pet.Type);
        }

    }
}