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
    public class TshirtImageUrlsRepository : Repository<TshirtImagesUrl>, ITshirtImageUrlsRepository
    {
        private ApplicationDbContext _db;
        public TshirtImageUrlsRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;        
        }
        public void Update(TshirtImagesUrl obj)
        {
            _db.TshirtImagesUrls.Update(obj);
        }
    }
}
