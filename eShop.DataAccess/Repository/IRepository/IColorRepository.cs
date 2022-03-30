using eShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess.Repository.IRepository
{
    public interface IColorRepository :IRepository<Color>
    {
        void Update(Color entity);
    }
}
