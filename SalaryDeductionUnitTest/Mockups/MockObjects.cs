using System;
using System.Collections.Generic;
using System.Text;
using VeritecAssignment.Business.SalaryPackage;

namespace SalaryDeductionUnitTest.Mockups
{
    public class RateBracket : IRateBracket
    {
        public decimal YearlyEarningsLessThan { get; set; }
        public decimal Rate { get; set; }
        public bool Accumulative { get; set; } = true;
    }
    public class RateCalculationRequest : IRateCalculationRequest
    {
        public IRateBracket[] Rates { get; set; }
        public decimal YearlyEarnings { get; set; }
    }
    public class RateCalculationResponse : IRateCalculationResponse
    {
        public decimal DeductionPerMonth { get; set; } = default;
    }
    public class MockRepository : IRateRepository
    {
        private readonly RateBracketDTO[] incomeTaxBrackets = {
                new RateBracketDTO() { YearlyEarningsLessThan = 18201.00m, Rate = default },
                new RateBracketDTO() { YearlyEarningsLessThan = 37001, Rate = 0.19m },
                new RateBracketDTO() { YearlyEarningsLessThan = 87001.00m, Rate = 0.325m },
                new RateBracketDTO() { YearlyEarningsLessThan = 180001.00m, Rate = 0.37m },
                new RateBracketDTO() { YearlyEarningsLessThan = decimal.MaxValue, Rate = 0.47m },
            };

        private readonly RateBracketDTO[] budgetRepairBrackets = {
                new RateBracketDTO() { YearlyEarningsLessThan = 180001.00m, Rate = default },
                new RateBracketDTO() { YearlyEarningsLessThan = decimal.MaxValue, Rate = 0.02m },
            };

        private readonly RateBracketDTO[] medicareLevyBrackets = {
                new RateBracketDTO() { YearlyEarningsLessThan = 21336, Rate = default },
                new RateBracketDTO() { YearlyEarningsLessThan = 26669, Rate = 0.10m },
                new RateBracketDTO() { YearlyEarningsLessThan = decimal.MaxValue, Rate = 0.02m, Accumulative = false },
            };

        private readonly RateBracketDTO[] superBrackets = {
                new RateBracketDTO() { YearlyEarningsLessThan = decimal.MaxValue, Rate = 0.095m },
            };

        public IRateBracket[] GetBudgetRepairRates()
        {
            return (budgetRepairBrackets);
        }

        public IRateBracket[] GetBudgetRepairRates(string year)
        {
            return (GetBudgetRepairRates());
        }

        public IRateBracket[] GetIncomeTaxRates()
        {
            return (incomeTaxBrackets);
        }

        public IRateBracket[] GetIncomeTaxRates(string year)
        {
            return (GetIncomeTaxRates());
        }

        public IRateBracket[] GetMedicareLevyRates()
        {
            return (medicareLevyBrackets);
        }

        public IRateBracket[] GetMedicareLevyRates(string year)
        {
            return (GetMedicareLevyRates());
        }

        public IRateBracket[] GetSuperannuationRates()
        {
            return (superBrackets);
        }

        public IRateBracket[] GetSuperannuationRates(string year)
        {
            return (GetSuperannuationRates());
        }
    }
    sealed class RateBracketDTO : IRateBracket
    {
        public decimal YearlyEarningsLessThan { get; set; }
        public decimal Rate { get; set; }
        public bool Accumulative { get; set; } = true;
    }
}
