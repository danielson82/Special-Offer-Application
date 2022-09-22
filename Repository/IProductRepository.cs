using Entities;

namespace Repository
{
	public interface IProductRepository
	{
		Product GetProduct(string productCode);
	}
}