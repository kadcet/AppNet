using AppNET.Domain;
using AppNET.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.App
{
    public interface IProductService
    {
        void Created(int id, string categoryName, string productName, int productAmount, decimal productPurchasePrice,decimal productSalesPrice, decimal productTotalPrice);

        bool Deleted(int productId);
        
        IReadOnlyCollection<Product> GetAllProduct();

        Product Update(int productId, string categoryName, string newProductName, int productAmount, decimal productPurchasePrice, decimal productSalesPrice, decimal productTotalPrice);

        bool DeleteProductsByCategory(string categoryName);
    }

    
}
