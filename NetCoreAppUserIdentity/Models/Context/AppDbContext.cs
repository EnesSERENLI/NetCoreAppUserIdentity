using Microsoft.AspNetCore.Identity.EntityFrameworkCore; //Nuget => IdentityDbContext için gerekli kütüphane
using Microsoft.EntityFrameworkCore;
using NetCoreAppUserIdentity.Models.Entity;

namespace NetCoreAppUserIdentity.Models.Context
{
    public class AppDbContext:IdentityDbContext<AppUser> 
    {
        public AppDbContext(DbContextOptions options) //Context yapılandırması base(options)'a göre olacak..(startup)
            :base(options)
        {

        }
    }
}
