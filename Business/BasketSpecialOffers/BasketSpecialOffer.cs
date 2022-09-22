using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace Business.BasketSpecialOffers
{
	public class BasketSpecialOfferCalculator
	{
		public const int NumberOfQualifyingProducts = 10;

		public const decimal DiscountToApplyForBasketTotal = 0.1m;

		public static bool IsBasketQualifying(Basket basket)
		{
			if (basket.TotalNumberOfUnitsInBasket >= NumberOfQualifyingProducts)
			{
				return true;
			}

			return false;
		}

		public static void ApplyBasketLevelPromotion(Basket basket)
		{
			if (BasketSpecialOfferCalculator.IsBasketQualifying(basket))
			{
				basket.BasketTotal -= (basket.BasketTotal * DiscountToApplyForBasketTotal);
			}
		}
	}
}
