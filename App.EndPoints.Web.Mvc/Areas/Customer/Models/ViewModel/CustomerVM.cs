using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.Web.Mvc.Areas.Customer.Models.ViewModel
{
    public class CustomerVM
    {
        [Display(Name = "شماره کاربری")]
        public string Id { get; set; }

        
        [Display(Name = "نام")]
        public string? FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string? LastName { get; set; }
        
        [Display(Name = "آدرس")]
        public string? Address { get; set; }
        /*[Display(Name = "تصویر")]
        public string Image { get; set; }*/

        [Display(Name = "شماره موبایل")]
        public string? PhoneNumber { get; set; }
        public IEnumerable<string>? Roles { get; set; } = new List<string>();
    }
}
