using FindMyCat.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace FindMyCat.Services
{
	public class PetService : IPetService
	{
        /// <summary>
        /// Get pet of owner grouped by owner's gender
        /// </summary>
        /// <param name="petType">cat or dog</param>
        /// <returns></returns>
        public async Task<List<OwnerGenderPet>> GetOwnerGenderPet(string petType)
        {
            var petsList = await this.GetPetOwners();

            var filteredPetList = petsList
                        .Where(o => o.Pets != null) // find owner with pets
                        .GroupBy(o => o.Gender, o => o.Pets.Where(p => p.Type.Equals(petType)),
                                    (g, ps) => new { Gender = g, Pets = ps.SelectMany(x => x) }) // groups owner gender and cats
                        .Select(g => new OwnerGenderPet
                        {
                            Gender = g.Gender,
                            Pets = g.Pets.OrderBy(p => p.Name).ToList()
                        })
                        .ToList(); // select grouped gender->cats name ordered asc

            return filteredPetList;
        }

        /// <summary>
        /// Get Original people.json and convert to List of PetOwner
        /// </summary>
        /// <returns>list of PetOwner</returns>
        public async Task<List<PetOwner>> GetPetOwners()
        {
            List<PetOwner> petOwners;

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://agl-developer-test.azurewebsites.net");

            try
            {
                var response = await client.GetStringAsync("people.json");

                petOwners = JsonConvert.DeserializeObject<List<PetOwner>>(response);
            }
            catch(Exception ex)
            {
                petOwners = null;
            }

            return petOwners;

        }
	}
}