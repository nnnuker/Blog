using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Entities;
using BLL.Interfaces;
using MvcPL.Areas.Admin.Models;
using MvcPL.Infrastructure.Providers;
using Ninject;

namespace MvcPL.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly IService<BllRole> roleService;
        private static readonly int loadedItems = 20;

        public AdminController(IKernel kernel)
        {
            this.userService = kernel.Get<IUserService>();
            this.roleService = kernel.Get<IService<BllRole>>();
        }

        public ActionResult Index(int pageNumber = 1)
        {
            var users = userService.GetAll();
            var model = users.Skip((pageNumber - 1) * loadedItems)
                .Take(loadedItems)
                .Select(u => new UserModel(u, roleService.Get(u.RoleId).Name));

            ViewBag.PageNumber = pageNumber;
            ViewBag.IsElements = users.Count() > pageNumber * loadedItems;
            ViewBag.HasPrevius = pageNumber > 1;

            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }

            return View(model);
        }

        public ActionResult CreateUser()
        {
            return View("EditUser");
        }

        public ActionResult DeleteUser(int id)
        {
            if (id > 0)
                userService.Delete(id);

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult EditUser(string json)
        {
            if (json == null)
                return View();

            var model = System.Web.Helpers.Json.Decode<UserModel>(json);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(UserModel userModel)
        {
            if (userModel == null)
            {
                return View();
            }

            ModelState["Id"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error while registration");
                return View(userModel);
            }

            var user = new BllUser
            {
                Id = userModel.Id,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                RoleId = roleService.GetAll().First(r => r.Name == userModel.Role).Id
            };

            if (userModel.Id == 0)
            {
                user.Password = Crypto.HashPassword(userModel.Password);
                userService.Create(user);
                var membershipUser = userService.Get(user.Email);

                if (membershipUser != null)
                {
                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("", "Error while registration");
                return View(userModel);
            }

            user.Password = userService.Get(userModel.Email).Password;
            userService.Update(user);

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult RoleIndex(int pageNumber = 1 )
        {
            var roles = roleService.GetAll();
            var model = roles
                .Skip((pageNumber - 1)*loadedItems)
                .Take(loadedItems);

            ViewBag.PageNumber = pageNumber;
            ViewBag.IsElements = roles.Count() > pageNumber * loadedItems;
            ViewBag.HasPrevius = pageNumber > 1;

            if (Request.IsAjaxRequest())
            {
                return PartialView(roles);
            }

            return View(roles);
        }

        public ActionResult CreateRole()
        {
            return View("EditRole");
        }

        public ActionResult DeleteRole(int id)
        {
            roleService.Delete(id);

            return RedirectToAction("RoleIndex");
        }

        public ActionResult EditRole(string json)
        {
            if (json == null)
                return View();

            var model = System.Web.Helpers.Json.Decode<BllRole>(json);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditRole(BllRole roleModel)
        {
            if (roleModel == null)
            {
                return View();
            }

            ModelState["Id"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error while editing role");
                return View(roleModel);
            }

            if (roleModel.Id == 0)
            {
                roleService.Create(roleModel);
                var membershipRole = roleService.GetAll().FirstOrDefault(r => r.Name == roleModel.Name);

                if (membershipRole != null)
                {
                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("", "Error while editing role");
                return View(roleModel);
            }

            roleService.Update(roleModel);
            return RedirectToAction("RoleIndex");
        }
    }
}