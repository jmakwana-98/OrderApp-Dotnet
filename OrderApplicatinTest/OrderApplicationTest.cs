using Microsoft.EntityFrameworkCore;
using OrderApplication.Controllers;
using OrderApplication.Models;
using OrderApplication.MySQL;
using Xunit;

public class OrderApplicationTest
{
    private DbContextOptions<OrderContext> GetDbContextOptions()
    {
        return new DbContextOptionsBuilder<OrderContext>()
            .UseInMemoryDatabase(databaseName: "OrderTestDb")
            .Options;
    }

    [Fact]
    public async Task CanAddOrder()
    {
        var options = GetDbContextOptions();

        using (var context = new OrderContext(options))
        {
            var controller = new OrdersController(context);
            var order = new Order { LastName = "Test", Description = "Sample Order", Quantity = 5 };

            await controller.PostOrder(order);
        }

        using (var context = new OrderContext(options))
        {
            Assert.Equal(1, context.Orders.Count());
            Assert.Equal("Test", context.Orders.Single().LastName);
        }
    }
    
    [Fact]
    public async Task CanDeleteOrder()
    {
        var options = GetDbContextOptions();

        using (var context = new OrderContext(options))
        {
            var controller = new OrdersController(context);
            var order = new Order { LastName = "Test", Description = "Sample Order", Quantity = 5 };
            await controller.PostOrder(order);

            await controller.DeleteOrder(1);
        }

        using (var context = new OrderContext(options))
        {
            Assert.Empty(context.Orders); 
        }
    }
}
