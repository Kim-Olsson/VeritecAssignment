using System;
using System.Collections.Generic;
using System.Text;
using VeritecAssignment.Business.SalaryPackage;

namespace VeritecAssignment.Repository.StaticRepository
{
    public sealed class RateBracketDTO : IRateBracket
    {
        public decimal YearlyEarningsLessThan { get; set; }
        public decimal Rate { get; set; }
        public bool Accumulative { get; set; } = true;
    }
}
