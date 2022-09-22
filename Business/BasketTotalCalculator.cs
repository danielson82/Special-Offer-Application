using Business.BasketSpecialOffers;
using Entities;
using Repository;

namespace Business
{
	public class BasketTotalCalculator
	{
		public IProductRepository ProductRepository { get; set; }

		public SpecialOfferManager SpecialOfferManager { get; set; }

		public BasketTotalCalculator(IProductRepository productRepository)
		{
			this.ProductRepository = productRepository;
			this.SpecialOfferManager = new SpecialOfferManager();
		}

		public decimal GetBasketTotal(Basket basket)
		{
			foreach (var basketItem in basket.BasketItems)
			{
				basket.BasketTotal += this.CalculateTotalBasketItem(basketItem);
			}

			BasketSpecialOfferCalculator.ApplyBasketLevelPromotion(basket);

			return basket.BasketTotal;
		}

		public decimal CalculateTotalBasketItem(BasketItem basketItem)
		{
			var specialOffer = this.SpecialOfferManager.FindBestSpecialOffer(basketItem);

			if (specialOffer != null)
			{
				return specialOffer.GetBasketItemTotalForThisOffer(basketItem);
			}

			return basketItem.Product.Price * basketItem.Quantity;
		}
	}
}
