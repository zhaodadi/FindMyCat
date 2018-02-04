using FindMyCat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyCat.Services
{
	public interface IPetService
    {
        Task<List<OwnerGenderPet>> GetOwnerGenderPet(string petType);

        Task<List<PetOwner>> GetPetOwners();
	}
}