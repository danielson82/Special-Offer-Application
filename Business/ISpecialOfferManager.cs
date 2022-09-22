using System.Collections.Generic;

namespace Business
{
	public interface ISpecialOfferManager
	{
		IList<ISpecialOffer> SpecialOffers { get; set; }
	}
}