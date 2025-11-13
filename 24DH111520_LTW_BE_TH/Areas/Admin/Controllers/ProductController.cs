using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using _24DH111520_LTW_BE_TH.Models;
using Microsoft.EntityFrameworkCore;

namespace _24DH111520_LTW_BE_TH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly MyStoreContext _context;

        public ProductController(MyStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/Product
        public IActionResult Index(string search, string sort, int page = 1)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();

            // SEARCH
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.ProductName.Contains(search));
            }

            // SORT
            products = sort switch
            {
                "name_desc" => products.OrderByDescending(p => p.ProductName),
                "price" => products.OrderBy(p => p.ProductPrice),
                "price_desc" => products.OrderByDescending(p => p.ProductPrice),
                _ => products.OrderBy(p => p.ProductName)
            };

            // PAGING
            int pageSize = 10;
            var totalItems = products.Count();
            var items = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Search = search;
            ViewBag.Sort = sort;
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(items);
        }

        // GET: Admin/Product/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Product/Create
        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                product.ProductId = 0;
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                return View(product);
            }
        }

        // GET: Admin/Product/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Product/Edit
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            try
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }
        }

        // GET: Admin/Product/DeleteDirect/5
        public IActionResult DeleteDirect(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                }
            }
            catch { }

            return RedirectToAction("Index");
        }
    }
}
