using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using DoAn_LTWeb_TRINHCHITAI.Models;

namespace DoAn_LTWeb_TRINHCHITAI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            MyDBContext user = new MyDBContext();
            List<User> users = user.Users.ToList();
            return View(users);
        }

        public ActionResult Login()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (user != null)
            {
                MyDBContext db = new MyDBContext();
                User myUser = db.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
                if (myUser != null)
                {
                    if (true)
                    {
                        HttpCookie authCookie = new HttpCookie("auth", myUser.UserName);
                        HttpCookie roleCookie = new HttpCookie("role", myUser.Role);
                        HttpCookie idCookie = new HttpCookie("id", myUser.UserId.ToString());
                        Response.Cookies.Add(authCookie);
                        Response.Cookies.Add(roleCookie);
                        Response.Cookies.Add(idCookie);

                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("Password", "Invalid username or password.");
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            HttpCookie authCookie = new HttpCookie("auth");
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpCookie roleCookie = new HttpCookie("role");
            roleCookie.Expires = DateTime.Now.AddDays(-1);

            HttpCookie idCookie = new HttpCookie("id");
            idCookie.Expires = DateTime.Now.AddDays(-1);

            Response.Cookies.Add(authCookie);
            Response.Cookies.Add(roleCookie);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public ActionResult Register(User user, string retypePassword)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (user.Password != retypePassword)
            {
                ModelState.AddModelError("retypePassword", "Passwords do not match.");
                return View(user);
            }

            MyDBContext db = new MyDBContext();
            User myUser = db.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
            if (myUser != null)
            {
                ModelState.AddModelError("UserName", "Username already exist.");
                return View(user);
            }
            myUser = db.Users.Where(u => u.EmailAddress == user.EmailAddress).FirstOrDefault();
            if (myUser != null)
            {
                ModelState.AddModelError("EmailAddress", "EmailAddress already exist.");
                return View(user);
            }

            myUser = new User();
            myUser.UserName = user.UserName;
            myUser.Password =user.Password;
            myUser.EmailAddress = user.EmailAddress;
            myUser.Role = "user";
            db.Users.Add(myUser);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult SuaUser(int id)
        {
            MyDBContext db = new MyDBContext();
            User us = db.Users.Where(row => row.UserId == id).FirstOrDefault();
            return View(us);
        }
        [HttpPost]
        public ActionResult SuaUser(User user)
        {
            MyDBContext db = new MyDBContext();
            User us = db.Users.Where(row => row.UserId == user.UserId).FirstOrDefault();
            //Update
            us.UserId = user.UserId;
            us.UserName = user.UserName;
            us.Password = user.Password;
            us.EmailAddress =user.EmailAddress;
            us.Role = user.Role;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult XoaUser(int id)
        {
            MyDBContext db = new MyDBContext();
            User us = db.Users.Where(row => row.UserId == id).FirstOrDefault();
            return View(us);
        }
        [HttpPost]
        public ActionResult XoaUser(int id, User user)
        {
            MyDBContext db = new MyDBContext();
            User us = db.Users.Where(row => row.UserId == id).FirstOrDefault();
            db.Users.Remove(us);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}