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

        public decimal Balance()
        {
            return CaseListIncome().Sum(i => i.Price) - CaseListExplanation().Sum(e => e.Price);
        }

        public IReadOnlyCollection<Case> CaseList(Func<Case, bool> expression = null)
        {
            return _caseRepository.GetList().ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Case> CaseListExplanation(Func<Case, bool> expression = null)
        {
            return _caseRepository.GetList().Where(x => x.ProcessType == Domain.ProcessType.Expense).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Case> CaseListIncome(Func<Case, bool> expression = null)
        {
            return _caseRepository.GetList ().Where(x => x.ProcessType == Domain.ProcessType.Income).ToList().AsReadOnly();
        }

        public bool Deleted(int caseId)
        {
            return _caseRepository.Remove(caseId);
        }

        public void Exp(string explanation, int amount, decimal totalPrice)
        {
            Case caseexpense = new Case();
            caseexpense.ProcessType = Domain.ProcessType.Expense;
            caseexpense.Explanation= explanation;
            caseexpense.Price= totalPrice;
            _caseRepository.Add(caseexpense);
        }

        public void Inc(string income, int amount, decimal totalPrice)
        {
            Case caseexpense = new Case();
            caseexpense.ProcessType = Domain.ProcessType.Income;
            caseexpense.Explanation = income;
            caseexpense.Price = totalPrice;
            _caseRepository.Add(caseexpense);
        }
    }
}
