namespace ElabsysPieShop.Models
{
	public class OrderDetail
	{
		public int OrderDetailId { get; set; }
		public int OrderId { get; set; }
		public int PieId { get; set; }
		public int Amount { get; set; }
		public decimal Price { get; set; }
		public Pie Pie { get; set; } = default!;
		public Order Order { get; set; } = default!;
	}
}
