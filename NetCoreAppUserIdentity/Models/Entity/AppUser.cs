using Microsoft.AspNetCore.Identity;
using System;

namespace NetCoreAppUserIdentity.Models.Entity
{
    public class AppUser:IdentityUser //IdentityUser'dan miras alacak. İçerisinde user için tanımlanacak bir çok property mevcut..
    {
        public DateTime BirthDate { get; set; }
    }
}
