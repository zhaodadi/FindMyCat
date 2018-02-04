using FindMyCat.Caching;
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
        private readonly ICache _petCache;
        private readonly string PET_CACHE_KEY = "PetDataCache";

        public PetService()
        {
            _petCache = new SystemCache();
        }

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
            if (_petCache.Contains(PET_CACHE_KEY))
            {
                //Log.Info("EWINERY DEBUG : returning feed from cache " + feedUrl, this);
                return _petCache.Get<List<PetOwner>>(PET_CACHE_KEY);
            }

            // no cache set
            List<PetOwner> petOwners =  JsonConvert.DeserializeObject<List<PetOwner>>(await this.GetPetData());
            _petCache.Set(PET_CACHE_KEY, petOwners, 15);

            return await GetPetOwners();

        }

        /// <summary>
        /// Get source data, ToDo: move to a seprate class
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetPetData()
        {
            string response;

            HttpClient client = new HttpClient();

            // ToDo: move source url to config
            client.BaseAddress = new Uri("http://agl-developer-test.azurewebsites.net");

            try
            {
                response = await client.GetStringAsync("people.json");
            }
            catch (Exception ex)
            {
                response = string.Empty;
            }

            return response;
        }
    }
}