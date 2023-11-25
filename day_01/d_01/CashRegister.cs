using System;
using System.Xml.Linq;

namespace d_01
{
	public class CashRegister
	{
		public string name_ { get; }
		public Queue<Customer> customers_ { get; } = new Queue<Customer>();

		public CashRegister(string name) => name_ = name;

		public void AddCustomer(Customer customer) => customers_.Enqueue(customer);

		public Customer? ProceedNextCustomer()
		{
			if (customers_.Count > 0)
			{
				return customers_.Dequeue();
			}
			else
			{
				return null;
			}
		}

        public override string ToString() => $"{name_}, ({customers_.Count()} people with {customers_.Sum(customer => customer.GetGoods())} items behind)";

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            CashRegister otherRegister = (CashRegister)obj;
            return name_ == otherRegister.name_;
        }

        public override int GetHashCode() => HashCode.Combine(name_);
    }
}

