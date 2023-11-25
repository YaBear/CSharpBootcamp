string[] commandLineArgs = Environment.GetCommandLineArgs();

if (commandLineArgs.Length != 4)
{
    throw new Exception("Invalid number of arguments, check your input and retry.");
}

double principal;
double annualInterestRate;
int numberOfMonths;

if (!double.TryParse(commandLineArgs[1], out principal))
{
    throw new Exception("Invalid argument - principal, check your input and retry.");
}

if (!double.TryParse(commandLineArgs[2], out annualInterestRate))
{
    throw new Exception("Invalid argument - interest rate, check your input and retry.");
}

if (!int.TryParse(commandLineArgs[3], out numberOfMonths))
{
    throw new Exception("Invalid argument - number of months, check your input and retry.");
}

double monthlyInterestRate = annualInterestRate / 12 / 100;
double monthlyPayment = (principal * monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, numberOfMonths)) / (Math.Pow(1 + monthlyInterestRate, numberOfMonths) - 1);

// Берется первое число текущего месяца.
DateTime paymentDate = DateTime.Today;
paymentDate = paymentDate.AddDays(-(DateTime.Today.Day - 1));
/*DateTime paymentDate = new(2021, 5, 1);*/

double remainingPrincipal = principal;

for (var month = 1; month <= numberOfMonths; ++month)
{
    paymentDate = paymentDate.AddMonths(1);
    int daysInMonth = DateTime.DaysInMonth(paymentDate.Year, paymentDate.Month);
    int daysInYear = DateTime.IsLeapYear(paymentDate.Year) ? 366 : 365;
    double interestPayment = (remainingPrincipal * annualInterestRate * daysInMonth) / (100 * daysInYear);
    double principalPayment = monthlyPayment - interestPayment;
    if (remainingPrincipal < monthlyPayment)
    {
        principalPayment = remainingPrincipal;
        monthlyPayment = principalPayment + interestPayment;
    }
    remainingPrincipal -= principalPayment;
    Console.WriteLine($"{month,-4} \t {paymentDate,-10:MM/dd/yyyy} \t {monthlyPayment,-10:F2} \t {principalPayment,-10:F2} \t {interestPayment,-10:F2} \t {remainingPrincipal,-10:F2}");
}
