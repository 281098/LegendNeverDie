﻿using BotDetect.Web.Mvc;
using LND.Common;
using LND.Model.Models;
using LND.Web.App_Start;
using LND.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LND.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public AccountController()
        {
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        // [CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userByEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userByEmail != null)
                {
                    ModelState.AddModelError("email", "Email đã tồn tại");
                    return View(model);
                }
                var userByUserName = await _userManager.FindByNameAsync(model.UserName);
                if (userByUserName != null)
                {
                    ModelState.AddModelError("email", "Tài khoản đã tồn tại");
                    return View(model);
                }
                if (model.Password == model.ConfirmPassword)
                {
                    var user = new ApplicationUser()
                    {
                        FullName = model.FullName,
                        Address = model.Address,
                        UserName = model.UserName,
                        Email = model.Email,
                        EmailConfirmed = true,
                        BirthDay = DateTime.Now,
                        PhoneNumber = model.PhoneNumber
                    };

                    await _userManager.CreateAsync(user, model.Password);

                    var adminUser = await _userManager.FindByEmailAsync(model.Email);
                    if (adminUser != null)
                        await _userManager.AddToRolesAsync(adminUser.Id, new string[] { "ViewUser" });

                    string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/mail/register.html"));
                    content = content.Replace("{{FullName}}", adminUser.FullName);
                    content = content.Replace("{{UserName}}", adminUser.UserName);
                    content = content.Replace("{{PhoneNumber}}", adminUser.PhoneNumber);
                    content = content.Replace("{{Address}}", adminUser.Address);
                    content = content.Replace("{{Link}}", "http://localhost:49972/dang-nhap-html");

                    MailHelper.SendMail(adminUser.Email, "Đăng ký thành công", content);

                    ViewData["SuccessMsg"] = "Đăng ký thành công";
                }
                else
                    ViewData["FailedMessage"] = "Mật khẩu xác nhận phải trùng với Mật khẩu";
            }

            return View();
        }

        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            //return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.Find(model.UserName, model.Password);

                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties();
                    props.IsPersistent = model.Rememberme;
                    authenticationManager.SignIn(props, identity);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}