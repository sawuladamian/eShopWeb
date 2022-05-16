using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ISizeRepository Size { get; }
        
        ITshirtRepository Tshirt  { get; }
        
        IApplicationUserRepository ApplicationUser { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }
        ITshirtImageUrlsRepository TshirtImageUrl { get; }
        void Save();


    }
}
