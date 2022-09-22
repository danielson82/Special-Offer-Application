using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public class Basket
	{
		public Basket()
		{
			this.BasketItems = new List<BasketItem>();
		}

		public IList<BasketItem> BasketItems { get; }

		public int TotalNumberOfUnitsInBasket 
		{
			get
			{
				if (this.BasketItems != null)
				{
					return BasketItems.Sum(m => m.Quantity);
				}

				return 0;
			}
		}

		public decimal BasketTotal { get; set; }
	}
}
