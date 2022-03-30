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
    public class TshirtRepository :Repository<Tshirt>,  ITshirtRepository
    {
        private ApplicationDbContext _db;
        public TshirtRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public void Update(Tshirt obj)
        {
            var objFromDb = _db.Tshirts.FirstOrDefault(u => u.Id == obj.Id);
                if(objFromDb != null)
            {
                objFromDb.Name=obj.Name;
                objFromDb.Description=obj.Description;
                objFromDb.ListPrice=obj.ListPrice;
                objFromDb.Price=obj.Price;
                objFromDb.Price50=obj.Price50;
                objFromDb.ColorId = obj.ColorId;
                objFromDb.SizeId = obj.SizeId;
                if (obj.ImageUrl != null) 
                { 
                    objFromDb.ImageUrl=obj.ImageUrl;
                }
            }
        }
    }
}
