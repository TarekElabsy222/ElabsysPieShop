
using Microsoft.EntityFrameworkCore;

namespace ElabsysPieShop.Models
{
    public class ShoppingCartRepository : IShoppingCart
    {
        private readonly ElabsysPieShopDbContext _context;

        private ShoppingCartRepository(ElabsysPieShopDbContext context)
        {
            _context = context;
        }

        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>
                ()?.HttpContext?.Session;
            ElabsysPieShopDbContext context = services.GetService<ElabsysPieShopDbContext>
                () ?? throw new Exception("Error Initializing");
            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);
            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }
        public void AddToCart(Pie pie)
        {
            var shoppingCartItem =
                _context.ShoppingCartItems.SingleOrDefault(
                    s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem= new ShoppingCartItem()
                {
                    ShoppingCartId= ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _context.ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);
            _context.ShoppingCartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??=
                _context.ShoppingCartItems.Where(c=>c.ShoppingCartId==ShoppingCartId)
                .Include(s=>s.Pie)
                .ToList();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(c =>
            c.ShoppingCartId == ShoppingCartId)
                .Select(c=> c.Pie.Price * c.Amount).Sum();
            return total;
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                _context.ShoppingCartItems.SingleOrDefault(
                    s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);
            var localAmount = 0;
            if(shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
            return localAmount;
        }
    }
}
