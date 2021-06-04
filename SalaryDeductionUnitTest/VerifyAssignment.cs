using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalaryDeductionUnitTest.Mockups;
using VeritecAssignment.Business.SalaryPackage;

namespace SalaryDeductionUnitTest
{
    [TestClass]
    public class VerifyAssignment
    {
        [TestMethod]
        [DataRow("65000.00", "5639.27", "59360.73", "1188.00", "0.00", "10839.00", "47333.73", "3944.48")]
        public void VerifyPackageCalculation(string GrossSalary, string Super, string TaxableIncome, string MedicareLevy, string BudgetRepairLevy, string IncomeTax, string NetIncome, string PayPacket)
        {
            decimal grossSalary = decimal.Parse(GrossSalary);
            decimal super = decimal.Parse(Super);
            decimal taxableIncome = decimal.Parse(TaxableIncome);
            decimal medicareLevy = decimal.Parse(MedicareLevy);
            decimal budgetRepairLevy = decimal.Parse(BudgetRepairLevy);
            decimal incomeTax = decimal.Parse(IncomeTax);
            decimal netIncome = decimal.Parse(NetIncome);
            decimal payPacket = decimal.Parse(PayPacket);


            ISalaryPackageCalculator calc = new SalaryPackageCalculator(new SalaryPackageRule(new MockRepository()));
            ISalaryPackageRequest request = new SalaryPackageRequest()
            {
                GrossSalary = grossSalary,
                PayFrequency = PayFrequencyEnum.Monthly
            };

            var response = calc.Calculate(request);

            Assert.AreEqual(response.GrossSalary, grossSalary);
            foreach (IDeduction deduction in response.GrossSalaryDeductions)
            {
                if (deduction.Name.Equals("Superannuation"))
                    Assert.AreEqual(deduction.DeductableAmount, super);
            }
            Assert.AreEqual(response.TaxableIncome, taxableIncome);
            foreach (IDeduction deduction in response.TaxableIncomeDeductions)
            {
                if (deduction.Name.Equals("Medicare Levy"))
                    Assert.AreEqual(deduction.DeductableAmount, medicareLevy);
                else if (deduction.Name.Equals("Budget Repair Levy"))
                    Assert.AreEqual(deduction.DeductableAmount, budgetRepairLevy);
                else if (deduction.Name.Equals("Income Tax"))
                    Assert.AreEqual(deduction.DeductableAmount, incomeTax);
            }
            Assert.AreEqual(response.NetIncome, netIncome);
            Assert.AreEqual(response.PayPacket, payPacket);
        }
    }
}
