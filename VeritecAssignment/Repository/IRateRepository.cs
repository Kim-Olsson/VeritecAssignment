namespace VeritecAssignment.Business.SalaryPackage
{
    public interface IRateRepository
    {
        IRateBracket[] GetIncomeTaxRates();
        IRateBracket[] GetMedicareLevyRates();
        IRateBracket[] GetBudgetRepairRates();
        IRateBracket[] GetSuperannuationRates();
        IRateBracket[] GetIncomeTaxRates(string year);
        IRateBracket[] GetMedicareLevyRates(string year);
        IRateBracket[] GetBudgetRepairRates(string year);
        IRateBracket[] GetSuperannuationRates(string year);
    }
}
