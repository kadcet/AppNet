using AppNET.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.Domain.Entities
{
    public sealed class Product : AuditEntity
    {
        public string Name { get; set; }

        public int Amount { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SalesPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public string CategoryName { get; set; }
        //public int CategoryId { get; set; }

        public ProcessType Typee { get; set; }
    }
}

