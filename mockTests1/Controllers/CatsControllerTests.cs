using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mock.depart.Controllers;
using mock.depart.Models;
using mock.depart.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mock.depart.Controllers.Tests
{


    [TestClass()]
    public class CatsControllerTests
    {

        [TestMethod()]
        public void Delete_CatNotFoundTest()
        {
            Mock<CatsService> catsServiceMock = new Mock<CatsService>();
            Mock<CatsController> catsControllerMock = new Mock<CatsController>(catsServiceMock.Object) { CallBase = true };

            catsServiceMock.Setup(s => s.Get(It.IsAny<int>())).Returns(value: null);
            catsControllerMock.Setup(c => c.UserId).Returns("1");

            var actionResult = catsControllerMock.Object.DeleteCat(0);

            var result = actionResult.Result as NotFoundResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Delete_CatIsNotYoursTest()
        {
            Mock<CatsService> catsServiceMock = new Mock<CatsService>();
            Mock<CatsController> catsControllerMock = new Mock<CatsController>(catsServiceMock.Object) { CallBase = true };

            CatOwner co = new CatOwner()
            {
                Id = "11111"
            };
            Cat c = new Cat()
            {
                Id = 1,
                Name = "Loki",
                CuteLevel = Cuteness.Amazing,
                CatOwner = co
            };

            catsServiceMock.Setup(s => s.Get(It.IsAny<int>())).Returns(c);
            catsControllerMock.Setup(c => c.UserId).Returns("1");

            var actionResult = catsControllerMock.Object.DeleteCat(0);

            var result = actionResult.Result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Cat is not yours", result.Value);
        }

        [TestMethod()]
        public void Delete_CatCutnessBarelyOkTest()
        {
            Mock<CatsService> catsServiceMock = new Mock<CatsService>();
            Mock<CatsController> catsControllerMock = new Mock<CatsController>(catsServiceMock.Object) { CallBase = true };

            CatOwner co = new CatOwner()
            {
                Id = "11111"
            };
            Cat c = new Cat()
            {
                Id = 1,
                Name = "Garfield",
                CuteLevel = Cuteness.BarelyOk,
                CatOwner = co
            };

            catsServiceMock.Setup(s => s.Get(It.IsAny<int>())).Returns(c);
            catsServiceMock.Setup(s => s.Delete(It.IsAny<int>())).Returns(c);
            catsControllerMock.Setup(c => c.UserId).Returns("11111");

            var actionResult = catsControllerMock.Object.DeleteCat(0);

            var result = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(result);

            // Vérification que l'objet retourné est le bon chat
            Cat? catResult = (Cat?)result!.Value;
            Assert.AreEqual(c.Id, catResult!.Id); // Vérifie que l'ID du chat est correct
        }


        [TestMethod()]
        public void Delete_CatIsTooCute()
        {
            Mock<CatsService> catsServiceMock = new Mock<CatsService>();
            Mock<CatsController> catsControllerMock = new Mock<CatsController>(catsServiceMock.Object) { CallBase = true };

            CatOwner co = new CatOwner()
            {
                Id = "11111"
            };
            Cat c = new Cat()
            {
                Id = 1,
                Name = "Loki",
                CuteLevel = Cuteness.Amazing,
                CatOwner = co
            };

            catsServiceMock.Setup(s => s.Get(It.IsAny<int>())).Returns(c);
            catsControllerMock.Setup(c => c.UserId).Returns("11111");

            var actionResult = catsControllerMock.Object.DeleteCat(0);

            var result = actionResult.Result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Cat is too cute", result.Value);
        }

    }
}