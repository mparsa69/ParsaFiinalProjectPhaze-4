using App.Domain.Core.OrderAgg.Contracts.IAppServices;
using App.Domain.Core.OrderAgg.Contracts.IServices;
using App.Domain.Core.OrderAgg.Dtos;
namespace App.Domain.AppServices.Order
{
    public class CommentAppService : ICommentAppService
    {
        private readonly ICommentService _commentService;

        public CommentAppService(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public List<CommentDto> AllComments()
        {
            return _commentService.AllComments();
        }

        public void Delete(int id)
        {
            _commentService.Delete(id);
        }

        public CommentDto Details(int id)
        {
            return _commentService.Details(id);
        }

        public void DisableShow(int id)
        {
            _commentService.DisableShow(id);
        }

        public void EnableShow(int id)
        {
            _commentService.EnableShow(id);
        }

        public List<CommentDto> OrderComments(int orderId)
        {
            return _commentService.OrderComments(orderId);
        }
    }
}
