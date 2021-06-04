using System;
using System.Collections.Generic;
using System.Text;

namespace VeritecAssignment.Business.SalaryPackage
{
    public sealed class RateCalculationRequest : IRateCalculationRequest
    {
        public IRateBracket[] Rates { get; set; }
        public decimal YearlyEarnings { get; set; }
    }
}
