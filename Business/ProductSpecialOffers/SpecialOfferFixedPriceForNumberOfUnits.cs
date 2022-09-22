using Entities;

namespace Business
{
	public class SpecialOfferFixedPriceForNumberOfUnits : ISpecialOffer
	{
		private readonly string ItemCode;

		private readonly int NumberOfUnitsToQualify;

		private readonly decimal FixedTotalPriceForUnits;
	
		public SpecialOfferFixedPriceForNumberOfUnits(string itemCode, int numberOfUnitsToQualify, decimal fixedPriceForUnits)
		{
			this.ItemCode = itemCode;
			this.NumberOfUnitsToQualify = numberOfUnitsToQualify;
			this.FixedTotalPriceForUnits = fixedPriceForUnits;
		}
		
		public decimal GetBasketItemTotalForThisOffer(BasketItem basketItem)
		{
			var totalNumberOfUnitsInBasket = basketItem.Quantity;

			if (totalNumberOfUnitsInBasket == this.NumberOfUnitsToQualify)
			{
				return this.FixedTotalPriceForUnits;
			}

			var numberOfPromotionSets = totalNumberOfUnitsInBasket / this.NumberOfUnitsToQualify;

			var totalMoneyForPromotionBundles = numberOfPromotionSets * this.FixedTotalPriceForUnits;

			var numberOfUnitsWithoutPromotion = basketItem.Quantity % this.NumberOfUnitsToQualify;

			var totalMoneyForBasketItemWithoutPromotion = basketItem.Product.Price * numberOfUnitsWithoutPromotion;

			return totalMoneyForPromotionBundles + totalMoneyForBasketItemWithoutPromotion;
		}

		public bool IsApplicable(BasketItem basketItem)
		{
			if (basketItem.Product.ItemCode == this.ItemCode &&
				basketItem.Quantity >= this.NumberOfUnitsToQualify)
			{
				return true;
			}

			return false;
		}
	}
}
