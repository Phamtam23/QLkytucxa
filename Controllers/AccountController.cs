using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Quanlykytucxa.Models;
using System.Globalization;
using Quanlykytucxa.Viewmodel;
using Microsoft.AspNetCore.Http;

namespace Quanlykytucxa.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<SinhVien> signInManager;
        private UserManager<SinhVien> userManager;
        public AccountController(SignInManager<SinhVien> signInManager, UserManager<SinhVien> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

      
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult VerifyEMail()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterviewModel model)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new Random();
                string cccd = "";
                for (int i = 0; i < 10; i++)
                {
                    cccd += rnd.Next(0, 10).ToString();
                }
                SinhVien users = new SinhVien
                {
                    TenSv = model.Name,
                    GioiTinh=model.Gioitinh,
                    Email = model.Email,
                    UserName = model.Email,
                    Cccd = cccd,
                    Sdt = cccd,
                    DiaChi ="Da Nang",
                    NgaySinh= DateTime.ParseExact("23/01/2004", "dd/MM/yyyy", CultureInfo.InvariantCulture)

            };

               
                
                var result = await userManager.CreateAsync(users, model.Password);
               

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(users, "client");
                    return RedirectToAction("Login", "Account",new { area=""});
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View();
                }
            }
            return View(model);

        }
        public async Task<IActionResult> Login(Loginviewmodel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RemeberMe, false);

                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    Response.Cookies.Append("UserId", user.Id, new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(7) 
                    });
                    if (await userManager.IsInRoleAsync(user, "admin"))
                    {
                        
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    return RedirectToAction("Index", "Home",new {area=""});
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailModelView model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Something is wrong");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ChangePassword", "Account", new { email = model.Email });
                }
            }
            return View(model);
        }
        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
            return View(new ChangePasswordViewModel { Email = username });
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    var result = await userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddPasswordAsync(user, model.NewPassword);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }    
                }
                else
                {
                    ModelState.AddModelError("", "Email not found");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong try again");
                return View(model);
            }
           
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
