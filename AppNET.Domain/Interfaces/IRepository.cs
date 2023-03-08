using AppNET.Domain.Entities;
using AppNET.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Add(T entity);

        bool Remove(int id);

        bool Remove(T entity);

        // Geriye tek bir entity almak istediğim zaman
        T GetBeyId(int id);

        // liste olarak bütün datayı almak için
        ICollection<T> GetList(Func<T,bool> expression=null);

        T Update(int id,T entity);

    }
}
