using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Controllers {

    public class CustomerController {

        private readonly AppDbContext _context = null!;
        public CustomerController(AppDbContext context) {
           _context = context;
        }
        
        public async Task<IEnumerable<Customer>> GetAll() {
           return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByPk(int customerId) {
            return await _context.Customers.FindAsync();
        }

        public async Task<Customer> Insert(Customer customer) {
            if (customer.Id != 0) {
                throw new ArgumentException("CustomerId must be 0 when entering new row.");
            }
            await _context.Customers.AddAsync(customer);
            _context.SaveChangesAsync();
            return customer;
        }

        public async Task Update(int customerId, Customer customer) {
            if (customerId != customer.Id) {
                throw new Exception("The entered ID does not match one in the table.");
            }
            _context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return;
        }
        public async Task Delete(int customerId) {
            Customer? customer = await GetByPk(customerId);
            if (customer is null) {
                throw new Exception("This Id does not exist");
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
