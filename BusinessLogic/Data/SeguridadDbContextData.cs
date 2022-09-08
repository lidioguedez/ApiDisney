using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class SeguridadDbContextData 
    {

        public static async Task SeedUserAsync(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager) {

            if (!userManager.Users.Any()) {
                var usuario = new Usuario
                {
                    Nombre = "Admin",
                    Apellido = "Master",
                    UserName = "admmaster",
                    Email = "masterapi@lidioguedez.com",
                    Imagen = "imagen.jpg",
                    Direccion = new Direccion
                    {
                        Calle = "Rucci",
                        Ciudad = "Ciudadela",
                        CodigoPostal = "1702",
                        Departamento = "GBA",
                        Pais = "Argentina"
                    }
                };

               await userManager.CreateAsync(usuario, "OKmaster123456$");

            }


            if (!roleManager.Roles.Any()) {
                var role = new IdentityRole
                {
                    Name = "ADMIN"
                };
                await roleManager.CreateAsync(role);
            }


        }

    }
}
