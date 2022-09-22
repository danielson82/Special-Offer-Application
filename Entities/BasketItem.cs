namespace Entities
{
	public class BasketItem
	{
		public Product Product { get; set; }

		public int Quantity { get; set; }

		public decimal BasketItemsTotal { get; set; }
	}
}
