using System;
using System.Threading.Tasks;
using FindMyCat.Models;
using FindMyCat.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FindMyCat.Tests.Services
{
    [TestClass]
    public class PetServiceTest
    {
        private static PetService _petService;

        [ClassInitialize]
        public static void InitializeClass(TestContext ctx)
        {
            _petService = new PetService();
        }


        [TestMethod]
        public async Task GetPetOwnersTest()
        {
            var petList = await _petService.GetPetOwners();
        
            Assert.IsNotNull(petList, "External data source is down?");
            Assert.IsTrue(petList.Count > 0, "External data source is invalid?");
        }


        [TestMethod]
        public async Task GetOwnerGenderCatTest()
        {
            var petOwnerList = await _petService.GetOwnerGenderPet(PetTypes.CAT);

            Assert.IsTrue(petOwnerList.Count == 2, "More than two gender??");

            Assert.IsTrue(petOwnerList[0].Gender.Equals(Gender.MALE));
            Assert.IsTrue(petOwnerList[1].Gender.Equals(Gender.FEMALE));

            foreach (var owner in petOwnerList)
            {
                foreach (var pet in owner.Pets)
                {
                    Assert.AreEqual(pet.Type, PetTypes.CAT);
                }
            }
        }


        [TestMethod]
        public async Task GetOwnerGenderDogTest()
        {
            var petOwnerList = await _petService.GetOwnerGenderPet(PetTypes.DOG);

            Assert.IsTrue(petOwnerList.Count == 2, "More than two gender??");

            Assert.IsTrue(petOwnerList[0].Gender.Equals(Gender.MALE));
            Assert.IsTrue(petOwnerList[1].Gender.Equals(Gender.FEMALE));

            foreach (var owner in petOwnerList)
            {
                foreach (var pet in owner.Pets)
                {
                    Assert.AreEqual(pet.Type, PetTypes.DOG);
                }
            }
        }
    }
}
