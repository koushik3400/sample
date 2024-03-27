using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Stock.api.Model.Inventory;

namespace Stock.api.Model
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        public DbSet<Category> Categorys { get; set; }
        public DbSet<Stock_Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseProducts> PurchaseProducts { get; set; }
        public DbSet<SaleProducts> SaleProducts { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<StockMaster> StockMasters { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Stock_users> Users { get; set; }
        public DbSet<StockDash> StockDashs { get; set; }

        public IQueryable<StockDash> getAdminDash()
        {
            return this.StockDashs.FromSql("EXECUTE stockDashboard");
        }
    }
}
