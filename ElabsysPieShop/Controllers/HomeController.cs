using ElabsysPieShop.Models;
using ElabsysPieShop.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ElabsysPieShop.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPieRepository _pieRepository;

		public HomeController(IPieRepository pieRepository)
		{
			_pieRepository = pieRepository;
		}

		public IActionResult Index()
		{
			var piesOfWeak = _pieRepository.PiesOfTheWeek;
			var homeViewModel = new HomeViewModel(piesOfWeak);
			return View(homeViewModel);
		}
	}
}
