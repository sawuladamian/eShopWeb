using eShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess.Repository.IRepository
{
    public interface ITshirtRepository : IRepository<Tshirt>
    {
        void Update(Tshirt obj);
    }
}
