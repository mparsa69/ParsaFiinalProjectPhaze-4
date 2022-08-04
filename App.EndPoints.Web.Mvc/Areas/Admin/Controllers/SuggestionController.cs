using App.Domain.Core.SuggestionAgg.Contracts.IAppServices;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace App.EndPoints.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ConstantProperty.Role_Admin)]
    public class SuggestionController : Controller
    {
        private readonly ISuggestionAppService _suggestionAppService;
        public SuggestionController(ISuggestionAppService suggestionAppService)
        {
            _suggestionAppService = suggestionAppService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OrderSuggestions(int orderId,string? name)
        {
            var suggestions=_suggestionAppService.OrderSuggestions(orderId, name);
            return View(suggestions);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            _suggestionAppService.Delete(id);
            return RedirectToAction("");
        }
        [HttpPost]
        public IActionResult EnableShow(int id)
        {
            _suggestionAppService.EnableShow(id);
            /*return RedirectToAction("OrderComments", "Order", new {id});*/
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public IActionResult DisableShow(int id)
        {
            _suggestionAppService.DisableShow(id);
            return RedirectToAction("Index", "Order");
        }
    }
}
