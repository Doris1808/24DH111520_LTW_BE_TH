using Microsoft.AspNetCore.Mvc;
using _24DH111520_LTW_BE_TH.Models;
using Microsoft.EntityFrameworkCore;

namespace _24DH111520_LTW_BE_TH.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly MyStoreContext _context;

        public HomeController(MyStoreContext context)
        {
            _context = context;
        }

        // Trang chủ
        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.ProductId)
                .Take(12)
                .ToList();

            // ✅ TRUYỀN CATEGORIES VÀO VIEWBAG
            ViewBag.Categories = _context.Categories.ToList();

            return View(products);
        }

        // Chi tiết sản phẩm
        public IActionResult ProductDetail(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // Tìm kiếm sản phẩm
        public IActionResult Search(string keyword, int? categoryId, string sort, int page = 1)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                products = products.Where(p => p.ProductName.Contains(keyword));
            }

            if (categoryId.HasValue && categoryId > 0)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            products = sort switch
            {
                "price_asc" => products.OrderBy(p => p.ProductPrice),
                "price_desc" => products.OrderByDescending(p => p.ProductPrice),
                "name" => products.OrderBy(p => p.ProductName),
                _ => products.OrderByDescending(p => p.ProductId)
            };

            int pageSize = 12;
            var totalItems = products.Count();
            var items = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Keyword = keyword;
            ViewBag.CategoryId = categoryId;
            ViewBag.Sort = sort;
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.Categories = _context.Categories.ToList();

            return View(items);
        }
    }
}
