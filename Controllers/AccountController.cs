using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Entites;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly MyAppContext _Context;

        public AccountController(MyAppContext context)
        {

            _Context = context;
        }
        public IActionResult Index()
        {
            return View(_Context.UserAccounts.ToList());
        }

        public IActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount account = new UserAccount();
                account.Email = model.Email;
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.Password = model.Password;
                account.UserName = model.UserName;

                try
                {
                    _Context.UserAccounts.Add(account);
                    _Context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName} {account.LastName} registrated succsessfully. Please login.";
                    return View();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Alrady Registred plese login or use Diff email");
                    return View(model);
                }

            }


            return View(model);
        }

        public IActionResult Login() { 
            return View();
        
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _Context.UserAccounts.Where(x => (x.UserName == model.UserNameOrEmail || x.Email == model.UserNameOrEmail) && x.Password == model.Password).FirstOrDefault();
                if (user != null) 
                {
                    // Success
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("Name", user.FirstName),
                        new Claim(ClaimTypes.Role, "User"),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("SecurePage");
                }
                else
                {
                    ModelState.AddModelError("", "UserName or Password is invalid");
                }
            }
                return View(model);

        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult SecurePage() 
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}
