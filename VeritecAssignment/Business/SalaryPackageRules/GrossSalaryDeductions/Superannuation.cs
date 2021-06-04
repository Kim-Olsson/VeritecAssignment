using System;


namespace VeritecAssignment.Business.SalaryPackage
{
    public sealed class SuperContribution : RateCalculator, IGrossSalaryDeduction
    {
        public string Name { get; } = "Superannuation";
        public decimal DeductableAmount { get; private set; }
        private readonly IRateRepository rateRepository;
        public SuperContribution(IRateRepository RateRepository)
        {
            rateRepository = RateRepository;
        }
        public IDeduction Calculate(ISalaryPackageRequest Package)
        {
            decimal GS = Package.GrossSalary;
            IDeduction result;
            IRateBracket[] rates = rateRepository.GetSuperannuationRates();
            IRateCalculationRequest request = new RateCalculationRequest() { YearlyEarnings = GS, Rates = rates };
            IRateBracket bracket = this.GetRateBracket(request);

            if (bracket != null)
            {
                result = new Deduction()
                {
                    DeductableAmount = Math.Round((Package.GrossSalary / (1 + bracket.Rate) * bracket.Rate), 2),
                    Name = this.Name
                };
                return (result);
            }
            else
            {
                result = new Deduction()
                {
                    DeductableAmount = default,
                    Name = "No suitable rate found for " + this.Name
                };
                return (result);
            }
        }
    }
}