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
    public class ColorRepository : Repository<Color>, IColorRepository
    {
        private ApplicationDbContext _db;
        public ColorRepository(ApplicationDbContext db) :base (db)
        {
            _db = db;
        }
        public void Update(Color entity)
        {
            var objFromDb = _db.Colors.FirstOrDefault(x => x.Id == entity.Id);
                if(objFromDb != null)
            {
                objFromDb.Type = entity.Type;
                objFromDb.ColorInRGB = entity.ColorInRGB;
            }
        }
    }
}
