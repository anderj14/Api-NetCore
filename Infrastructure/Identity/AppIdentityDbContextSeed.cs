using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager){
            if(!userManager.Users.Any()){
                var user = new AppUser{
                    DisplayName = "Ander",
                    Email = "ander@gmail.com",
                    UserName = "anderJf",
                    Address = new Address{
                        FirstName = "Ander",
                        LastName = "Frias",
                        Street = "Doral 10 25",
                        City = "Santiago",
                        State = "Santiago",
                        ZipCode = "51010"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}