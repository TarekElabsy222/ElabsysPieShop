namespace ElabsysPieShop.Models
{
	public class OrderRepositiory : IOrderRepositiory
	{
		private readonly ElabsysPieShopDbContext _context;
		private readonly IShoppingCart _shoppingCart;

		public OrderRepositiory(ElabsysPieShopDbContext context, IShoppingCart shoppingCart)
		{
			_context = context;
			_shoppingCart = shoppingCart;
		}

		public void CreateOrder(Order order)
		{
			List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
			order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
			order.OrderDetails = new List<OrderDetail>();
			foreach(ShoppingCartItem? shoppingCartItem in shoppingCartItems)
			{
				var orderDetail = new OrderDetail
				{
					Amount = shoppingCartItem.Amount,
					PieId = shoppingCartItem.Pie.PieId,
					Price = shoppingCartItem.Pie.Price
				};
				order.OrderDetails.Add(orderDetail);
			}
			_context.Orders.Add(order);
			_context.SaveChanges();
		}
	}
}
