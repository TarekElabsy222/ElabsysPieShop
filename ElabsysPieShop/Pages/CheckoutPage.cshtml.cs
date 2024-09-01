using ElabsysPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElabsysPieShop.Pages
{
    public class CheckoutPageModel : PageModel
    {
        private readonly IOrderRepositiory _orderRepositiory;
        private readonly IShoppingCart _shoppingCart;

		public CheckoutPageModel(IOrderRepositiory orderRepositiory, IShoppingCart shoppingCart)
		{
			_orderRepositiory = orderRepositiory;
			_shoppingCart = shoppingCart;
		}

		public Order Order { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
			if (_shoppingCart.ShoppingCartItems.Count == 0)
			{
				ModelState.AddModelError("", "Your cart empty, add some pies first");
			}
			if (ModelState.IsValid)
			{
				_orderRepositiory.CreateOrder(Order);
				_shoppingCart.ClearCart();
				return RedirectToAction("CheckoutCompletePage");
			}
			return Page();
		}
    }
}
