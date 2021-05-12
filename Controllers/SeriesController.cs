using System.Threading.Tasks;
using IMDB.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcSeries.Data;
using MvcSeries.Models;
using System.Linq;

namespace IMDB.Controllers
{
    public class SeriesController : Controller
    {
        private readonly MvcSeriesContext _context;

        public SeriesController(MvcSeriesContext context)
        {
            _context = context;
        }

        // GET: Series
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var series = from s in _context.Series
                         select s;

            int pageSize = 16;
            //return View(await _context.Series.ToListAsync());
             return View(await PaginatedList<Series>.CreateAsync(series.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Series/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series
                .FirstOrDefaultAsync(m => m.Id == id);
            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }
    }
}
