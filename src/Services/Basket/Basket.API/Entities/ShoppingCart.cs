namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }

        public List<ShopingCartItem> Items { get; set; } = new List<ShopingCartItem>();

        public ShoppingCart()
        {
        }
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                   totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
