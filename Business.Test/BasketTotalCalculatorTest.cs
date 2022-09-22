using System.Collections.Generic;
using Entities;
using NUnit.Framework;
using Repository;

namespace Business.Test
{
	[TestFixture]
	public class BasketTotalCalculatorTest
	{
		public IList<Product> SetupProducts()
		{
			List<Product> products = new List<Product>();

			products.Add(new Product { ItemCode = "A", Price = 1.50m });
			products.Add(new Product { ItemCode = "B", Price = 2.00m });
			products.Add(new Product { ItemCode = "C", Price = 0.75m });
			products.Add(new Product { ItemCode = "D", Price = 3.00m });
			products.Add(new Product { ItemCode = "E", Price = 2.40m });

			return products;
		}

		[Test]
		public void Test_Case_1_When_Five_Products_Are_Added_And_Do_Not_Qualify_For_Promotion_Then_Total_Is_Based_On_Normal_Price()
		{
			// Test Case 1
			// A, B, C, D, E 

			var basketManager = new BasketManager(new Basket());

			basketManager.AddProduct("A");
			basketManager.AddProduct("B");
			basketManager.AddProduct("C");
			basketManager.AddProduct("D");
			basketManager.AddProduct("E");

			var basketTotalCalculator = this.CreateBasketTotalCalculator();

			var basketTotal = basketTotalCalculator.GetBasketTotal(basketManager.Basket);

			Assert.That(basketTotal, Is.EqualTo(9.65m));
		}

		[Test]
		public void Test_Case_2_When_Multiple_Quantities_Are_Added_Then_Promotion_Is_Applied_Only_When_Enough_Has_Been_Purchased()
		{
			// Test Case 2
			// A, B, B, C, C
			//  C - qualifies for 2 for price for 1 quantity 

			var basketManager = new BasketManager(new Basket());

			basketManager.AddProduct("A");
			basketManager.AddProduct("B", 2);
			basketManager.AddProduct("C", 2);

			var basketTotalCalculator = this.CreateBasketTotalCalculator();

			var basketTotal = basketTotalCalculator.GetBasketTotal(basketManager.Basket);

			Assert.That(basketTotal, Is.EqualTo(6.25m));
		}

		[Test]
		public void Test_Case_3_When_Three_Products_And_Two_Products_Qualify_For_Fixed_Price_Promo_Then_Two_Promotions_Are_Applied_Correctly()
		{
			// Test Case 3
			//B, D, D, B, B, E, E 
			// B qualifies for 3 for fixed price promo
			// D qualfies for 2 for fixed price promo
			// E do not qualify for any promo

			var basketManager = new BasketManager(new Basket());

			basketManager.AddProduct("B", 3);
			basketManager.AddProduct("D", 2);
			basketManager.AddProduct("E", 2);

			var basketTotalCalculator = this.CreateBasketTotalCalculator();

			var basketTotal = basketTotalCalculator.GetBasketTotal(basketManager.Basket);

			Assert.That(basketTotal, Is.EqualTo(14.30m));
		}

		[Test]
		public void Test_Case_4()
		{
			// Test Case 4
			// D, A, B, B, D, D
			// A - nothing
			// B - nothing 
			// D - 3 for fixed price 

			// This should be 
			// 7 pound ( D 3for 7Pounds)
			// 1.50 (A for 1.50)
			// 4 pounds (B for 2) - 2 x 2f
			// 12.50

			var basketManager = new BasketManager(new Basket());

			basketManager.AddProduct("A");
			basketManager.AddProduct("B", 2);
			basketManager.AddProduct("D", 3);

			var basketTotalCalculator = this.CreateBasketTotalCalculator();

			var basketTotal = basketTotalCalculator.GetBasketTotal(basketManager.Basket);

			Assert.That(basketTotal, Is.EqualTo(12.50m));
		}

		[Test]
		public void Test_Case_5()
		{
			// Test Case 5
			// D, C, D, E, E, E, C, C, D, D
			// C - qualfies for 2 for 1 + 1 unit without promo - 
			// D - qualifies for 2x2for1 or (3 for 1 + 1 unit without promo). 2x2 for 1 is cheaper 
			// E - qualfies for 3 for the price of 2 
			// Total 13.77

			// C -> two for price of one (2x1) 0.75 + 0.75 = 1.50
			// D 4 units -> 2x (2for1) = 2x4.50 = 9 pounds 
			// E 3 units -> (3 for 2) = 2 x 2.40 = 4.80

			// Summary 15.30 - 1.53 (10 percent) = 13.77

			var basketManager = new BasketManager(new Basket());

			basketManager.AddProduct("C", 3);
			basketManager.AddProduct("D", 4);
			basketManager.AddProduct("E", 3);

			var basketTotalCalculator = this.CreateBasketTotalCalculator();

			var basketTotal = basketTotalCalculator.GetBasketTotal(basketManager.Basket);

			Assert.That(basketTotal, Is.EqualTo(13.77m));
		}

		private BasketTotalCalculator CreateBasketTotalCalculator()
		{
			return new BasketTotalCalculator(new ProductRepository(), new SpecialOfferManager());
		}
	}
}
