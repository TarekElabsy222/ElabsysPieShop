namespace ElabsysPieShop.Models
{
	public interface IPieRepository
	{
		IEnumerable<Pie> AllPies { get; }
		IEnumerable<Pie> PiesOfTheWeek { get; }
		Pie? GetPiebyId(int pieId);
		IEnumerable<Pie> SearchPies(string searchQuery);
	}
}
