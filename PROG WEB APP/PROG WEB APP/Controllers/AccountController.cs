using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PROG_WEB_APP.Models;
using PROG_WEB_APP.ViewModels;
using System.Diagnostics;

namespace PROG_WEB_APP.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<Employee> signInManager;
        private readonly UserManager<Employee> usersManager;

        public AccountController(SignInManager<Employee> signInManager, UserManager<Employee> usersManager)
        {
            this.signInManager = signInManager;
            this.usersManager = usersManager;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            Console.Write("does it get here pleaase  ");
            if (ModelState.IsValid)
            {
                
                Console.Write("does it get here ");
                Employee employee = new Employee
                {
                    Fullname = model.FullName,
                    Email = model.Email,
                    UserName = model.Email,
                    

                };

                var result = await usersManager.CreateAsync(employee, model.Password);
                if (result.Succeeded)
                {
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
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }




       

        public IActionResult Register()
            {
                return View();
            }

        }
    } 
