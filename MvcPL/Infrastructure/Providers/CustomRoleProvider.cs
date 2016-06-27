using System;
using System.Linq;
using System.Web.Security;
using BLL.Entities;
using BLL.Interfaces;

namespace MvcPL.Infrastructure.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserService
            => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        public IService<BllRole> RoleService
            => (IService<BllRole>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IService<BllRole>));

        public override bool IsUserInRole(string email, string roleName)
        {
            BllUser user = UserService.GetAll().FirstOrDefault(u => u.Email == email);

            if (user == null) return false;

            BllRole userRole = RoleService.Get(user.RoleId);

            if (userRole != null && userRole.Name == roleName)
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string email)
        {
            var roles = new string[] { };

            var user = UserService.Get(email);

            if (user == null) return roles;

            var userRole = RoleService.Get(user.RoleId);

            if (userRole != null)
            {
                roles = new string[] { userRole.Name };
            }
            return roles;
        }

        public override void CreateRole(string roleName)
        {
            if (roleName == null) return;
            var newRole = new BllRole() { Name = roleName };
            RoleService.Create(newRole);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            var bllRole = RoleService.GetAll().FirstOrDefault(r => r.Name == roleName);
            if (bllRole == null)
            {
                return false;
            }
            var id = bllRole.Id;
            RoleService.Delete(new BllRole {Name = roleName, Id = id});
            return true;
        }

        public override bool RoleExists(string roleName)
        {
            var bllRole = RoleService.GetAll().FirstOrDefault(r => r.Name == roleName);
            return bllRole != null;
        }

        public override string[] GetAllRoles()
        {
            return RoleService.GetAll().Select(x => x.Name).ToArray();
        }

        #region Stubs

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
        #endregion

    }
}