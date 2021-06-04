using System;
using VeritecAssignment.Repository;

namespace VeritecAssignment.Business.SalaryPackage
{
    public sealed class MedicareLevy : RateCalculator, ITaxableIncomeDeduction
    {
        public string Name { get; } = "Medicare Levy";
        public decimal DeductableAmount { get; private set; }
        private readonly IRateRepository rateRepository;
        public MedicareLevy(IRateRepository RateRepository)
        {
            rateRepository = RateRepository;
        }
        public IDeduction Calculate(ISalaryPackageRequest Request)
        {
            decimal TI = Math.Floor(Request.TaxableIncome);

            IRateBracket[] rates = rateRepository.GetMedicareLevyRates();
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