namespace ElabsysPieShop.Models
{
	public interface ICategoryRepository
	{
		IEnumerable<Category> AllCategories { get; }
	}
}
