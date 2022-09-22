using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Repository;

namespace Business
{
	public class BasketManager
	{
		public Basket Basket { get; set; }

		public IProductRepository ProductRepository { get; set; }

		public BasketManager(Basket basket)
		{
			this.ProductRepository = new ProductRepository();

			this.Basket = basket;
		}

		public void AddProduct(string itemCode)
		{
			this.AddProduct(itemCode, quantity:1);
		}

		public void AddProduct(string itemCode, int quantity)
		{
			if (this.Basket == null)
			{
				this.Basket = new Basket();
			}
			
			var product = this.ProductRepository.GetProduct(itemCode);

			this.AddProduct(product, quantity);
		}

		public void AddProduct(Product product, int quantity)
		{
			var productAlreadyInTheBasket = this.Basket.BasketItems.FirstOrDefault(m => m.Product.ItemCode == product.ItemCode);

			if (productAlreadyInTheBasket != null)
			{
				productAlreadyInTheBasket.Quantity += quantity;
			}
			else
			{
				this.Basket.BasketItems.Add(new BasketItem { Product = product, Quantity = quantity });
			}
		}
	}
}
