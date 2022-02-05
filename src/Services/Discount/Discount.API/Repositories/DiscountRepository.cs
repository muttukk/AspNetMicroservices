using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        //private readonly IConfiguration configuration;

        //public DiscountRepository(IConfiguration _configuration)
        //{
        //    configuration = _configuration ?? throw new ArgumentNullException(nameof(_configuration)); ;
        //}

        //public async Task<bool> CreateDiscount(Coupon coupon)
        //{
        //    using NpgsqlConnection connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        //    var affected= await connection.ExecuteAsync
        //        ("INSERT INTO Coupon(productname,description,amount) VALUES (@ProductName,@Description,@Amount)",
        //        new { ProductName = coupon.ProductName,Description=coupon.Description,Amount=coupon.Amount });

        //    if (affected == 0)
        //        return false;

        //    return true;
        //}

        //public async Task<bool> DeleteDiscount(string productName)
        //{
        //    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        //    var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
        //        new { ProductName = productName });
        //    if (affected == 0)
        //        return false;

        //    return true;
        //}

        //public async Task<Coupon> GetDiscount(string productName)
        //{
        //   using NpgsqlConnection connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        //    Coupon coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
        //        ("SELECT id,productname,description,amount FROM Coupon WHERE productname=@ProductName", new { ProductName = productName });
        //    if (coupon==null)
        //    {
        //        return new Coupon { ProductName="No Discount",Amount=0,Description="No Discount" };
        //    }
        //    return coupon;
        //}

        //public async Task<bool> UpdateDiscount(Coupon coupon)
        //{
        //    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        //    var affected = await connection.ExecuteAsync
        //            ("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
        //                    new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

        //    if (affected == 0)
        //        return false;

        //    return true;
        //}

        public string HelloWorld()
        {
            return "Hello World!!!";
        }
    }
}
