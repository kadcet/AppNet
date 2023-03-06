using AppNET.Domain.Entities;
using AppNET.Domain.Interfaces;
using AppNET.Infrastructure;
using AppNET.Infrastructure.EFCore;
using AppNET.Infrastructure.IOToTXT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.App
{

    // Form.Cs de bu class çağırılacak.Burada Infrastructure katmanındaki IOCContainer clasındaki Register metodunu çağırıp kayıt edilir.
    public class ApplicationServiceSettings
    {
        public static void RegisterAllService()
        {
            #region Temp
            //IOCContainer.Register("bir", 1);
            //IOCContainer.Register("bir", 11);
            //IOCContainer.Register("iki", 2);
            //IOCContainer.Register("üç", 3);
            //IOCContainer.Register("dört", 4); 
            #endregion

            IOCContainer.Register<IRepository<Category>>(()=>new TextFileRepository<Category>());
            IOCContainer.Register<IRepository<Product>>(()=> new TextFileRepository<Product>());
            //IOCContainer.Register<IRepository<Category>>(() => new EFCoreRepository<Category>());
            IOCContainer.Register<ICategoryService>(()=> new CategoryService());
            IOCContainer.Register<IProductService>(() => new ProductService());
        }

        //public static IRepository<Category> Metod()
        //{
        //    return new TextFileRepository<Category>();
        //}
    }
}
