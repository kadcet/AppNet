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

namespace AppNET.App
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService()
        {
            _productRepository = IOCContainer.Resolve<IRepository<Product>>();
        }
        public void Created(int id, string categoryName, string productName, int productStock, decimal productPrice)
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
            product.Price = productPrice;
            product.Stock = productStock;
            _productRepository.Add(product);
        }

        

        public void Deleted(int productId)
        {
            _productRepository.Remove(productId);
        }

        public void Deleted(Product entity)
        {
            _productRepository.Remove(entity);
        }

        public IReadOnlyCollection<Product> GetAllProduct()
        {
            return _productRepository.GetList().ToList().AsReadOnly();
        }

        public Product Update(int productId,string categoryName, string newProductName,int stock,decimal price)
        {
            if (string.IsNullOrWhiteSpace(newProductName))
                throw new ArgumentException("Ürün İsmi Boş Olamaz");

            Product product=new Product();
            product.Id=productId;
            product.Name = MyExtensions.FirstLetterUppercase(newProductName);
            product.CategoryName = categoryName;
            product.Stock = stock;
            product.Price = price;
            return _productRepository.Update(productId, product);

            

        }
    }
}
