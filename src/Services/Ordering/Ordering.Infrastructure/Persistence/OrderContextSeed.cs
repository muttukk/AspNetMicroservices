using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreconfiguredOrders());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {
                    UserName = "swn",
                    TotalPrice = 350,
                    FirstName = "Muttu",
                    LastName = "Kallimath",
                    EmailAddress = "muttuproex@gmail.com",
                    AddressLine = "Frankfort",
                    Country = "USA",
                    State = "KY",
                    ZipCode = "40601",
                    CardName ="Muttu Kallimath",
                    CardNumber="1234 5678 9876 5432",
                    Expiration="01/25",
                    CVV = "123",
                    PaymentMethod=0,
                    CreatedBy="swn",
                    CreatedDate=DateTime.Now,
                    LastModifiedBy="swn",
                    LastModifiedDate=DateTime.Now
                     }
            };
        }
    }
}
