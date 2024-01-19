using Edukate.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Edukate.Controllers
{
    public class HomeController : Controller
    {
        EdukateDbContext _context { get; }

        public HomeController(EdukateDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.Instructors.ToListAsync();

            return View(data);
        }
    }
}