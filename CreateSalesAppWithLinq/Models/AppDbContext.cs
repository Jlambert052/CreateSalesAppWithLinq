using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreateSalesAppWithLinq.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CreateSalesAppWithLinq.Models {

    public class AppDbContext : DbContext {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        

        public AppDbContext() { } //only for console not for web
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            string connString = @"server=localhost\sqlexpress;" +
                                "database=SalesDatabase;" +
                                "trusted_connection=true;";
            if(!builder.IsConfigured) {
                builder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder) {

        }

    }
}

