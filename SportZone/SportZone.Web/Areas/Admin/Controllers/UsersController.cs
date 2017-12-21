using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportZone.Data.Models;
using SportZone.Services.Admin;
using SportZone.Web.Areas.Admin.Models.Users;
using SportZone.Web.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        #region fields

        private readonly IAdminUserService users;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        #endregion

        #region ctror

        public UsersController(IAdminUserService users,
                RoleManager<IdentityRole> roleManager,
                UserManager<User> userManager)
        {
            this.users = users;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        #endregion

        #region methods

        public async Task<IActionResult> Index(string searchText, int page = 1)
        {
            var search = string.IsNullOrEmpty(searchText)
                 ? string.Empty
                 : searchText.ToLower();

            var usersFromManager = await userManager
                    .Users
                    .OrderBy(u => u.Id)
                    .Where(u => u.UserName.ToLower().Contains(search))
                    .Skip((page - 1) * AdminPageSize)
                    .Take(AdminPageSize)
                    .ToListAsync();

            var users = await this.users.AllAsync(search, page);
            var usersRoles = new Dictionary<string, IList<string>>();
            foreach (var user in usersFromManager)
            {
                var userRoles = await this.userManager.GetRolesAsync(user);
                usersRoles[user.Id] = userRoles;
            }

            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();


            if (!string.IsNullOrEmpty(searchText))
            {
                ViewData["Title"] = $"Search Results For {search}";
            }

            else
            {
                ViewData["Title"] = "User Administration";
            }

            var viewModel = new AdminUsersViewModel
            {
                Users = users,
                UserRoles = usersRoles,
                Roles = roles,
                TotalUsers = await this.users.TotalAsync(search),
                CurrentPage = page
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(RoleFormModel model)
        {
            var role = model.Role;
            var isValidModelData = await CheckRoleAndUserAsync(role, model.UserId);
            if (!isValidModelData)
            {
                TempData.AddErrorMessage("Invalid details");
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userName = user.UserName;
            if (await IsUserInRoleAsync(user, role))
            {
                TempData.AddErrorMessage($"User {userName} is already in {role} role");
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.AddToRoleAsync(user, role);
            TempData.AddSuccessMessage($"User {userName} added to role {role}");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(RoleFormModel model)
        {
            var role = model.Role;
            var isValidModelData = await CheckRoleAndUserAsync(role, model.UserId);
            if (!isValidModelData)
            {
                TempData.AddErrorMessage("Invalid details");
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userName = user.UserName;
            if (!await IsUserInRoleAsync(user, role))
            {
                TempData.AddErrorMessage($"User {userName} is not in {role} role");
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.RemoveFromRoleAsync(user, role);
            TempData.AddSuccessMessage($"User {userName} removed from role {role}");

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CheckRoleAndUserAsync(string role, string userId)
        {
            if (!await this.roleManager.RoleExistsAsync(role))
            {
                return false;
            }

            var user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> IsUserInRoleAsync(User user, string role)
            => await this.userManager.IsInRoleAsync(user, role);
        #endregion
    }
}