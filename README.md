# VeritecAssignment
The Veritec coding assignment
### Author
Kim Olsson
kolsson@kobold.com.au
## Coding Decisions and Approach
The approach taken on this assignment was to demonstrate the application of the Clean Architecture and  much of the SOLID principles. It's built using TDD approach and the extensive use of Dependency Injection. In general, all major classes implements an interface and have a Request and Response interface pattern. I use mainly the built in code analyzer in VS 2019 and NDepend. Although NDepend is a expensive analyser I believe it to be superior to most other analysers because it calculates technical debt and code covergae rather than requiring you to add a newline after a closing curly brace etc. If you're using TDD, this tool is, in my opinion, the bees knees.
## Code
The solution is divided into two projects, a unit test project called SalaryDeductionTest and the application project called VeritecAssignment. The code is structured into three different layers, a user interfce layer (Console application), a business layer, and a repository/data layer. The Dependency Injector ties it all together. VeritectAssignment project is divided into three main folders. Business which contains the application logic, ConsoleApplication that implements the user interface, and Repository which holds the tax rate tables.

The main business class is the SalaryPackageCalculator which accepts a ISalaryPackageRequest and returns a ISalaryPackageReponse object containing the result. The SalaryPackageCalculator is only aware of two types of deductions, gross salary deductions and taxable income deductions. It doesn't know which gross salary or taxable income deductions are required and relies on an implementation of the ISalaryPackageRule to provide the deductions logic. For gross salary deductions, it uses the ISalaryPackage interface to return a list of IDeductionCalculator object that implements the IGrossSalaryDeduction interface. For taxable income deductions, it asks the same interface for a list of IDeductionCalculator objects that implements the ITaxableIncomeDeduction interface. To add further taxable income deduction, simply add a new class that implements the ITaxableIncomeDeduction. The Name field in both ITaxableIncomeDeduction and IGrossSalaryDeduction interface is used by the SalaryPackage object add to a list of named deductions that will be printed out by the ConsoleApplication.

The SalaryPackageRule object, which implements the ISalaryPackage interface) manages the deductions objects that implements the IDeductionCalculator interface. All the gross and taxable income objects are derived from the RateCalculator since almost all deductions relies on the same data format to calculate the deduction based on income brackets.

The implementation of the IRepository interface uses a simple static table for the sake of simplicity and to save time. Other storage options can be used, like SQL, MongoDb, Excel, csv files, or even data import from MYOB data files.

## Extensibility
The application can easily be extended to meet future changes in tax laws and tax tables. The IRepository interface provides two interfaces, one that assumes the currently valid tax rates table and a second one that accepts a financial year parameter if previous year's tax rate tables need to be used.
## Deviations
The application outputs the deductions in alphabetical order while the Example outputs the deductions in a non-alphabetical order. This because the types return from an assembly are listed in alphabetical order which I believe is a better way to display it. One could get ConsoleApplication to explicitly output the deductions in that order, but it would create a dependency on the names of the deductions.
