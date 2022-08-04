using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.ApplicationUserAgg.Entities
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name="نام")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }
        [Display(Name="آدرس کامل شامل نام شهر، خیابان، کد پستی و ...")]
        public string? FullAddress { get; set; }
        

    }
}
