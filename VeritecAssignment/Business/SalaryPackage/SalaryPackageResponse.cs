using System;
using System.Collections.Generic;
using System.Text;

namespace VeritecAssignment.Business.SalaryPackage
{
    public sealed class SalaryPackageResponse : ISalaryPackageResponse
    {
        public decimal GrossSalary { get; set; }
        public PayFrequencyEnum PayFrequency { get; set; }
        public ICollection<IDeduction> GrossSalaryDeductions { get; set; }
        public decimal TaxableIncome { get; set; }
        public ICollection<IDeduction> TaxableIncomeDeductions { get; set; }
        public decimal NetIncome { get; set; }
        public decimal PayPacket { get; set; }
    }
}
