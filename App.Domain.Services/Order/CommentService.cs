using App.Domain.Core.OrderAgg.Contracts.IRepositories;
using App.Domain.Core.OrderAgg.Contracts.IServices;
using App.Domain.Core.OrderAgg.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Order
{
    public class CommentService : ICommentService
    {
        private readonly ICommentCommandRepository _commentCommandRepository;
        private readonly ICommentQueryRepository _commentQueryRepository;

        public CommentService(ICommentCommandRepository commentCommandRepository, ICommentQueryRepository commentQueryRepository)
        {
            _commentCommandRepository = commentCommandRepository;
            _commentQueryRepository = commentQueryRepository;
        }

        

        public void Delete(int id)
        {
            _commentCommandRepository.Delete(id);
        }

        public CommentDto Details(int id)
        {
            return _commentQueryRepository.Details(id);
        }

        public void DisableShow(int id)
        {
            _commentCommandRepository.DisableShow(id);
        }

        public void EnableShow(int id)
        {
            _commentCommandRepository.EnableShow(id);
        }

        public List<CommentDto> OrderComments(int orderId)
        {
            return _commentQueryRepository.OrderComments(orderId);
        }
        public List<CommentDto> AllComments()
        {
            return _commentQueryRepository.AllComments();
        }
    }
}
