using Entities;

namespace Business
{
	public interface ISpecialOffer
	{
		bool IsApplicable(BasketItem basketItem);

		decimal GetBasketItemTotalForThisOffer(BasketItem basketItem);
	}
}