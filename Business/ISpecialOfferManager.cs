using System.Collections.Generic;
using Entities;

namespace Business
{
	public interface ISpecialOfferManager
	{
		IList<ISpecialOffer> SpecialOffers { get; set; }

		ISpecialOffer FindBestSpecialOffer(BasketItem basketItem);
	}
}