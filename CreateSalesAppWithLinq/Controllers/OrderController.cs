using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Controllers {

    public class OrderController {

        private readonly AppDbContext _context = null!;
        public OrderController(AppDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAll() {
            return await _context.Orders.ToListAsync();
        }
        //Return a list of orders for the entered customerID
        //need to pull Order data where customerId is = to
        public async Task<IEnumerable<Order>> GetOrderByCx(int customerId) {
            //method synthax
                return await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
            //query syntax
            //var orders = from o in _context.Orders
            //             where o.CustomerId == customerId
            //             select o;
            //return await orders.ToListAsync();
        }
        //pass in the customer code, and return orders related to that code.
        public async Task<IEnumerable<Order>> GetOrderbyCxCode(string customerCode) {
            var orders = from o in _context.Orders
                         join c in _context.Customers
                            on o.CustomerId equals c.Id
                        where customerCode == c.Code
                        select o;
            return await orders.ToListAsync();
        }
        //show a list of orders that have a certain productId in the orderline. 
        public async Task<IEnumerable<Order>> GetOrderByOrdl(int productId) {
            var ordlist = from o in _context.Orders
                          join ol in _context.OrderLines
                            on o.Id equals ol.OrderId
                          join p in _context.Products
                              on ol.ProductId equals p.Id
                          where productId == p.Id
                          select o;
            return await ordlist.ToListAsync();
        }

        //pass in order instance and update by changing the status to closed

        public async Task UpdateStatusClosed(int orderId, Order order) {
            order.Status = "Closed";
            await Update(orderId, order);
        }
        //Update status to INPROCESS if the total is > 0.

        public async Task UpdateStatusInProcess(int orderId, Order order) {
            if (order.Total > 0) {
                order.Status = "InProcess";
                await Update(orderId, order);
            } 
            if (order.Status is "InProcess") {
                Console.WriteLine("Order already marked in process");
            } 
            else {
                Console.WriteLine("Order status is paid; does not need to be updated to InProcess.");
            }
        }
            //


            public async Task<Order?> GetByPk(int orderId) {
            return await _context.Orders.FindAsync(orderId);
        }


        public async Task<Order?> Insert(Order order) {
            if (order.Id != 0) {
                throw new Exception("The Id must be 0 in an insert.");
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task Update(int orderId, Order order) {
            if (orderId != order.Id) {
                throw new ArgumentException("product id does not match entry in table");
            }
            _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int orderId) {
            Order? order = await GetByPk(orderId);
            if (order is null) {
                throw new Exception("table entry does not exist, try different Id");
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

    }
}
