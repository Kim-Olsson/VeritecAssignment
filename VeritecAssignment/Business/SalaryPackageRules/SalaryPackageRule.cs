using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace VeritecAssignment.Business.SalaryPackage
{
    public class SalaryPackageRule : ISalaryPackageRule
    {
        private readonly IRateRepository rateRepository;
        public SalaryPackageRule(IRateRepository RateRepository)
        {
            rateRepository = RateRepository;
        }
        public IEnumerable<T> LoadPackageRules<T>()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes().Where(t => typeof(T).IsAssignableFrom(t) && t.IsClass);
            var instances = types.Select(Instantiate<T>).ToList();

            return (instances);
        }
        private T Instantiate<T>(Type t) => (T)Activator.CreateInstance(t, this.rateRepository);
    }
}