using App.Domain.Core.ApplicationUserAgg.Entities;
using App.Domain.Core.OrderAgg.Contracts.IAppServices;
using App.Domain.Core.OrderAgg.Dtos;
using App.Domain.Core.OrderAgg.Entities;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace App.EndPoints.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ConstantProperty.Role_Admin)]
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        public IActionResult Index(string? name)
        {
            var orders=_orderAppService.GetAll(name);
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderAppService.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EnableShow(int id)
        {
            _orderAppService.EnableShow(id);
            /*return RedirectToAction("OrderComments", "Order", new {id});*/
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public IActionResult DisableShow(int id)
        {
            _orderAppService.DisableShow(id);
            return RedirectToAction("Index", "Order");
        }
    }
}
