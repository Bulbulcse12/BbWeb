using BbWeb.Data;
using BbWeb.Models;
using BbWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BbWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class TagController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TagController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var tag = _db.Tags.ToList();
            return View(tag);
        }

        // Create tag

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _db.Tags.Add(tag);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // Tag update
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var tag = _db.Tags.FirstOrDefault(c => c.Id == id);
            if(tag == null)
            {
                return NotFound();
            }

            return View("Create", tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _db.Tags.Update(tag);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(tag);
        }


        // Delete tag
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var tag = _db.Tags.FirstOrDefault(c => c.Id == id);
            if (tag == null)
            {
                return NotFound();
            }
            _db.Tags.Remove(tag);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
