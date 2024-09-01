
using Microsoft.EntityFrameworkCore;

namespace ElabsysPieShop.Models
{
	public class PieRepository : IPieRepository
	{
		private readonly ElabsysPieShopDbContext _context;

        public PieRepository(ElabsysPieShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Pie> AllPies
		{
			get
			{
				return _context.Pies.Include(c => c.Category);
			}
		}
			

		public IEnumerable<Pie> PiesOfTheWeek
		{
			get { return AllPies.Where(p => p.IsPieOfWeek);}
		}

		public Pie? GetPiebyId(int pieId) => AllPies.FirstOrDefault(p=>p.PieId == pieId);
		
		public IEnumerable<Pie> SearchPies(string searchQuary)
		{
			return _context.Pies.Where(p=>p.Name.Contains(searchQuary));
		}
	}
}
