using System;
namespace d_01
{
	public static class CustomerExtensions
	{
        public static CashRegister ChooseCashRegisterWithLeastCustomers(this Customer customer, IEnumerable<CashRegister> cashRegisters)
        {
            if (cashRegisters == null || !cashRegisters.Any())
            {
                throw new ArgumentException("Cash registers collection is empty or null.");
            }

            return cashRegisters.OrderBy(register => register.customers_.Count).First();
        }

        public static CashRegister ChooseCashRegisterWithLeastGoods(this Customer customer, IEnumerable<CashRegister> cashRegisters)
        {
            if (cashRegisters == null || !cashRegisters.Any())
            {
                throw new ArgumentException("Cash registers collection is empty or null.");
            }

            return cashRegisters
                .OrderBy(register => register.customers_.Sum(customer => customer.GetGoods()))
                .First();
        }
    }
}

