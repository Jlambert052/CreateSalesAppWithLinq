using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Controllers {

    public class OrderLineController {


        private readonly AppDbContext _context = null!;
        private OrderController ordctrl;
        public OrderLineController(AppDbContext context) {
            _context = context;
            ordctrl = new(_context);
        }

        public async Task<IEnumerable<OrderLine>> GetAll() {
            return await _context.OrderLines.ToListAsync();
        }

        public async Task<OrderLine?> GetByPk(int orderlineId) {
            return await _context.OrderLines.FindAsync(orderlineId);
        }
        //Update orderline to show quantity and order total increases
        private async Task OrderTotalUpdate(int orderId) {
            var order = await ordctrl.GetByPk(orderId);
            if (order is null) {
                throw new Exception("Order not found.");
            }
            order.Total = (from ol in _context.OrderLines
                          join p in _context.Products
                            on ol.ProductId equals p.Id
                          where ol.OrderId == orderId
                          select new {
                              LineTotal = ol.Quantity * p.Price
                          }).Sum(x => x.LineTotal);
            await ordctrl.Update(order.Id, order);
        }

        public async Task<OrderLine?> Insert(OrderLine orderline) {
            if (orderline.Id != 0) {
                throw new Exception("The Id must be 0 in an insert.");
            }
            _context.OrderLines.Add(orderline);
            await _context.SaveChangesAsync();
            await OrderTotalUpdate(orderline.OrderId);
            return orderline;
        }

        public async Task Update(int orderlineId, OrderLine orderline) {
            if (orderlineId != orderline.Id) {
                throw new ArgumentException("product id does not match entry in table");
            }
            _context.Entry(orderline).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            await OrderTotalUpdate(orderline.OrderId);
        }

        public async Task Delete(int orderlineId) {
            OrderLine? orderline = await GetByPk(orderlineId);
            if (orderline is null) {
                throw new Exception("table entry does not exist, try different Id");
            }
            _context.OrderLines.Remove(orderline);
            await _context.SaveChangesAsync();
            await OrderTotalUpdate(orderline.OrderId);
        }
    }
}
