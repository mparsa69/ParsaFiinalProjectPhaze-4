using App.Domain.Core.OrderAgg.Contracts.IRepositories;
using App.Infrastructures.Database.SqlServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Repositories.EfCore.OrderRepo
{
    public class CommentCommandRepository : ICommentCommandRepository
    {
        private readonly AppDbContext _dbContext;

        public CommentCommandRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var model = _dbContext.Comments.FirstOrDefault(x => x.Id == id);
            _dbContext.Comments.Remove(model);
            _dbContext.SaveChanges();
        }

        public void DisableShow(int id)
        {
            var comment = _dbContext.Comments.SingleOrDefault(x => x.Id == id);
            comment.IsShow = false;
            _dbContext.SaveChanges();
        }

        public void EnableShow(int id)
        {
            var comment = _dbContext.Comments.SingleOrDefault(x => x.Id == id);
            comment.IsShow = true;
            _dbContext.SaveChanges();
        }
    }
}
