using ElabsysPieShop.Models;
using ElabsysPieShop.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ElabsysPieShop.Controllers
{
	public class PieController : Controller
	{
		private readonly IPieRepository _pieRepository;
		private readonly ICategoryRepository _categoryRepository;

		public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
		{
			_pieRepository = pieRepository;
			_categoryRepository = categoryRepository;
		}

		/*public IActionResult Index()
		{
			PieListViewModel pieListViewModel = new PieListViewModel(_pieRepository.AllPies,"All Pies");
			return View(pieListViewModel);
		}*/
		public ViewResult Index(string category)
		{
			IEnumerable<Pie> pies;
			string? currentCategory;
			if (string.IsNullOrEmpty(category))
			{
				pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
				currentCategory = "All pies";
			}
			else
			{
				pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
					.OrderBy(p => p.PieId);
				currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
			}
			return View(new PieListViewModel(pies, currentCategory));
		}
		public IActionResult Details(int id)
		{
			var pie = _pieRepository.GetPiebyId(id);
			if (pie == null) 
				return NotFound();
			return View(pie);
		}
		public IActionResult Search()
		{
			return View();
		}
	}
}
