using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalaryDeductionUnitTest.Mockups;
using VeritecAssignment.Business.SalaryPackage;

namespace SalaryDeductionUnitTest
{
    [TestClass]
    public class RateCalculatorTest
    {
        private readonly IRateBracket[] incomeTaxBrackets = {
                new RateBracket() { YearlyEarningsLessThan = 18201.00m, Rate = 0.00m },
                new RateBracket() { YearlyEarningsLessThan = 37001, Rate = 0.19m },
                new RateBracket() { YearlyEarningsLessThan = 87001.00m, Rate = 0.325m },
                new RateBracket() { YearlyEarningsLessThan = 180001.00m, Rate = 0.37m },
                new RateBracket() { YearlyEarningsLessThan = decimal.MaxValue, Rate = 0.47m }
            };
        private readonly IRateBracket[] budgetRepairBrackets = {
                new RateBracket() { YearlyEarningsLessThan = 180001.00m, Rate = 0.00m },
                new RateBracket() { YearlyEarningsLessThan = decimal.MaxValue, Rate = 0.02m }
            };
        private readonly IRateBracket[] medicareLevyBrackets = {
                new RateBracket() { YearlyEarningsLessThan = 21336, Rate = 0.00m },
                new RateBracket() { YearlyEarningsLessThan = 26669, Rate = 0.10m },
                new RateBracket() { YearlyEarningsLessThan = decimal.MaxValue, Rate = 0.02m, Accumulative = false }
            };
        [TestMethod]
        [DataRow("18200.00", "0.00")]
        [DataRow("25000.00", "1292.00")]
        [DataRow("45000.00", "6172.00")]
        [DataRow("95000.00", "22782.00")]
        [DataRow("200000.00", "63632.00")]

        public void VerifyRateCalculator(string YearlyEarnings, string MonthlyDeduction)
        {
            decimal monthlyDeduction = decimal.Parse(MonthlyDeduction);

            IRateCalculator calc = new RateCalculator();
            IRateCalculationRequest request = new Mockups.RateCalculationRequest() { YearlyEarnings = decimal.Parse(YearlyEarnings), Rates = incomeTaxBrackets };

            IRateCalculationResponse response = calc.CalculateRate(request);
            Assert.AreEqual(response.DeductionPerMonth, monthlyDeduction);
        }
        [TestMethod]
        [DataRow("180000.00", "0.00")]
        [DataRow("200000.00", "400.00")]
        public void VerifyBudgetRepairRateCalculator(string YearlyEarnings, string MonthlyDeduction)
        {
            decimal monthlyDeduction = decimal.Parse(MonthlyDeduction);

            IRateCalculator calc = new RateCalculator();
            IRateCalculationRequest request = new Mockups.RateCalculationRequest() { YearlyEarnings = decimal.Parse(YearlyEarnings), Rates = budgetRepairBrackets };

            IRateCalculationResponse response = calc.CalculateRate(request);
            Assert.AreEqual(response.DeductionPerMonth, monthlyDeduction);
        }
        [TestMethod]
        [DataRow("21335.00", "0.00")]
        [DataRow("25000.00", "366.50")]
        [DataRow("40000.00", "800.00")]
        public void VerifyMedicareLevyRateCalculator(string YearlyEarnings, string MonthlyDeduction)
        {
            decimal monthlyDeduction = decimal.Parse(MonthlyDeduction);

            IRateCalculator calc = new RateCalculator();
            IRateCalculationRequest request = new Mockups.RateCalculationRequest() { YearlyEarnings = decimal.Parse(YearlyEarnings), Rates = medicareLevyBrackets };

            IRateCalculationResponse response = calc.CalculateRate(request);
            Assert.AreEqual(response.DeductionPerMonth, monthlyDeduction);
        }
        [TestMethod]
        [DataRow("65000.00", "5,639.27")]

        public void VerifySuperannuationRateCalculator(string YearlyEarnings, string SuperContribution)
        {
            decimal superContribution = decimal.Parse(SuperContribution);

            var calc = new SuperContribution(new MockRepository());
            ISalaryPackageRequest request = new SalaryPackageRequest() { GrossSalary = decimal.Parse(YearlyEarnings) };

            IDeduction response = calc.Calculate(request);
            Assert.AreEqual(response.DeductableAmount, superContribution);
        }
    }
}
