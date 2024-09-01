
namespace ElabsysPieShop.Models
{
	public class CategoryRepository : ICategoryRepository
	{
        private readonly ElabsysPieShopDbContext _context;

        public CategoryRepository(ElabsysPieShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> AllCategories =>
            _context.Categories.OrderBy(p=>p.CategoryName);
		
			
	}
}
