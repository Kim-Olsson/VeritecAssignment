using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using VeritecAssignment.Business.SalaryPackage;

namespace VeritecAssignment.ConsoleApplication
{
    public sealed class Application : IApplication
    {
        const string SPACE = " ";

        private readonly string[] PAY_FREQUENCEY_OPTIONS = { Properties.Resources.WEEK, Properties.Resources.FORTNIGHT, Properties.Resources.MONTH };
        private readonly ISalaryPackageCalculator packageCalculator;
        public Application(ISalaryPackageCalculator PackageCalculator)
        {
            packageCalculator = PackageCalculator;
        }
        public void Start()
        {
            ISalaryPackageRequest request = new SalaryPackageRequest();
            ISalaryPackageResponse result;

            EnterPackageSalaryInformation(request);

            ConsoleUI.LineBreak();
            ConsoleUI.DisplayLine(Properties.Resources.CalculatingSalaryDetails);

            result = packageCalculator.Calculate(request);

            ConsoleUI.LineBreak();
            DisplayGrossSalaryDeductions(result);

            ConsoleUI.LineBreak();
            DisplayTaxableIncome(result);

            ConsoleUI.LineBreak();
            ConsoleUI.DisplayLine(Properties.Resources.Deductions);
            DisplayTaxableIncomeDeductions(result);

            ConsoleUI.LineBreak();
            DisplaySummary(result);

            ConsoleUI.LineBreak();
            ConsoleUI.PressAnyKey(Properties.Resources.PressAnyKeyToEnd);
        }
        private void EnterPackageSalaryInformation(ISalaryPackageRequest Package)
        {
            string payFrequencyInput;

            Package.GrossSalary = ConsoleUI.InputCurrency(Properties.Resources.EnterYourSalaryAmount, default, decimal.MaxValue);
            payFrequencyInput = ConsoleUI.SelectFromList(Properties.Resources.EnterPayFrequency, PAY_FREQUENCEY_OPTIONS);

            if (payFrequencyInput.ToUpper().Equals(Properties.Resources.WEEK))
                Package.PayFrequency = PayFrequencyEnum.Weekly;
            else if (payFrequencyInput.ToUpper().Equals(Properties.Resources.FORTNIGHT))
                Package.PayFrequency = PayFrequencyEnum.Fortnightly;
            else
                Package.PayFrequency = PayFrequencyEnum.Monthly;
        }
        private void DisplayGrossSalaryDeductions(ISalaryPackageResponse Package)
        {
            ConsoleUI.DisplayLine(Properties.Resources.GrossPackage + SPACE + Package.GrossSalary.ToString(Properties.Resources.CurrencyFormat, CultureInfo.CurrentCulture));
            foreach (IDeduction deduction in Package.GrossSalaryDeductions)
            {
                ConsoleUI.DisplayFormDataLine(deduction.Name, deduction.DeductableAmount.ToString(Properties.Resources.CurrencyFormat, CultureInfo.CurrentCulture));
            }
        }
        private void DisplayTaxableIncomeDeductions(ISalaryPackageResponse Package)
        {
            foreach (IDeduction deduction in Package.TaxableIncomeDeductions)
            {
                ConsoleUI.DisplayFormDataLine(deduction.Name, deduction.DeductableAmount.ToString(Properties.Resources.CurrencyFormat, CultureInfo.CurrentCulture));
            }
        }
        private void DisplayTaxableIncome(ISalaryPackageResponse Package)
        {
            ConsoleUI.DisplayFormDataLine(Properties.Resources.TaxableIncome, Package.TaxableIncome.ToString(Properties.Resources.CurrencyFormat, CultureInfo.CurrentCulture));
        }
        private void DisplaySummary(ISalaryPackageResponse Package)
        {
            ConsoleUI.DisplayFormDataLine(Properties.Resources.NetIncome, Package.NetIncome.ToString(Properties.Resources.CurrencyFormat, CultureInfo.CurrentCulture));
            ConsoleUI.DisplayFormData(Properties.Resources.PayPacket, Package.PayPacket.ToString(Properties.Resources.CurrencyFormat, CultureInfo.CurrentCulture));
            ConsoleUI.DisplayLine(SPACE + Properties.Resources.Per + SPACE + PayFrequenceyToString(Package.PayFrequency));
        }
        private string PayFrequenceyToString(PayFrequencyEnum payFrequency)
        {
            return payFrequency switch
            {
                PayFrequencyEnum.Weekly => (Properties.Resources.WeekForDisplay),
                PayFrequencyEnum.Fortnightly => (Properties.Resources.FortnightForDisplay),
                _ => (Properties.Resources.MonthForDisplay),
            };
        }
        public void Stop()
        {
        }
    }
}
