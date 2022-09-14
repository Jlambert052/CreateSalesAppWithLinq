using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Controllers {

    public class ProductController {

        private readonly AppDbContext _context = null!;
        public ProductController(AppDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Product>>GetAll() {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByPk(int productId) {
            return await _context.Products.FindAsync(productId);
        }


        public async Task<Product?> Insert(Product product) {
            if(product.Id != 0) {
                throw new Exception("The Id must be 0 in an insert.");
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task Update(int productId, Product product) {
            if(productId != product.Id) {
                throw new ArgumentException("product id does not match entry in table");
            }
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int productId) {
            Product? product = await GetByPk(productId);
            if(product is null) {
                throw new Exception("table entry does not exist, try different Id");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
