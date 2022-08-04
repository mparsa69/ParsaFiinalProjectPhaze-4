using App.Domain.Core.OrderAgg.Contracts.IRepositories;
using App.Domain.Core.OrderAgg.Dtos;
using App.Infrastructures.Database.SqlServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Repositories.EfCore.OrderRepo
{
    public class CommentQueryRepository : ICommentQueryRepository
    {
        private readonly AppDbContext _dbContext;

        public CommentQueryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public CommentDto Details(int id)
        {
            var comment = _dbContext.Comments.Select(x => new CommentDto()
            {
                Id = x.Id,
                CommentText = x.CommentText,
                CreatedAt = x.CreatedAt,
                OrderId = x.OrderId,
                CreatedUserId = x.CreatedUserId,
                ThirdCategoryId = x.ThirdCategoryId,
                IsShow = x.IsShow
            }).Where(x => x.Id == id).FirstOrDefault();
            return comment;
        }

        public List<CommentDto> OrderComments(int orderId)
        {
            var comments = _dbContext.Comments.Select(x => new CommentDto()
            {
                Id = x.Id,
                CommentText = x.CommentText,
                CreatedAt = x.CreatedAt,
                OrderId = x.OrderId,
                CreatedUserId = x.CreatedUserId,
                ThirdCategoryId = x.ThirdCategoryId,
                IsShow = x.IsShow
            }).Where(x => x.OrderId == orderId).ToList();
            return comments;
        }
        public List<CommentDto> AllComments()
        {
            var comments = _dbContext.Comments.Select(x => new CommentDto()
            {
                Id = x.Id,
                CommentText = x.CommentText,
                CreatedAt = x.CreatedAt,
                OrderId = x.OrderId,
                CreatedUserId = x.CreatedUserId,
                ThirdCategoryId = x.ThirdCategoryId,
                IsShow = x.IsShow
            }).OrderByDescending(y=>y.CreatedAt).ToList();
            return comments;
        }
    }
}
