using Entities;

namespace Business
{
	public class SpecialOfferNumberOfProductsInThePriceOfLowerQuantity : ISpecialOffer
	{
		private readonly string ItemCode;

		private readonly int NumberOfUnitsToQualify;

		private readonly int NumberOfUnitsToCharge;

		public SpecialOfferNumberOfProductsInThePriceOfLowerQuantity(string itemCode, int numberOfUnitsToQualify, int numberOfUnitsToCharge)
		{
			this.ItemCode = itemCode;
			this.NumberOfUnitsToQualify = numberOfUnitsToQualify;
			this.NumberOfUnitsToCharge = numberOfUnitsToCharge;
		}

		public decimal GetBasketItemTotalForThisOffer(BasketItem basketItem)
		{
			var totalPriceToPayForProducts =
				this.CalculateTotalMoneyToPayForProductBundlesInPromotion(basketItem) +
				this.CalculateTotalMoneyToPayForProductsNotIncludedInPromotionBundle(basketItem);

			return totalPriceToPayForProducts;
		}

		private decimal CalculateTotalMoneyToPayForProductBundlesInPromotion(BasketItem basketItem)
		{
			int numberOfQualifyingBundles = basketItem.Quantity / this.NumberOfUnitsToQualify;

			var totalPriceForQualyfingBundles = numberOfQualifyingBundles * basketItem.Product.Price * this.NumberOfUnitsToCharge;

			return totalPriceForQualyfingBundles;
		}

		private decimal CalculateTotalMoneyToPayForProductsNotIncludedInPromotionBundle(BasketItem basketItem)
		{
			var numberOfUnitsNotIncludedInSpeciallOfferBundles = basketItem.Quantity % this.NumberOfUnitsToQualify;

			var totalPriceForNonQualifingUnits = basketItem.Product.Price * numberOfUnitsNotIncludedInSpeciallOfferBundles;

			return totalPriceForNonQualifingUnits;
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