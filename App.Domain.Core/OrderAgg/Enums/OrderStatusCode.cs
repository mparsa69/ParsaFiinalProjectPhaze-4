using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.OrderAgg.Enums
{
    public enum OrderStatusCode
    {
        [Display(Name ="منتظر پیشنهاد متخصصان")]
        WaitForSuggestion=0,
        [Display(Name = "منتظر انتخاب متخصص")]
        WaitToChooseExpert =1,
        [Display(Name = "منتظر آمدن متخصص به محل شما")]
        WaitToArriveExpert =2,
        [Display(Name = "شروع شده")]
        Started =3,
        [Display(Name = "انجام شده")]
        Done =4,
        [Display(Name = "پرداخت شده")]
        Payed =5


    }
}
