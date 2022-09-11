using BbWeb.Data;
using BbWeb.Interfaces.Manager;
using BbWeb.Models;
using BbWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BbWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        // private readonly ApplicationDbContext _db;
        ICategoryManager _categoryManager;
        //public CategoryController(ApplicationDbContext db,ICategoryManager categoryManager)
        //{
        //    _db = db;
        //    _categoryManager = categoryManager;
        //}
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        public IActionResult Index()
        {
            // var post = _db.Categories.ToList();
            var post = _categoryManager.GetAll().ToList();
            return View(post);
        }

        // category create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                //_db.Categories.Add(category);
                //_db.SaveChanges();
               bool isSaved = _categoryManager.Add(category);
                if (isSaved)
                {
                    return RedirectToAction(nameof(Index));
                }
               
               
            }
            return View(category);
        }

        // category edit

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            // var post = _db.Categories.FirstOrDefault(c => c.Id == id);
            var post = _categoryManager.GetFirstOrDefault(c => c.Id == id);
            if(post == null)
            {
                return NotFound();
            }

            return View("Create",post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                // _db.Categories.Update(category);
             bool isSaved = _categoryManager.Update(category);
                if (isSaved)
                {
                    return RedirectToAction(nameof(Index));
                }
                
               
            }

            return View(category);
        }

        // Category delete

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //  var post = _db.Categories.FirstOrDefault(c => c.Id == id);
            var post = _categoryManager.GetFirstOrDefault(c => c.Id == id);
            if (post != null)
            {
                // _db.Categories.Remove(post);
               bool isSaved =_categoryManager.Delete(post);
                if (isSaved)
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
