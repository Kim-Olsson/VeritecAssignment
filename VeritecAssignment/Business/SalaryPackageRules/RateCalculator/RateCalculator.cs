using System;
using System.Collections.Generic;
using System.Text;

namespace VeritecAssignment.Business.SalaryPackage
{
    public class RateCalculator : IRateCalculator
    {
        public RateCalculator()
        {
        }
        public IRateCalculationResponse CalculateRate(IRateCalculationRequest Request)
        {
            IRateCalculationResponse response = new RateCalculationResponse();

            CalculateRate(Request, response);
            return (response);
        }
        public IRateCalculationResponse CalculateRate(IRateCalculationRequest Request, IRateCalculationResponse Response)
        {
            decimal lowerThreshold = default;
            
            foreach (IRateBracket bracket in Request.Rates)
            {
                decimal deduction;

                if ((Request.YearlyEarnings < bracket.YearlyEarningsLessThan) || (bracket.YearlyEarningsLessThan == default))
                {

                    if (bracket.Accumulative)
                    {
                        deduction = (Request.YearlyEarnings - lowerThreshold) * bracket.Rate;
                        Response.DeductionPerMonth += deduction;
                    }
                    else
                    {
                        deduction = Request.YearlyEarnings * bracket.Rate;
                        Response.DeductionPerMonth = deduction;
                    }
                    return (Response);
                }
                else
                {
                    deduction = ((bracket.YearlyEarningsLessThan - 1) - lowerThreshold) * bracket.Rate;
                    Response.DeductionPerMonth += deduction;
                }
                lowerThreshold = bracket.YearlyEarningsLessThan - 1;
            }
            return (Response);
        }
        public IRateBracket GetRateBracket(IRateCalculationRequest Request)
        {
            foreach (IRateBracket bracket in Request.Rates)
            {
                if (Request.YearlyEarnings < bracket.YearlyEarningsLessThan)
                    return (bracket);
            }
            return (null);
        }
    }
}
