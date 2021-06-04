using System;
using System.Collections.Generic;
using System.Text;

namespace VeritecAssignment.Business.SalaryPackage
{
    public interface IRateBracket
    {
        decimal YearlyEarningsLessThan { get; set; }
        decimal Rate { get; set; }
        bool Accumulative { get; set; }
    }
    public interface IRateCalculationRequest
    {
        IRateBracket[] Rates { get; set; }
        decimal YearlyEarnings { get; set; }
    }
    public interface IRateCalculationResponse
    {
        decimal DeductionPerMonth { get; set; }
    }
    public interface IRateCalculator
    {
        IRateCalculationResponse CalculateRate(IRateCalculationRequest Request);
        IRateCalculationResponse CalculateRate(IRateCalculationRequest Request, IRateCalculationResponse Response);
        IRateBracket GetRateBracket(IRateCalculationRequest Request);
    }
}
