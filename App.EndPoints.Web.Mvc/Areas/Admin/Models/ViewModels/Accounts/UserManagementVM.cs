using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.Web.Mvc.Areas.Admin.Models.ViewModels.Accounts
{
    public class UserManagementVM
    {
        [Display(Name = "شماره کاربری")]
        public string Id { get; set; }

        [Display(Name = " نام کاربری")]
        public string? Name { get; set; }
        [Display(Name = "نام")]
        public string? FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string? LastName { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "شماره موبایل")]
        public string? PhoneNumber { get; set; }
        public IEnumerable<string>? Roles { get; set; } = new List<string>();
    }
}
