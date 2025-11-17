using Microsoft.AspNetCore.Mvc;
using _24DH111520_LTW_BE_TH.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace _24DH111520_LTW_BE_TH.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly MyStoreContext _context;

        public CartController(MyStoreContext context)
        {
            _context = context;
        }

        // Xem giỏ hàng
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // ✅ Thêm vào giỏ hàng (GET - dùng cho link)
        public IActionResult Add(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                TempData["Error"] = "Sản phẩm không tồn tại!";
                return RedirectToAction("Index", "Home");
            }

            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductImage = product.ProductImage,
                    Quantity = 1
                });
            }

            SaveCart(cart);
            TempData["Success"] = $"Đã thêm {product.ProductName} vào giỏ hàng!";

            // ✅ Quay lại trang trước đó
            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }
            return RedirectToAction("Index");
        }

        // Xóa khỏi giỏ hàng
        public IActionResult Remove(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                TempData["Success"] = "Đã xóa sản phẩm khỏi giỏ hàng!";
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // Cập nhật số lượng
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.Remove(item);
                    TempData["Success"] = "Đã xóa sản phẩm khỏi giỏ hàng!";
                }
                else
                {
                    item.Quantity = quantity;
                    TempData["Success"] = "Đã cập nhật số lượng!";
                }
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // Clear giỏ hàng
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            TempData["Success"] = "Đã xóa toàn bộ giỏ hàng!";
            return RedirectToAction("Index");
        }

        // === HELPER METHODS ===
        private List<CartItem> GetCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString("Cart", cartJson);
        }
    }
}
