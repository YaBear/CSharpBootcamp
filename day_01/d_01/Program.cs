using Microsoft.Win32;

namespace d_01
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test with least customers.

            Console.WriteLine("Shop where customers choose register with Least Customers in queue\n");

            Store store = new(40, 3);

            List<Customer> customers = new List<Customer>
            {
                new Customer("Mathew", 1),
                new Customer("Andrew", 2),
                new Customer("John", 3),
                new Customer("Frank", 4),
                new Customer("Monster", 5),
                new Customer("Kate", 6),
                new Customer("Johanna", 7),
                new Customer("Ann", 8),
                new Customer("Renata", 9),
                new Customer("Lali", 10)
            };

            foreach (var customer in customers)
            {
                customer.FillCart(9);
                var register = customer.ChooseCashRegisterWithLeastCustomers(store.cashRegisters_);
                register.AddCustomer(customer);
                Console.WriteLine($"{customer} - {register}");
            }

            int customerCounter = new();
            List<Customer> cantBuy = new();
            while (store.IsOpen && customerCounter < 10)
            {
                foreach (var register in store.cashRegisters_)
                {
                    Customer? customer = register.ProceedNextCustomer();
                    if (customer is not null)
                    {
                        if (customer.GetGoods() <= store.GetCapacity())
                        {
                            store.GetGoodsFromStore(customer.GetGoods());
                            ++customerCounter;
                        }
                        else
                        {
                            cantBuy.Add(customer);
                            ++customerCounter;
                        }
                    }
                }
            }

            foreach (var customer in cantBuy)
            {
                Console.WriteLine($"{customer.name_}, Customer #{customer.id_} ({customer.GetGoods()} items left in cart)");
            }

            // Test with least goods in cart.

            Console.WriteLine("\nShop where customers choose register with Least Goods in queue\n");

            Store store2 = new(40, 3);

            foreach (var customer in customers)
            {
                var register = customer.ChooseCashRegisterWithLeastGoods(store2.cashRegisters_);
                register.AddCustomer(customer);
                Console.WriteLine($"{customer} - {register}");
            }

            int customerCounter2 = new();
            List<Customer> cantBuy2 = new();
            while (store2.IsOpen && customerCounter2 < 10)
            {
                foreach (var register in store2.cashRegisters_)
                {
                    Customer? customer = register.ProceedNextCustomer();
                    if (customer is not null)
                    {
                        if (customer.GetGoods() <= store2.GetCapacity())
                        {
                            store2.GetGoodsFromStore(customer.GetGoods());
                            ++customerCounter2;
                        }
                        else
                        {
                            cantBuy2.Add(customer);
                            ++customerCounter2;
                        }
                    }
                }
            }

            foreach (var customer in cantBuy2)
            {
                Console.WriteLine($"{customer.name_}, Customer #{customer.id_} ({customer.GetGoods()} items left in cart)");
            }
        }
    }
}