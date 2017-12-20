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

        public async Task<IActionResult> Index()
        {
            var usersFromManager = await userManager.Users.ToListAsync();
            var users = await this.users.AllAsync();
            var firstUser = usersFromManager.FirstOrDefault();
            var usersRolesDict = new Dictionary<string, IList<string>>();
            foreach (var user in usersFromManager)
            {
                var userRoles = await this.userManager.GetRolesAsync(user);
                //usersRolesDict[user.Id] = new List<string>();
                usersRolesDict[user.Id] = userRoles;
            }
            var userRole = await this.userManager.GetRolesAsync(firstUser);
            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            return View(new AdminUsersViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid details");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.AddToRoleAsync(user, model.Role);
            TempData.AddSuccessMessage($"User {user.UserName} added to role {model.Role}");

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}