using System;
using System.Collections.Generic;
using System.Text;

namespace VeritecAssignment.Business.SalaryPackage
{
    public sealed class RateCalculationResponse : IRateCalculationResponse
    {
        public decimal DeductionPerMonth { get; set; }
    }
}
