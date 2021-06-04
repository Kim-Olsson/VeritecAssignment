using System;
using System.Collections.Generic;
using System.Text;

namespace VeritecAssignment.Business.SalaryPackage
{
    public class Deduction : IDeduction
    {
        public string Name { get; set; }

        public decimal DeductableAmount { get; set; }
    }
}
