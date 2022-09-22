using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Entities;

namespace Repository
{
	public class ProductRepository : IProductRepository
	{
		private IList<Product> Products { get; set; }

		public ProductRepository()
		{
			this.Products = new List<Product>();

			this.Products.Add(new Product { ItemCode = "A", Price = 1.50m });
			this.Products.Add(new Product { ItemCode = "B", Price = 2.00m });
			this.Products.Add(new Product { ItemCode = "C", Price = 0.75m });
			this.Products.Add(new Product { ItemCode = "D", Price = 3.00m });
			this.Products.Add(new Product { ItemCode = "E", Price = 2.40m });
		}

		public Product GetProduct(string itemCode)
		{
			Guard.Against.Null(itemCode, nameof(itemCode));

			var product = this.Products.FirstOrDefault(m => m.ItemCode == itemCode.ToUpper());

			return product;
		}
	}
}
