using System;
namespace d_01
{
	public class Storage
	{
		private int capacity_ { get; set; }

		public Storage(int capacity) => capacity_ = capacity;

		public void SetCapacity(int amount) => capacity_ = amount;

		public int GetCapacity() => capacity_;

		public bool IsEmpty() => capacity_ == 0;
	}
}

