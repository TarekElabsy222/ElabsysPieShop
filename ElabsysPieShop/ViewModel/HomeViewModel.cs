using ElabsysPieShop.Models;

namespace ElabsysPieShop.ViewModel
{
	public class HomeViewModel
	{		
		public IEnumerable<Pie> PiesOfWeak { get; }
		public HomeViewModel(IEnumerable<Pie> piesOfWeak)
		{
			PiesOfWeak = piesOfWeak;
		}
	}
}
