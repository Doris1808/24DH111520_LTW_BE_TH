#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Checkout()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                TempData["Error"] = "Giỏ hàng trống!";
                return RedirectToAction("Index", "Cart");
            }

            var cart = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
            ViewBag.Cart = cart;
            ViewBag.Total = cart?.Sum(x => x.Total) ?? 0;

            //  TỰ ĐỘNG ĐIỀN THÔNG TIN NẾU ĐÃ ĐĂNG NHẬP
            var username = HttpContext.Session.GetString("Username");
            if (!string.IsNullOrEmpty(username))
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);
                if (customer != null)
                {
                    ViewBag.CustomerName = customer.CustomerName;
                    ViewBag.CustomerPhone = customer.CustomerPhone;
                    ViewBag.CustomerEmail = customer.CustomerEmail;
                    ViewBag.CustomerAddress = customer.CustomerAddress;
                }
            }

            return View();
        }

        // POST: Xử lý đơn hàng
        [HttpPost]
        public async Task<IActionResult> ProcessOrder(
            string customerName,
            string customerPhone,
            string customerEmail,
            string customerAddress,
            string paymentMethod)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                TempData["Error"] = "Giỏ hàng trống!";
                return RedirectToAction("Index", "Cart");
            }

            var cart = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
            if (cart == null || !cart.Any())
            {
                TempData["Error"] = "Giỏ hàng trống!";
                return RedirectToAction("Index", "Cart");
            }

            //  LẤY CUSTOMER ĐÃ ĐĂNG NHẬP (KHÔNG TẠO MỚI)
            var username = HttpContext.Session.GetString("Username");
            Models.Customer customer;

            if (!string.IsNullOrEmpty(username))
            {
                // Nếu đã đăng nhập → Lấy customer từ DB
                customer = await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);

                if (customer != null)
                {
                    // Cập nhật thông tin mới nhất
                    customer.CustomerName = customerName;
                    customer.CustomerPhone = customerPhone;
                    customer.CustomerEmail = customerEmail;
                    customer.CustomerAddress = customerAddress;
                    _context.Update(customer);
                }
                else
                {
                    // Nếu không tìm thấy → Tạo mới
                    customer = new Models.Customer
                    {
                        CustomerName = customerName,
                        CustomerPhone = customerPhone,
                        CustomerEmail = customerEmail,
                        CustomerAddress = customerAddress,
                        Username = username
                    };
                    _context.Customers.Add(customer);
                }
            }
            else
            {
                // Nếu chưa đăng nhập → Tạo guest
                customer = new Models.Customer
                {
                    CustomerName = customerName,
                    CustomerPhone = customerPhone,
                    CustomerEmail = customerEmail,
                    CustomerAddress = customerAddress,
                    Username = null
                };
                _context.Customers.Add(customer);
            }

            await _context.SaveChangesAsync();

            // 2. Tạo Order với Payment Method
            var order = new Order
            {
                CustomerId = customer.CustomerId,
                OrderDate = DateTime.Now,
                TotalAmount = cart.Sum(x => x.Total),
                AddressDelivery = customerAddress,
                PaymentStatus = paymentMethod
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

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
            await _context.SaveChangesAsync();

            // 4. Xóa giỏ hàng
            HttpContext.Session.Remove("Cart");

            // 5.  Chuyển sang trang ConfirmOrder
            TempData["OrderId"] = order.OrderId;
            TempData["CustomerName"] = customerName;
            TempData["TotalAmount"] = order.TotalAmount.ToString("N0");
            TempData["PaymentMethod"] = paymentMethod;
            TempData.Keep(); 

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
            ViewBag.PaymentMethod = TempData["PaymentMethod"];

            TempData.Keep(); 

            return View();
        }
        
        public async Task<IActionResult> History()
        {
            
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

    }
}
