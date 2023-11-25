using System;
using System.Xml.Linq;

namespace d_01
{
	public class Customer
	{
		public string name_ { get; }
		public int id_ { get; }
		private int goods_ { get; set; } = 0;

        public Customer(string name, int id)
		{
			name_ = name;
			id_ = id;
		}

		public void FillCart(int capacity)
		{
            Random random = new();
			goods_ = random.Next(1, capacity);
        }

        public int GetGoods()
        {
            return goods_;
        }

		public override string ToString() => $"{this.name_}, customer #{this.id_} ({this.goods_} items in cart)";

        public override bool Equals(object? obj)
        {
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			Customer otherCustomer = (Customer)obj;
			return this.name_ == otherCustomer.name_ && this.id_ == otherCustomer.id_;
        }

        public static bool operator ==(Customer first, Customer second)
        {
            if (ReferenceEquals(first, second))
                return true;

            if (first is null || second is null)
                return false;

            return first.Equals(second);
        }

        public static bool operator !=(Customer first, Customer second) => !(first == second);

        public override int GetHashCode() => HashCode.Combine(name_, id_);
    }
}

