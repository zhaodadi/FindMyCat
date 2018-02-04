using FindMyCat.Models;
using FindMyCat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
		public async Task<ActionResult> Index()
		{
            var petsList = await _petService.GetOwnerGenderPet(PetTypes.CAT);


            return View(petsList);
		}
	}
}