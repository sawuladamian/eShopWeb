using eShop.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess.Repository.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Size = new SizeRepository(_db);
            Color = new ColorRepository(_db);
            Tshirt = new TshirtRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            //ShoppingCart = new ShoppingCartRepository(_db);
            //OrderHeader = new OrderHeaderRepository(_db);
            //OrderDetail = new OrderDetailRepository(_db);
        }

        
        //public IShoppingCartRepository ShoppingCart { get; private set; }
        //public IOrderDetailRepository OrderDetail { get; private set; }
        //public IOrderHeaderRepository OrderHeader { get; private set; }
        

        public ISizeRepository Size { get; private set; }
        public IColorRepository Color { get; private set; }
        public ITshirtRepository Tshirt { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

