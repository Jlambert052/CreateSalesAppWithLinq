using CreateSalesAppWithLinq.Controllers;
using CreateSalesAppWithLinq.Models;

Console.WriteLine("Sales App Time go BRRRRRRR");

AppDbContext _context = new();
CustomerController custCtrl = new(_context);
OrderController ordCtrl = new(_context);
ProductController prodCtrl = new(_context);
OrderLineController ordlCtrl = new(_context);

/*
(await ordCtrl.GetOrderbyCxCode("BBUY")).ToList().ForEach(o => Console.WriteLine($"{o.Description}"));

var orders = await ordCtrl.GetOrderByOrdl(4);

orders.ToList().ForEach(o => Console.WriteLine($"{o.Description}"));
*/

/*
var mOrder = await ordCtrl.GetByPk(4);
Console.WriteLine($"Before the change; {mOrder.Status}");

await ordCtrl.UpdateStatusClosed(mOrder.Id, mOrder);

mOrder = await ordCtrl.GetByPk(4);
Console.WriteLine($"After the change; {mOrder.Status}");
*/

/*
var targtwl = await ordCtrl.GetByPk(8);
Console.WriteLine($"The order status is currently {targtwl.Status}");

await ordCtrl.UpdateStatusInProcess(targtwl.Id, targtwl);
Console.WriteLine($"The order status is currently {targtwl.Status}");

var micrBuild = await ordCtrl.GetByPk(7);
Console.WriteLine($"The order status is currently {micrBuild.Status}");

await ordCtrl.UpdateStatusInProcess(micrBuild.Id, micrBuild);
Console.WriteLine($"The order status is currently {micrBuild.Status}");
*/
/*
var orderLine = await ordlCtrl.GetByPk(4);

orderLine.Quantity = 2;

await ordlCtrl.Update(orderLine.Id, orderLine);
*/

/* add new product and check its properties on the db
Product? prodGPU1 = new() {
    Name = "3060ti", Price = 600
};
await prodCtrl.Insert(prodGPU1);
Console.WriteLine(prodGPU1);
*/

//add an order with the new products

/* add new order and associated orderlines; not sure 
 * 
Order? micr1 = new() {
    Description = "GPU order", Total = 14000, Status = "New", Date = DateTime.Now, CustomerId = 8
};

OrderLine ? micrA = new() {
    OrderId = 10, ProductId = 7, Quantity = 10
};

OrderLine? micrB = new() {
    OrderId = 10, ProductId = 8, Quantity = 10
};

await ordlCtrl.Insert(micrA);
await ordlCtrl.Insert(micrB);

*/
/* adjusting the order on file
var micOrd = await ordlCtrl.GetByPk(11);

micOrd.Quantity = 20;

await ordlCtrl.Update(micOrd.Id, micOrd);
*/

var gpuOrd = await ordCtrl.GetByPk(10);

await ordCtrl.UpdateStatusInProcess(gpuOrd.Id, gpuOrd);