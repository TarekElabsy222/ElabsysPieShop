using ElabsysPieShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElabsysPieShop.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IOrderRepositiory _orderRepositiory;
		private readonly IShoppingCart _shoppingCart;

		public OrderController(IOrderRepositiory orderRepositiory, IShoppingCart shoppingCart)
		{
			_orderRepositiory = orderRepositiory;
			_shoppingCart = shoppingCart;
		}

		public IActionResult Checkout()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;
			if(_shoppingCart.ShoppingCartItems.Count == 0)
			{
				ModelState.AddModelError("", "Your cart empty, add some pies first");
			}
			if(ModelState.IsValid)
			{
				_orderRepositiory.CreateOrder(order);
				_shoppingCart.ClearCart();
				return RedirectToAction("CheckoutComplete");
			}
			return View(order);
		}
		public IActionResult CheckoutComplete()
		{
			ViewBag.CheckoutCompleteMessage = "Thanks for your order. You'll soon enjoy our delicious pies";
			return View();
		}
	}
}
