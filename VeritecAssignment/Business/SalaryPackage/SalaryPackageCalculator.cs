using System;
using System.Collections.Generic;

namespace VeritecAssignment.Business.SalaryPackage
{
    public sealed class SalaryPackageCalculator : ISalaryPackageCalculator
    {
        const int WEEKS_IN_A_YEAR = 52;
        const int FORTNIGHTS_IN_A_YEAR = WEEKS_IN_A_YEAR / 2;
        const int MONTHS_IN_A_YEAR = 12;

        public decimal GrossSalary { get; set; }
        public PayFrequencyEnum PayFrequency { get; set; }
        public decimal TaxableIncome { get; set; }
        public decimal NetIncome { get; set; }
        public IEnumerable<IDeduction> GrossSalaryDeductions { get => grossSalaryDeductions; } 
        public IEnumerable<IDeduction> TaxableIncomeDeductions { get => taxableIncomeDeductions; }
        public decimal PayPacket
        {
            get
            {
                if (PayFrequency == PayFrequencyEnum.Weekly)
                    return NetIncome / WEEKS_IN_A_YEAR;
                else if (PayFrequency == PayFrequencyEnum.Fortnightly)
                    return NetIncome / FORTNIGHTS_IN_A_YEAR;
                else
                    return NetIncome / MONTHS_IN_A_YEAR;
            }
        }
        private IEnumerable<IGrossSalaryDeduction> grossSalaryDeductions { get; set; }
        private IEnumerable<ITaxableIncomeDeduction> taxableIncomeDeductions { get; set; }
        public SalaryPackageCalculator(ISalaryPackageRule PackageRules)
        {
            grossSalaryDeductions = PackageRules.LoadPackageRules<IGrossSalaryDeduction>();
            taxableIncomeDeductions = PackageRules.LoadPackageRules<ITaxableIncomeDeduction>();
        }
        public ISalaryPackageResponse Calculate(ISalaryPackageRequest Request)
        {
            ISalaryPackageResponse response = new SalaryPackageResponse()
            {
                GrossSalary = Request.GrossSalary,
                PayFrequency = Request.PayFrequency,
                TaxableIncome = Request.GrossSalary,
                NetIncome = default,
                GrossSalaryDeductions = new List<IDeduction>(),
                TaxableIncomeDeductions = new List<IDeduction>()
            };
            ApplyGrossSalaryDeductions(Request, response);

            Request.TaxableIncome = response.TaxableIncome;
            ApplyTaxableIncomeDeductions(Request, response);
            CalculatePayPacket(response);

            return (response);
        }
        private void CalculatePayPacket(ISalaryPackageResponse response)
        {
            response.PayPacket = response.PayFrequency switch
            {
                PayFrequencyEnum.Weekly => response.NetIncome / WEEKS_IN_A_YEAR,
                PayFrequencyEnum.Fortnightly => response.NetIncome / FORTNIGHTS_IN_A_YEAR,
                PayFrequencyEnum.Monthly => response.NetIncome / MONTHS_IN_A_YEAR,
                _ => response.NetIncome / MONTHS_IN_A_YEAR,
            };
            response.PayPacket = Math.Round(response.PayPacket, 2);
        }
        private void ApplyGrossSalaryDeductions(ISalaryPackageRequest Request, ISalaryPackageResponse Response)
        {
            Response.TaxableIncome = Request.GrossSalary;
            foreach (IDeductionCalculator deduction in this.grossSalaryDeductions)
            {
                IDeduction deductionResponse;

                deductionResponse = deduction.Calculate(Request);
                Response.TaxableIncome -= deductionResponse.DeductableAmount;
                Response.GrossSalaryDeductions.Add(deductionResponse);
            }
        }
        private void ApplyTaxableIncomeDeductions(ISalaryPackageRequest Request, ISalaryPackageResponse Response)
        {
            Response.NetIncome = Request.TaxableIncome;
            foreach (IDeductionCalculator deduction in this.taxableIncomeDeductions)
            {
                IDeduction deductionResponse;

                deductionResponse = deduction.Calculate(Request);
                Response.NetIncome -= deductionResponse.DeductableAmount;
                Response.TaxableIncomeDeductions.Add(deductionResponse);
            }
        }
    }
}