using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScheduleApp.Models;

namespace ScheduleApp.Controllers
{
   public class AccountController : Controller
   {
      private readonly UserManager<AppUser> _userManager;
      private readonly SignInManager<AppUser> _signInManager;

      public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
      {
         _userManager = userManager;
         _signInManager = signInManager;
      }

      [HttpGet]
      [Route("/login")]
      public async Task<IActionResult> Login()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      [Route("/login")]
      public async Task<IActionResult> Login(LoginViewModel model)
      {
         if (ModelState.IsValid)
         {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);

            if (result.Succeeded)
            {
               return RedirectToAction("Index", "Schedule");
            }
            else
            {
               throw new Exception("Invalid login");
            }
         }
         return View(model);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      [Route("/logout")]
      public async Task<IActionResult> Logout()
      {
         await _signInManager.SignOutAsync();

         return View();
      }

      [HttpGet]
      [Route("/register")]
      public async Task<IActionResult> Register()
      {
         return View();
      }
      
      [HttpPost]
      [ValidateAntiForgeryToken]
      [Route("/register")]
      public async Task<IActionResult> Register(RegisterViewModel model)
      {
         if (ModelState.IsValid)
         {
            var user = new AppUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
               await _userManager.AddToRoleAsync(user, "freeAccount");
               return RedirectToAction("Index", "Schedule");
            }
            else
            {
               throw new Exception("Invalid registration");
            }
         }
         return View(model);
      }
   }

}