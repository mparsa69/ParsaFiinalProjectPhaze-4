using App.Domain.Core.OrderAgg.Dtos;
using App.Domain.Core.OrderAgg.Entities;
using App.Domain.Core.OrderAgg.Enums;
using App.Domain.Core.SuggestionAgg.Dtos;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.EndPoints.Web.Mvc.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _dbContext;

        public OrderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create(int catId, int secId)
        {
            ViewBag.SecId = secId;
            var o = new Order()
            {
                CustomerId= User.FindFirstValue(ClaimTypes.NameIdentifier),
                ThirdCategoryId= catId,
                
        };
            return View(o);
        }
        [HttpPost]
        public IActionResult Create(Order order,int SecondaryId)
        {
            var p=  _dbContext.ThirdCategories.Where(x=>x.Id==order.ThirdCategoryId).FirstOrDefault();
            order.BasePrice = p.Price;
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            
            return RedirectToAction("IndexThirdCategory", "Home", new {id=SecondaryId});
        }
        [HttpGet]
        public IActionResult CreateComment(int orderId,int categoryId)
        {
            CommentDto commentDto = new CommentDto()
            {
                OrderId = orderId,
                ThirdCategoryId = categoryId
            };
            return View(commentDto);
        }
        [HttpPost]
        public IActionResult CreateComment(CommentDto dto)
        {
            var comment = new Comment()
            {
                
                CommentText=dto.CommentText,
                CreatedAt=DateTime.Now,
                CreatedUserId= User.FindFirstValue(ClaimTypes.NameIdentifier),
                OrderId =dto.OrderId,
                ThirdCategoryId=dto.ThirdCategoryId,
            };
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
            return RedirectToAction("GetAllOrderByCustomerId","Order",new { customerId = User.FindFirstValue(ClaimTypes.NameIdentifier) });
        }
        public IActionResult GetAllOrderByCustomerId(string customerId)
        {
            var orders = _dbContext.Orders.Where(x => x.CustomerId == customerId).ToList();
            foreach(var order in orders)
            {
                
                if (order.Status == OrderStatusCode.WaitForSuggestion && order.ExpertId==null)
                {
                    var suggestionCount = _dbContext.Suggestions.Where(x => x.OrderId == order.Id).Count();
                    if (suggestionCount > 0)
                    {
                        order.Status = OrderStatusCode.WaitToChooseExpert;
                        _dbContext.Orders.Update(order);
                        _dbContext.SaveChanges();
                    }
                }
            }
            
            var ordersDto = _dbContext.Orders.Where(x => x.CustomerId == customerId).Select(x => new OrderCustomerDto()
            {
                Id = x.Id,
                Title = x.Title,
                ExpertId = x.ExpertId,
                Status = x.Status,
                CategoryId = x.ThirdCategoryId
                /*ExpertFirstName=_dbContext.ApplicationUsers.Where(y=>y.Id==x.ExpertId).Select(x=>x.FirstName).FirstOrDefault(),
                ExpertLastName=_dbContext.ApplicationUsers.Where(y=>y.Id==x.ExpertId).Select(x=>x.Family).FirstOrDefault(),*/
            }).ToList();
            return View(ordersDto);
        }
        public IActionResult OrderSuggestion(int orderId)
        {
            var suggestions = _dbContext.Suggestions.Where(x => x.OrderId == orderId).Select(x=>new SuggestionForCustomerDto()
            {
                Id=x.Id,
                StartTime=x.StartTime,
                SuggestedPrice=x.SuggestedPrice,
                SuggestionDate=x.SuggestionDate,
                Duration=x.Duration,
                IsApproved=x.IsApproved,
                ExpertId=x.ExpertId,
                ExpertUserName=_dbContext.ApplicationUsers.Where(y=>y.Id==x.ExpertId).Select(z=>z.UserName).FirstOrDefault(),
                ExpertFirstName=_dbContext.ApplicationUsers.Where(y=>y.Id==x.ExpertId).Select(z=>z.FirstName).FirstOrDefault(),
                ExpertLastName=_dbContext.ApplicationUsers.Where(y=>y.Id==x.ExpertId).Select(z=>z.Family).FirstOrDefault(),
                
            }).ToList();
            return View(suggestions);
        }

        [HttpPost]
        public IActionResult Approve(int suggestionId)
        {
            var sug = _dbContext.Suggestions.Where(x => x.Id == suggestionId).FirstOrDefault();
            sug.IsApproved = true;
            _dbContext.Suggestions.Update(sug);
            _dbContext.SaveChanges();
            ///
            var order=_dbContext.Orders.Where(x => x.Id == sug.OrderId).FirstOrDefault();
            order.ExpertId = sug.ExpertId;
            order.Status = OrderStatusCode.WaitToArriveExpert;
            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();
            return RedirectToAction("GetAllOrderByCustomerId", "Order", new { customerId = User.FindFirstValue(ClaimTypes.NameIdentifier) });
        }
        
    }
}
