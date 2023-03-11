using AppNET.Domain.Entities;
using AppNET.Domain.Interfaces;
using AppNET.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.App
{
    public class CaseService:ICaseSevice
    {
        private readonly IRepository<Case> _caseRepository;

        public CaseService()
        {
            _caseRepository = IOCContainer.Resolve<IRepository<Case>>();
        }

    }
}
