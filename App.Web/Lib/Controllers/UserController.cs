using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using App.Web.Lib.Attributes;
using App.Web.Lib.Data.Services;
using App.Web.Lib.Models;
using App.Web.Lib.ViewModels;
using X.PagedList;

namespace App.Web.Lib.Controllers
{
    [RoutePrefix("Admin")]
    [Trust(Privilege = "Admin")]
    public class UserController : BaseController
    {
        private readonly ISystemUserService _sus;
        private readonly ISystemRoleService _srs;

        public UserController(ISystemUserService sus, ISystemRoleService srs)
        {
            _sus = sus;
            _srs = srs;
        }

        #region Index

        [Route("Users"), HttpGet]
        public ActionResult Index(string term, int? page)
        {
            var model = _sus.GetAllUsers().Select(u => new SystemUserVm.Index()
            {
                SystemUserId = u.SystemUserId,
                UserName = u.UserName,
                UserFirstName = u.FirstName,
                UserLastName = u.LastName,
                UserAlias = u.Alias,
                UserEmailAddress = u.EmailAddress,
                UserLoginEnabled = u.LoginEnabled,
                UserRoleCount = u.SystemUserRoles.Count
            });
            if (!string.IsNullOrEmpty(term))
            {
                model = model.Where(s => s.UserName.Contains(term.ToLower()));
            }
            var pageNo = page ?? 1;
            var pagedData = model.ToPagedList(pageNo, AppConfig.PageSize);
            ViewBag.Data = pagedData;

            return View("Index", pagedData);
        }

        #endregion

        #region Detail 

        [Route("User-Detail/{id}"), HttpGet]
        public ActionResult Detail(Guid id)
        {
            var user = _sus.GetUserById(id);
            if (user == null)
            {
                GetAlert(Danger, "User cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new SystemUserVm.Detail()
            {
                SystemUserId = user.SystemUserId,
                UserName = user.UserName,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                UserAlias = user.Alias,
                UserEmailAddress = user.EmailAddress,
                UserLoginEnabled = user.LoginEnabled
            };
            var userRoles = _sus.GetRolesForUser(id);
            var roleDetail = userRoles.Select(rd => new SystemUserVm.UserRolesDetail()
            {
                SystemRoleId = rd.SystemRoleId,
                SystemRoleName = rd.SystemRole.Name
            }).ToList();
            model.UserRolesDetail = roleDetail;
            return View("Detail", model);
        }

        #endregion

        #region Create

        [Route("Create-User"), HttpGet]
        public ActionResult Create()
        {
            var model = new SystemUserVm.Create();
            var roles = _srs.GetAllRoles();;
            var roleDetail = roles.Select(rd => new CheckBoxListItem()
            {
                Id = rd.SystemRoleId,
                Display = rd.Name,
                IsChecked = false
            }).ToList();
            model.Roles = roleDetail;
            return View("Create", model);
        }

        [Route("Create-User"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(SystemUserVm.Create model)
        {
            if (ModelState.IsValid)
            {
                var rolesToAdd = model.Roles.Where(r => r.IsChecked).Select(r => r.Id).ToList();
                _sus.CreateUser(model.UserName, model.UserFirstName, model.UserLastName, model.UserAlias, model.UserEmailAddress, model.UserLoginEnabled, rolesToAdd);
                GetAlert(Success, "User created!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Create", model);
        }

        #endregion

        #region Edit

        [Route("Edit-User/{id}"), HttpGet]
        public ActionResult Edit(Guid id)
        {
            var user = _sus.GetUserById(id);
            if (user == null)
            {
                GetAlert(Danger, "User cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new SystemUserVm.Edit()
            {
                SystemUserId = user.SystemUserId,
                UserName = user.UserName,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                UserAlias = user.Alias,
                UserEmailAddress = user.EmailAddress,
                UserLoginEnabled = user.LoginEnabled
            };
            var userRoles = _sus.GetRolesForUser(id);
            var roles = _srs.GetAllRoles();
            var roleDetail = roles.Select(rd => new CheckBoxListItem()
            {
                Id = rd.SystemRoleId,
                Display = rd.Name,
                IsChecked = userRoles.Any(ur => ur.SystemRoleId == rd.SystemRoleId)
            }).ToList();
            model.Roles = roleDetail;
            return View("Edit", model);
        }

        [Route("Edit-User/{id}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(SystemUserVm.Edit model)
        {
            if (ModelState.IsValid)
            {
                var rolesToAdd = model.Roles.Where(r => r.IsChecked).Select(r => r.Id).ToList();
                _sus.EditUser(model.SystemUserId, model.UserName, model.UserFirstName, model.UserLastName, model.UserAlias, model.UserEmailAddress, model.UserLoginEnabled, rolesToAdd);
                GetAlert(Success, "User updated!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Edit", model);
        }

        #endregion

        #region Delete

        [Route("Delete-User/{id}"), HttpGet]
        public ActionResult Delete(Guid id)
        {
            var user = _sus.GetUserById(id);
            if (user == null)
            {
                GetAlert(Danger, "User cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new SystemUserVm.Delete()
            {
                SystemUserId = user.SystemUserId,
                UserName = user.UserName,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                UserAlias = user.Alias,
                UserEmailAddress = user.EmailAddress,
                UserLoginEnabled = user.LoginEnabled
            };
            return View("Delete", model);
        }

        [Route("Delete-User/{id}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(SystemUserVm.Delete model)
        {
            if (ModelState.IsValid)
            {
                _sus.DeleteUser(model.SystemUserId);
                GetAlert(Success, "User deleted!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Delete", model);
        }

        #endregion
    }
}
