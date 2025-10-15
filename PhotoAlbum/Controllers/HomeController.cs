using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoAlbum.Data;
using PhotoAlbum.Models;
using System.Diagnostics;

namespace PhotoAlbum.Controllers
{

    // The Home Controller class
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PhotoAlbumContext _context;

        public HomeController(ILogger<HomeController> logger, PhotoAlbumContext context)
        {
            _logger = logger;
            _context = context;
            
        }

        // GET: Photos
        public async Task<IActionResult> Index()
        {
            var photos = await _context.Photo
                .OrderByDescending(m => m.CreateDate)
                .Include(p => p.Category)
                .ToListAsync();

            return View(photos);
        }

        // GET: Photos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photo
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PhotoId == id);

            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}