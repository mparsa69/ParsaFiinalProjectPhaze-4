using App.Domain.Core.OrderAgg.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.OrderAgg.Contracts.IRepositories
{
    public interface ICommentQueryRepository
    {
        public List<CommentDto> OrderComments(int orderId);
        public List<CommentDto> AllComments();
        public CommentDto Details(int id);
    }
}
