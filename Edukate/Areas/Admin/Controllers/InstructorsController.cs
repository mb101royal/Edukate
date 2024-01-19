using Edukate.Contexts;
using Edukate.Helpers;
using Edukate.Models;
using Edukate.ViewModels.InstructorsViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Edukate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstructorsController : Controller
    {
        EdukateDbContext _context { get; }

        public InstructorsController(EdukateDbContext context)
        {
            _context = context;
        }

        // Index

        public async Task<IActionResult> Index()
        {
            var instructorsFroMDb = await _context.Instructors.Select(t => new InstructorsDetailsViewModel {
                Id = t.Id,
                Name = t.Name,
                Profession = t.Profession,
                ImageUrl = t.ImageUrl,
            }).ToListAsync();

            return View(instructorsFroMDb);
        }

        // Create

        // Get
        [HttpGet]
        public IActionResult Create()
            => View();

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(InstructorCreateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (!vm.ImageFile.IsValidSize(300)) return View(vm);

            if (!vm.ImageFile.IsValidType()) return View(vm);

            Instructor newInstructor = new()
            {
                Name = vm.Name,
                Profession = vm.Profession,
                ImageUrl = vm.ImageFile.SaveImageAsync(PathConstants.ImagesPath).Result,
            };

            await _context.AddAsync(newInstructor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Edit

        // Get
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.IsValidId()) return BadRequest();

            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, InstructorEditViewModel vm)
        {
            if (!id.IsValidId()) return BadRequest();

            if (!ModelState.IsValid) return View(vm);

            var instructorFromDb = await _context.Instructors.FindAsync(id);

            if (instructorFromDb == null) return NotFound();

            instructorFromDb.Name = vm.Name;
            instructorFromDb.Profession = vm.Profession;
            instructorFromDb.ImageUrl = vm.ImageFile.SaveImageAsync(PathConstants.ImagesPath).Result;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Delete

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.IsValidId()) return BadRequest();

            var instructorFromDb = await _context.Instructors.FindAsync(id);

            if (instructorFromDb == null) return NotFound();

            _context.Remove(instructorFromDb);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
