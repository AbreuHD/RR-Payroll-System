using Core.Application.Enum;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Owner.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Empleado.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.AdministradorDeUsuarios.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Contador.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Gerente.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.JefeDeObras.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.RRHH.ToString()));
        }
    }
}
