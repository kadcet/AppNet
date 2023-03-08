using AppNET.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.App
{
    public interface ICategoryService
    {
        void Created(int id, string name);

        bool Delete(int categoryId);
        bool Delete(Category entity);


        IReadOnlyCollection<Category> GetAllCategory();

        Category Update(int categoryId, string newCategoryName);
    }
}
