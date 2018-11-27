using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PetStore;
using PetStore.Repository;

namespace Tests
{
    [TestFixture]
    public class PetControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PetControllerAddsPet() {
            const int petId = 100;
            var expected = new Pet(petId, "Zoey", "Cat");
            var mockRepo = new Mock<IPetRepository>();
            mockRepo.Setup(r => r.GetPet(petId)).Returns(expected);
            var controller = new PetController(mockRepo.Object);

            var postResult = controller.Post(expected);
            Assert.IsInstanceOf<OkObjectResult>(postResult);

            var getResult = controller.Get(expected.Id);
            Assert.IsInstanceOf<OkObjectResult>(getResult);
            var okResult = (OkObjectResult)getResult;

            var pet = (Pet)okResult.Value;
            Assert.AreEqual(expected.Id, pet.Id);
            Assert.AreEqual(expected.Name, pet.Name);
            Assert.AreEqual(expected.Type, pet.Type);
        }

        [Test]
        public void PetControllerDeletesPet() {
            const int petId = 100;
            var mockRepo = new Mock<IPetRepository>();
            mockRepo.Setup(r => r.DeletePet(It.IsAny<int>())).Returns(true);

            var controller = new PetController(mockRepo.Object);
            var deleteResult = controller.Delete(petId);

            Assert.IsInstanceOf<OkResult>(deleteResult);
        }

        [Test]
        public void PetControllerDoesNotDeletesPetThatIsNotFound()
        {
            const int petId = 100;
            var mockRepo = new Mock<IPetRepository>();
            mockRepo.Setup(r => r.DeletePet(It.IsAny<int>())).Returns(false);

            var controller = new PetController(mockRepo.Object);
            var deleteResult = controller.Delete(petId);

            Assert.IsInstanceOf<NotFoundResult>(deleteResult);
        }

        [Test]
        public void PetControllerReturnsPet() {
            const int petId = 1;
            var expected = new Pet(petId, "Zoey", "Cat");

            var mockRepo = new Mock<IPetRepository>();
            mockRepo.Setup(r => r.GetPet(petId)).Returns(expected);

            var controller = new PetController(mockRepo.Object);

            var result = controller.Get(expected.Id);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = (OkObjectResult)result;
            var pet = (Pet)okResult.Value;

            Assert.AreEqual(expected.Id, pet.Id);
            Assert.AreEqual(expected.Name, pet.Name);
            Assert.AreEqual(expected.Type, pet.Type);
        }

        [Test]
        public void PetControllerReturnsAllPets() {
            var allPets = new List<Pet> {
                new Pet(1, "Garfield", "Cat"),
                new Pet(2, "Odie", "Dog")
            };

            var mockRepo = new Mock<IPetRepository>();
            mockRepo.Setup(r => r.GetAllPets()).Returns(allPets);

            var controller = new PetController(mockRepo.Object);
            var result = controller.Get();

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult) result;

            var expected = new OkObjectResult(allPets);

            Assert.AreEqual(okResult.Value, expected.Value);
        }
    }
}