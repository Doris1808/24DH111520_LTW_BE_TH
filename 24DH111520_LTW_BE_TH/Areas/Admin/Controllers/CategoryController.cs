using Microsoft.AspNetCore.Mvc;
using _24DH111520_LTW_BE_TH.Models;

namespace _24DH111520_LTW_BE_TH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly MyStoreContext _context;

        public CategoryController(MyStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/Category
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        // GET: Admin/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        [HttpPost]
        public IActionResult Create(Category category)
        {
            try
            {
                category.CategoryId = 0;
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(category);
            }
        }

        // GET: Admin/Category/Edit/5
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // POST: Admin/Category/Edit
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            try
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(category);
            }
        }

        // GET: Admin/Category/DeleteDirect/5
        public IActionResult DeleteDirect(int id)
        {
            try
            {
                var category = _context.Categories.Find(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Không thể xóa: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
