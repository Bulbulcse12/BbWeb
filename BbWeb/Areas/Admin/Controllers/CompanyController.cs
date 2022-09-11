using BbWeb.Data;
using BbWeb.Models;
using BbWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BbWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CompanyController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var posts = _db.Companies.ToList();
            return View(posts);
        }

        // Create company

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _db.Companies.Add(company);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }


        // company edit
       

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _db.Companies.FirstOrDefault(c => c.Id == id);
            if(post == null)
            {
                return NotFound();
            }
            return View("Create", post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Company company)
        {

      
            if (ModelState.IsValid)
            {
                _db.Companies.Update(company);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }

        // delete post

        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var post = _db.Companies.FirstOrDefault(c => c.Id == id);
            if(post == null)
            {
                return NotFound();
            }

            _db.Companies.Remove(post);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
