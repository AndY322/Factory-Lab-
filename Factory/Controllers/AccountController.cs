using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Factory.Models;
using System.Web.Security;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;


namespace Factory.Controllers
{
    public class AccountController : Controller
    {
        readonly int WorkFactor = 14;

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;

                using (FactoryContext db = new FactoryContext())
                {
                    user = db.Users.FirstOrDefault(u => u.UserName == model.Login);
                    //select * from users where Login = model.Login
                    if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User with this login and password don`t exist");
                    }
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ConfirmedPassword != model.Password)
                {
                    ModelState.AddModelError("", "Passwords don`t match");
                    return View(model);
                }
                User user = null;
                using (FactoryContext db = new FactoryContext())
                {
                    user = db.Users.FirstOrDefault(u => u.UserName == model.Login);
                    //select * from users where login = model.login
                }
                if (user == null)
                {
                    using (FactoryContext db = new FactoryContext())
                    {
                        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password, WorkFactor);
                        Roles.AddUserToRole(model.Login, "user");
                        db.Users.Add(
                            new User
                            {
                                UserName = model.Login,
                                PasswordHash = hashedPassword,
                                EmailConfirmed = false,
                                LockoutEnabled = false,
                                AccessFailedCount = 0
                            });
                        //insert into Users(UserName, PasswordHash, EmailConfirmed, LockoutEnabled, AccessFailedCount)
                        //values (model.Login, hashedPassword, 0, 0, 0)
                        await db.SaveChangesAsync();
                        user = db.Users.Where(u => u.UserName == model.Login).FirstOrDefault();
                    }
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User with this Login already exist");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}