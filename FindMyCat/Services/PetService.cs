using FindMyCat.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace FindMyCat.Services
{
	public class PetService : IPetService
	{
		public List<PetOwner> GetPetOwners()
		{
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://agl-developer-test.azurewebsites.net");

            var data = client.GetStringAsync("people.json").Result;

            var petOwners = JsonConvert.DeserializeObject<List<PetOwner>>(data);

            return petOwners;

        }
	}
}