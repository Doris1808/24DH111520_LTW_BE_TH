using Microsoft.AspNetCore.Mvc;
using _24DH111520_LTW_BE_TH.Models;
using System.Text.Json;

namespace _24DH111520_LTW_BE_TH.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly MyStoreContext _context;

        public OrderController(MyStoreContext context)
        {
            _context = context;
        }

        // GET: Trang Checkout
        public IActionResult Checkout()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                TempData["Error"] = "Giỏ hàng trống!";
                return RedirectToAction("Index", "Cart");
            }

            var cart = JsonSerializer.Deserialize<List<CartItem>>(cartJson);
            ViewBag.Cart = cart;
            ViewBag.Total = cart?.Sum(x => x.Total) ?? 0;

            return View();
        }

        // POST: Xử lý đơn hàng
        [HttpPost]
        public IActionResult ProcessOrder(string customerName, string customerPhone, string customerEmail, string customerAddress)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                TempData["Error"] = "Giỏ hàng trống!";
                return RedirectToAction("Index", "Cart");
            }

            var cart = JsonSerializer.Deserialize<List<CartItem>>(cartJson);
            if (cart == null || !cart.Any())
            {
                TempData["Error"] = "Giỏ hàng trống!";
                return RedirectToAction("Index", "Cart");
            }

            // Lấy username từ session
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                username = "guest"; // Nếu chưa đăng nhập
            }

            // 1. Tạo Customer mới
            var customer = new Models.Customer
            {
                CustomerName = customerName,
                CustomerPhone = customerPhone,
                CustomerEmail = customerEmail,
                CustomerAddress = customerAddress,
                Username = username
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            // 2. Tạo Order
            var order = new Order
            {
                CustomerId = customer.CustomerId,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                TotalAmount = cart.Sum(x => x.Total),
                AddressDelivery = customerAddress,
                PaymentStatus = "Pending"
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            // 3. Tạo OrderDetails
            foreach (var item in cart)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.ProductPrice
                };
                _context.OrderDetails.Add(orderDetail);
            }
            _context.SaveChanges();

            // 4. Xóa giỏ hàng
            HttpContext.Session.Remove("Cart");

            // 5. Chuyển sang trang ConfirmOrder
            TempData["OrderId"] = order.OrderId;
            TempData["CustomerName"] = customerName;
            TempData["TotalAmount"] = order.TotalAmount.ToString("N0");

            return RedirectToAction("ConfirmOrder");
        }

        // GET: Trang xác nhận đơn hàng
        public IActionResult ConfirmOrder()
        {
            if (TempData["OrderId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.OrderId = TempData["OrderId"];
            ViewBag.CustomerName = TempData["CustomerName"];
            ViewBag.TotalAmount = TempData["TotalAmount"];

            return View();
        }
    }
}
