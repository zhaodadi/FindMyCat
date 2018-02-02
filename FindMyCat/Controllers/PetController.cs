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
			_petService.DoNothing();
			return View();
		}
	}
}