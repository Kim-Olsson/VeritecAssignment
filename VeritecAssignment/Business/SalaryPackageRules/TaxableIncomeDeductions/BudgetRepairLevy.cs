using System;

namespace VeritecAssignment.Business.SalaryPackage
{
    public sealed class BudgetRepairLevy : RateCalculator, ITaxableIncomeDeduction
    {
        public string Name { get; } = "Budget Repair Levy";
        public decimal DeductableAmount { get; private set; }
        private readonly IRateRepository rateRepository;
        public BudgetRepairLevy(IRateRepository RateRepository)
        {
            rateRepository = RateRepository;
        }
        public IDeduction Calculate(ISalaryPackageRequest Request)
        {
            decimal TI = Math.Floor(Request.TaxableIncome);

            IRateBracket[] rates = rateRepository.GetBudgetRepairRates();
            IRateCalculationRequest request = new RateCalculationRequest() { YearlyEarnings = TI, Rates = rates };
            IRateCalculationResponse response = this.CalculateRate(request);

            IDeduction result = new Deduction()
            {
                DeductableAmount = Math.Ceiling(response.DeductionPerMonth),
                Name = this.Name
            };
            return (result);
        }
    }
}
