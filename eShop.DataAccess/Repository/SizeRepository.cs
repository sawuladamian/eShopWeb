using eShop.DataAccess.Data;
using eShop.DataAccess.Repository.IRepository;
using eShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess.Repository
{
    public class SizeRepository : Repository<Size>, ISizeRepository
    {
        private ApplicationDbContext _db;
        public SizeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Size obj)
        {
            var objFromDb = _db.Sizes.FirstOrDefault(x => x.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Type = obj.Type;
            }
        }
    }
}
