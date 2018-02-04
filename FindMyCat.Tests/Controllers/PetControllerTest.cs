using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using FindMyCat.Controllers;
using FindMyCat.Models;
using FindMyCat.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FindMyCat.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        private static PetService _petService;

        [ClassInitialize]
        public static void InitializeClass(TestContext ctx)
        {
            _petService = new PetService();
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            PetController controller = new PetController(_petService);

            // Act
            ViewResult result = controller.Index().Result as ViewResult;

            // Assert
            Assert.IsNotNull(result);

            Assert.AreSame(result.Model.GetType(), typeof(List<OwnerGenderPet>));
        }
    }
}
