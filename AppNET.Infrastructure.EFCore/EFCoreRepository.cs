using AppNET.Domain.Entities.Base;
using AppNET.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.Infrastructure.EFCore
{
    public class EFCoreRepository<T> : IRepository<T> where T : BaseEntity
    {
        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetBeyId(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> GetList(Func<T, bool> expression = null)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public T Update(int id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
