using FindMyCat.Models;
using FindMyCat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindMyCat.Controllers
{
	public class PetController : Controller
	{
		private readonly IPetService _petService;

		public PetController(IPetService petService)
		{
			_petService = petService;
		}

		// GET: Pet
		public ActionResult Index()
		{
            var petsList = _petService.GetPetOwners()
                        .Where(o => o.Pets != null) // find owner with pets
                        .GroupBy(o => o.Gender, o => o.Pets.Where(p => p.Type.Equals("Cat")),
                                    (g, ps) => new { Gender = g, Pets = ps.SelectMany(x => x) }) // groups owner gender and cats
                        .Select(g => new OwnerGenderPet
                        {
                            Gender = g.Gender,
                            Pets = g.Pets.OrderBy(p => p.Name).ToList()
                        }); // select grouped gender->cats name ordered asc

			return View(petsList);
		}
	}
}