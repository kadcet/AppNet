using AppNET.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.App
{
    public interface ICaseSevice
    {
        void Inc(string income, int amount, decimal totalPrice);

        void Exp(string explanation, int amount, decimal totalPrice);

        IReadOnlyCollection<Case> CaseList(Func<Case, bool> expression = null);

        IReadOnlyCollection<Case> CaseListIncome(Func<Case, bool> expression = null);

        IReadOnlyCollection<Case> CaseListExplanation(Func<Case, bool> expression = null);

        decimal Balance();
        //decimal Balance()=> CaseListIncome().Sum(ı=>ı.Price)-CaseListExplanation().Sum(e=>e.Price);

        bool Deleted(int caseId);
    }
}
