using System;
using System.Linq;
using System.Collections.Generic;

namespace VeritecAssignment.ConsoleApplication
{
    public class Validator
    {
        static readonly IgnoreCaseStringCompare ignoreCase = new IgnoreCaseStringCompare();

        public static bool IsValidCurrency(string Input)
        {
            bool result = false;
            if (Decimal.TryParse(Input, out _))
                result = true;
            else
                Console.WriteLine(Properties.Resources.Error_NotAValidCurrency);

            return (result);
        }
        public static bool IsValidSelectItem(string Input, string[] List)
        {
            bool result = false;

            if (List.Contains<string>(Input, Validator.ignoreCase))
                result = true;
            else
                Console.WriteLine(Properties.Resources.Error_NotAValidSelection);

            return (result);
        }
        public static bool IsBetween(string Input, decimal Low, decimal High)
        {
            bool result = false;

            if (Decimal.TryParse(Input, out decimal value) && value >= Low && value <= High)
                result = true;
            else
                Console.WriteLine(Properties.Resources.Error_ValueNotInRange);

            return (result);
        }
    }
    sealed class IgnoreCaseStringCompare : IEqualityComparer<string>
    {
        public bool Equals(string a, string b)
        {
            return (a.ToUpper().Equals(b.ToUpper()));
        }
        public int GetHashCode(string bx)
        {
            unchecked
            {
                return ((bx != null ? bx.GetHashCode() : 0) * 397) ^ (bx != null ? bx.GetHashCode() : 0);
            }
        }
    }
}
