using AppNET.Domain.Entities;
using AppNET.Domain.Interfaces;
using AppNET.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using AppNET.Infrastructure.Controls;
using AppNET.Domain;
using System.Reflection;

namespace AppNET.App
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService()
        {
            _productRepository = IOCContainer.Resolve<IRepository<Product>>();
        }
        public void Created(int id, string categoryName, string productName, int productAmount, decimal productPurchasePrice, decimal productSalesPrice, decimal productTotalPrice, ProcessType type)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Ürün İsmi Boş Olamaz");
            var oldProductName = _productRepository.GetList().FirstOrDefault(p => p.Name == productName);
            if (oldProductName != null)
                return;

            Product product=new Product();
            product.Id = id;
            product.Name = MyExtensions.FirstLetterUppercase(productName);
            product.CategoryName =Convert.ToString(categoryName);
            product.CreatedDate = DateTime.Now;
            product.Amount = productAmount;
            product.PurchasePrice = productPurchasePrice;
            product.SalesPrice = productSalesPrice;
            product.TotalPrice= productTotalPrice;
            product.Typee = ProcessType.Expense;
            _productRepository.Add(product);
        }

        

        public bool Deleted(int productId)
        {
           return _productRepository.Remove(productId);
        }
 

        public IReadOnlyCollection<Product> GetAllProduct()
        {
            return _productRepository.GetList().ToList().AsReadOnly();
        }

        public Product Update(int productId, string categoryName, string newProductName, int productAmount, decimal productPurchasePrice, decimal productSalesPrice, decimal productTotalPrice)
        {
            if (string.IsNullOrWhiteSpace(newProductName))
                throw new ArgumentException("Ürün İsmi Boş Olamaz");

            Product product=new Product();
            product.Id=productId;
            product.Name = MyExtensions.FirstLetterUppercase(newProductName);
            product.CategoryName = categoryName;
            product.Amount = productAmount;
            product.PurchasePrice= productPurchasePrice;
            product.SalesPrice = productSalesPrice;
            product.TotalPrice = productTotalPrice;
            return _productRepository.Update(productId, product);

        }

        public Product Update(int productId, string categoryName, string newProductName, int productAmount, decimal productPurchasePrice, decimal productSalesPrice, decimal productTotalPrice, ProcessType type)
        {
            if (string.IsNullOrWhiteSpace(newProductName))
                throw new ArgumentException("Ürün İsmi Boş Olamaz");

            Product product = new Product();
            product.Id = productId;
            product.Name = MyExtensions.FirstLetterUppercase(newProductName);
            product.CategoryName = categoryName;
            product.Amount = productAmount;
            product.PurchasePrice = productPurchasePrice;
            product.SalesPrice = productSalesPrice;
            product.TotalPrice = productTotalPrice;
            product.Typee = ProcessType.Income;
            return _productRepository.Update(productId, product);
        }





        public bool DeleteProductsByCategory(string categoryName)
        {
            var list = _productRepository.GetList().Where(x => x.CategoryName == categoryName).ToList();
            foreach (var item in list)
            {
                _productRepository.Remove(item.Id);
            }
            return true;
        }

        
    }
}
