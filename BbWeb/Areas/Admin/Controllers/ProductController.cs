using BbWeb.Data;
using BbWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using BbWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BbWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _he;
        public ProductController(ApplicationDbContext db,IWebHostEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index(int pg=1,string SearchText = "")
        {
            List<Product> posts;
            SearchText = SearchText.ToLower();
            if(SearchText != "" && SearchText != null)
            {
                posts = _db.Products.Include(c => c.Category).Include(x => x.Tag).
                    Where(c => c.ProductName.ToLower().Contains(SearchText) || c.Description.ToLower().Contains(SearchText) ||
                    c.Category.CategoryName.ToLower().Contains(SearchText) || c.Tag.TagName.ToLower().Contains(SearchText)).ToList();
            }
            else
            posts = _db.Products.Include(c=>c.Category).Include(x=>x.Tag).ToList();

            //const int pageSize = 10;
            //if (pg < 1)
            //    pg = 1;

            //int recsCount = posts.Count();
            //var pager = new Pager(recsCount, pg, pageSize);
            //int recSkip = (pg - 1) * pageSize;
            //var data = posts.Skip(recSkip).Take(pager.PageSize).ToList();
            //this.ViewBag.Pager = pager; 
            return View(posts);
        }


        // create products

        public IActionResult Create()
        {
            ViewData["categoryId"] = new SelectList(_db.Categories.ToList(),"Id","CategoryName");
            ViewData["tagId"] = new SelectList(_db.Tags.ToList(), "Id", "TagName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                // check same name  product already fexist or not

                //var searchProduct = _db.Products.FirstOrDefault(c => c.ProductName == product.ProductName);
                //if(searchProduct != null)
                //{
                //    ViewBag.message = "This product is already exist";
                //    ViewData["categoryId"] = new SelectList(_db.Categories.ToList(), "Id", "CategoryName");
                //    ViewData["tagId"] = new SelectList(_db.Tags.ToList(), "Id", "TagName");
                //    return View(product);
                //}

                // if same name product not exist
                // upload image when insert file

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }
                if (image == null)
                {
                    product.Image = "Images/default.png";
                }

                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            return View(product);
        }

        // product edit

        public IActionResult Edit(int? id)
        {
            ViewData["categoryId"] = new SelectList(_db.Categories.ToList(), "Id", "CategoryName");
            ViewData["tagId"] = new SelectList(_db.Tags.ToList(), "Id", "TagName");
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(f=>f.Category).Include(c=>c.Tag).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }
                if (image == null)
                {
                    product.Image = "Images/default.png";
                }

                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            return View(product);
        }

        // product details

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = _db.Products.Include(c => c.Category).Include(c => c.Tag).FirstOrDefault(c => c.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);

        }

        // product delete

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _db.Products.FirstOrDefault(c => c.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            _db.Products.Remove(post);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
