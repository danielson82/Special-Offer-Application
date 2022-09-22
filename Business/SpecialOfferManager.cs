using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace Business
{
	/// <summary>
	/// A £1.50
	/// B £2.00 3 for £5.00
	/// C £0.75 2 for the price of 1
	/// D £3.00 2 for £4.50, 3 for £7.00
	/// E £2.40 3 for the price of 2
	/// </summary>

	public class SpecialOfferManager : ISpecialOfferManager
	{
		public IList<ISpecialOffer> SpecialOffers { get; set; }

		public SpecialOfferManager()
		{
			this.SpecialOffers = new List<ISpecialOffer>();

			//Configure Fixed Price for X number of products
			this.SpecialOffers.Add(new SpecialOfferFixedPriceForNumberOfUnits("B", 3, 5.00m));
			this.SpecialOffers.Add(new SpecialOfferFixedPriceForNumberOfUnits("D", 2, 4.50m));
			this.SpecialOffers.Add(new SpecialOfferFixedPriceForNumberOfUnits("D", 3, 7.00m));

			// Configure X for Y products 
			this.SpecialOffers.Add(new SpecialOfferNumberOfProductsInThePriceOfLowerQuantity("C", 2, 1));
			this.SpecialOffers.Add(new SpecialOfferNumberOfProductsInThePriceOfLowerQuantity("E", 3, 2));
		}

		public ISpecialOffer FindBestSpecialOffer(BasketItem basketItem)
		{
			ISpecialOffer bestSpecialOffer = null;

			foreach(var specialOffer in this.SpecialOffers)
			{
				if (specialOffer.IsApplicable(basketItem))
				{
					if (bestSpecialOffer == null)
					{
						bestSpecialOffer = specialOffer;
					}
					else if (bestSpecialOffer.GetBasketItemTotalForThisOffer(basketItem) >
							specialOffer.GetBasketItemTotalForThisOffer(basketItem))
					{
						bestSpecialOffer = specialOffer;
					}
				}
			}

			return bestSpecialOffer;
		}
	}
}
