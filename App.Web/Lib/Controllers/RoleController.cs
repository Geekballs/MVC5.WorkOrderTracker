using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using App.Web.Lib.Attributes;
using App.Web.Lib.Data.Services;
using App.Web.Lib.ViewModels;
using X.PagedList;

namespace App.Web.Lib.Controllers
{
    [RoutePrefix("Admin")]
    [Trust(Privilege = "Admin")]
    public class RoleController : BaseController
    {
        private readonly ISystemRoleService _systemRoleService;

        public RoleController(ISystemRoleService systemRoleService)
        {
            _systemRoleService = systemRoleService;
        }

        #region Index

        [Route("Roles"), HttpGet]
        public ActionResult Index(string term, int? page)
        {
            var model = _systemRoleService.GetAllRoles().Select(r => new SystemRoleVm.Index()
            {
                SystemRoleId = r.SystemRoleId,
                RoleName = r.Name,
                RoleDescription = r.Description,
                SystemRoleUserCount = r.SystemUserRoles.Count
            });
            if (!string.IsNullOrEmpty(term))
            {
                model = model.Where(r => r.RoleName.Contains(term) || r.RoleDescription.Contains(term.ToLower()));
            }
            var pageNo = page ?? 1;
            var pagedData = model.ToPagedList(pageNo, AppConfig.PageSize);
            ViewBag.Data = pagedData;
            return View("Index", pagedData);
        }

        #endregion

        #region Detail 

        [Route("Role-Detail/{id}"), HttpGet]
        public ActionResult Detail(Guid id)
        {
            var role = _systemRoleService.GetRoleById(id);
            if (role == null)
            {
                GetAlert(Danger, "Role cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new SystemRoleVm.Detail()
            {
                SystemRoleId = role.SystemRoleId,
                RoleName = role.Name,
                RoleDescription = role.Description
            };
            var roleUsers = _systemRoleService.GetUsersInRole(id);
            var userDetail = roleUsers.Select(ru => new SystemRoleVm.RoleUsersDetail()
            {
                SystemUserId = ru.SystemUserId,
                UserName = ru.SystemUser.UserName
            }).ToList();
            model.RoleUsersDetail = userDetail;
            return View("Detail", model);
        }

        #endregion

        #region Create

        [Route("Create-Role"), HttpGet]
        public ActionResult Create()
        {
            var model = new SystemRoleVm.Create();
            return View("Create", model);
        }

        [Route("Create-Role"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(SystemRoleVm.Create model)
        {
            if (ModelState.IsValid)
            {
                _systemRoleService.CreateRole(model.RoleName, model.RoleDescription);
                GetAlert(Success, "Role created!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Create", model);
        }

        #endregion

        #region Edit

        [Route("Edit-Role/{id}"), HttpGet]
        public ActionResult Edit(Guid id)
        {
            var role = _systemRoleService.GetRoleById(id);
            if (role == null)
            {
                GetAlert(Danger, "Role cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new SystemRoleVm.Edit()
            {
                SystemRoleId = role.SystemRoleId,
                RoleName = role.Name,
                RoleDescription = role.Description
            };
            return View("Edit", model);
        }

        [Route("Edit-Role/{id}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(SystemRoleVm.Edit model)
        {
            if (ModelState.IsValid)
            {
                _systemRoleService.EditRole(model.SystemRoleId, model.RoleName, model.RoleDescription);
                GetAlert(Success, "Role updated!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Edit", model);
        }

        #endregion

        #region Delete

        [Route("Delete-Role/{id}"), HttpGet]
        public ActionResult Delete(Guid id)
        {
            var role = _systemRoleService.GetRoleById(id);
            if (role == null)
            {
                GetAlert(Danger, "Role cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new SystemRoleVm.Delete()
            {
                SystemRoleId = role.SystemRoleId,
                RoleName = role.Name,
                RoleDescription = role.Description
            };
            return View("Delete", model);
        }

        [Route("Delete-Role/{id}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(SystemRoleVm.Delete model)
        {
            if (ModelState.IsValid)
            {
                _systemRoleService.DeleteRole(model.SystemRoleId);
                GetAlert(Success, "Role deleted!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Delete", model);
        }

        #endregion
    }
}