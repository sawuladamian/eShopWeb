using eShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Tshirt> Tshirts { get; set; }
            
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //  public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        //   public DbSet<OrderHeader> OrderHeaders { get; set; }
        //    public DbSet<OrderDetail> OrderDetails { get; set; }
      
    }
}
