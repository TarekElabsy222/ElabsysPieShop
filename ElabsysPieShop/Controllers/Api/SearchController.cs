using ElabsysPieShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElabsysPieShop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IPieRepository _pieRepository;

        public SearchController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var allPies = _pieRepository.AllPies;
            return Ok(allPies);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if(!_pieRepository.AllPies.Any(p=>p.PieId == id))
                return NotFound();
            return Ok(_pieRepository.AllPies.Where(p=>p.PieId == id));
        }
        [HttpPost]
        public IActionResult SearchPies([FromBody] string searchQuery)
        {
             IEnumerable<Pie> pies = new List<Pie>();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                pies = _pieRepository.SearchPies(searchQuery);
            }
            return new JsonResult(pies);
        }
    }
}
