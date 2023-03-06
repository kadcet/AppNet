using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.Infrastructure
{
    public class IOCContainer
    {
        
        private static Dictionary<Type,Func<object>> container=new Dictionary<Type,Func<object>>();

        //Çözümlemek
        public static T Resolve<T>()
        {
            #region UzunHali
            //var keyTipi = typeof(T);
            //var metod = container[keyTipi];
            //var nesne = metod();
            //var donusTipi=(T) nesne;
            //return donusTipi; 
            #endregion
            return (T)container[typeof(T)]();
        }

        public static void Register<T>(Func<object> func)
        {
            if(container.ContainsKey(typeof(T)))
                container.Remove(typeof(T));

            container.Add(typeof(T),func);
        }
        //Register => kayıt işlemi
        //Resolve => Çözümleme
    }
}
