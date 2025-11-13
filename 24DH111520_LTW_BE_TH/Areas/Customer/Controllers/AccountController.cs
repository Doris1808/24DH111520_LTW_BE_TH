using Microsoft.AspNetCore.Mvc;
using _24DH111520_LTW_BE_TH.Models;  // ← ĐÃ CÓ DÒNG NÀY
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace _24DH111520_LTW_BE_TH.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {
        private readonly MyStoreContext _context;

        public AccountController(MyStoreContext context)
        {
            _context = context;
        }

        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public IActionResult Register(string username, string password, string confirmPassword,
            string customerName, string customerPhone, string customerEmail)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Username và Password không được để trống";
                return View();
            }

            if (password != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu không khớp";
                return View();
            }

            if (_context.Users.Any(u => u.Username == username))
            {
                ViewBag.Error = "Username đã tồn tại";
                return View();
            }

            try
            {
                // Create User
                var user = new Models.User  // ← THÊM Models. phía trước
                {
                    Username = username,
                    Password = HashPassword(password),
                    UserRole = 'C'
                };

                _context.Users.Add(user);

                // Create Customer - DÙNG FULL NAMESPACE
                var customer = new Models.Customer  // ← THÊM Models. phía trước
                {
                    Username = username,
                    CustomerName = customerName,
                    CustomerPhone = customerPhone,
                    CustomerEmail = customerEmail
                };

                _context.Customers.Add(customer);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi: " + ex.Message;
                return View();
            }
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || user.Password != HashPassword(password))
            {
                ViewBag.Error = "Sai username hoặc password";
                return View();
            }

            // Save to session
            HttpContext.Session.SetString("Username", username);
            HttpContext.Session.SetString("UserRole", user.UserRole.ToString());

            // Redirect based on role
            if (user.UserRole == 'A')
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Hash password (simple MD5)
        private string HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }
    }
}
