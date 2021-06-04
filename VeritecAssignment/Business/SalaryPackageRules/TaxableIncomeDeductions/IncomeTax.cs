using System;

namespace VeritecAssignment.Business.SalaryPackage
{
    public sealed class IncomeTax : RateCalculator, ITaxableIncomeDeduction
    {
        public string Name { get; } = "Income Tax";
        public decimal DeductableAmount { get; private set; }
        private readonly IRateRepository rateRepository;
        public IncomeTax(IRateRepository RateRepository)
        {
            rateRepository = RateRepository;
        }
        public IDeduction Calculate(ISalaryPackageRequest Package)
        {
            decimal TI = Math.Floor(Package.TaxableIncome);

            IRateBracket[] rates = rateRepository.GetIncomeTaxRates();
            IRateCalculationRequest request = new RateCalculationRequest() { YearlyEarnings = TI, Rates = rates };
            IRateCalculationResponse response = this.CalculateRate(request);

            IDeduction result = new Deduction()
            {
                DeductableAmount = Math.Round(response.DeductionPerMonth),
                Name = this.Name
            };
            return (result);
        }
    }
}