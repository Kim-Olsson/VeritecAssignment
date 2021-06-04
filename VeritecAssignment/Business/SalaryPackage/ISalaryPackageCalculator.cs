using System.Collections.Generic;

public enum PayFrequencyEnum
{
    Weekly,
    Fortnightly,
    Monthly
}
public interface IDeduction
{
    string Name { get; }
    decimal DeductableAmount { get; }
}
public interface IDeductionCalculator: IDeduction 
{
    IDeduction Calculate(ISalaryPackageRequest Request);
}
public interface ISalaryPackageCalculator
{
    ISalaryPackageResponse Calculate(ISalaryPackageRequest Request);
}
public interface ISalaryPackageRequest
{
    decimal GrossSalary { get; set; }
    decimal TaxableIncome { get; set; }
    PayFrequencyEnum PayFrequency { get; set; }
}
public interface ISalaryPackageResponse
{
    decimal GrossSalary { get; set; }
    PayFrequencyEnum PayFrequency { get; set; }
    ICollection<IDeduction> GrossSalaryDeductions { get; set; }
    decimal TaxableIncome { get; set; }
    ICollection<IDeduction> TaxableIncomeDeductions { get; set; }
    decimal NetIncome { get; set; }
    decimal PayPacket { get; set; }
}

public interface ITaxableIncomeDeduction : IDeductionCalculator
{
}
public interface IGrossSalaryDeduction : IDeductionCalculator
{
}
public interface ISalaryPackageRule
{
    IEnumerable<T> LoadPackageRules<T>();
}