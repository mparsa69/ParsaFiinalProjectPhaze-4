using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.OrderAgg.Contracts.IRepositories
{
    public interface ICommentCommandRepository
    {
        public void Delete(int id);
        public void EnableShow(int id);
        public void DisableShow(int id);
    }
}
