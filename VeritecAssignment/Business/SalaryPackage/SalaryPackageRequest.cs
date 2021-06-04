using System;
using System.Collections.Generic;
using System.Text;

namespace VeritecAssignment.Business.SalaryPackage
{
    public sealed class SalaryPackageRequest : ISalaryPackageRequest
    {
        public decimal GrossSalary { get; set; }
        public PayFrequencyEnum PayFrequency { get; set; }
        public decimal TaxableIncome { get; set; }
    }
}
