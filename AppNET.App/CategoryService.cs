using AppNET.Domain.Entities;
using AppNET.Domain.Interfaces;
using AppNET.Infrastructure;
using AppNET.Infrastructure.IOToTXT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppNET.App
{
    public class CategoryService : ICategoryService
    {

        private readonly IRepository<Category> _repositoryCategory;

        public CategoryService()
        {
            _repositoryCategory=IOCContainer.Resolve<IRepository<Category>>();
        }


        public void Created(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Kategori Adı Boş Olamaz");

            var oldCategory = _repositoryCategory.GetList().FirstOrDefault(c => c.Name == name);
            if (oldCategory != null)
                return;

            Category category=new Category();
            category.Id = id;
            category.Name=name.ToUpper();//yeni kayıt edilen bütün categoryler büyük harfle kayıt edilecek
            _repositoryCategory.Add(category);
        }

        public bool Delete(int categoryId)
        {
            return _repositoryCategory.Remove(categoryId);
        }

        public IReadOnlyCollection<Category> GetAllCategory()
        {
            return _repositoryCategory.GetList().ToList().AsReadOnly();
        }

        public Category Update(int categoryId, string newCategoryName)
        {
            if (string.IsNullOrWhiteSpace(newCategoryName))
                throw new ArgumentNullException("Kategori Adı Boş Olamaz");

            var category=new Category();
            category.Id=categoryId;
            category.Name=newCategoryName.ToUpper();
            return _repositoryCategory.Update(categoryId, category);
            // categoryId => güncellemek istediğim category nin Id si // category ise yeni değerleriyle güncellenen category
        }
    }
}
