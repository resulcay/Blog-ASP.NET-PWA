using Core.Areas.Admin.Models;
using DocumentFormat.OpenXml.ExtendedProperties;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        readonly RoleManager<AppRole> roleManager;
        readonly UserManager<AppUser> userManager;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppRole role = new()
                {
                    Name = model.Name
                };

                var result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditRole(int id)
        {
            var value =  roleManager.Roles.FirstOrDefault(x => x.Id == id);

            var model = new RoleEditViewModel
            {
                Id = value.Id,
                Name = value.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditViewModel model)
        {
            var value = roleManager.Roles.Where(x => x.Id == model.Id).FirstOrDefault();

            if (value != null)
            {
                value.Name = model.Name;
                var result = await roleManager.UpdateAsync(value);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", "Boş Geçilemez!");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var value = roleManager.Roles.FirstOrDefault(x => x.Id == id);
            var result = await roleManager.DeleteAsync(value);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult UserRoleList() 
        {
            var values = userManager.Users.ToList();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id) 
        {
            var user = userManager.Users.FirstOrDefault(x => x.Id == id);
            var roles = roleManager.Roles.ToList();
            TempData["UserId"] = user.Id;

            var userRoles = await userManager.GetRolesAsync(user);
            List<RoleAssignViewModel> roleAssignViewModel = new();

            foreach (var item in roles)
            {
                RoleAssignViewModel role = new()
                {
                    RoleId = item.Id,
                    Name = item.Name,
                    Exist = userRoles.Contains(item.Name)
                };
                roleAssignViewModel.Add(role);
            }

            return View(roleAssignViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> roleAssignViewModel)
        {
            var userId = (int)TempData["UserId"];
            var user = userManager.Users.FirstOrDefault(x => x.Id == userId);

            foreach (var item in roleAssignViewModel)
            {
                if (item.Exist)
                {
                    await userManager.AddToRoleAsync(user, item.Name);
                }
                else 
                {
                    await userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }

            return RedirectToAction("UserRoleList");
        }
    }
}
