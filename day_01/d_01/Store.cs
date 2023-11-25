using System;
namespace d_01
{
	public class Store
	{
        private Storage storageCapacity_ { get; } = new(0);
        public List<CashRegister> cashRegisters_ { get; }

        public Store(int storageCapacity, int numberOfCashRegisters)
        {
            if (storageCapacity < 0)
            {
                throw new ArgumentException("Storage capacity must be non-negative.");
            }
            storageCapacity_.SetCapacity(storageCapacity);
            cashRegisters_ = new List<CashRegister>();
            for (int i = 1; i <= numberOfCashRegisters; i++)
            {
                cashRegisters_.Add(new CashRegister($"Register #{i}"));
            }
        }

        public bool IsOpen => storageCapacity_.GetCapacity() > 0;

        public void GetGoodsFromStore(int amount) => storageCapacity_.SetCapacity(storageCapacity_.GetCapacity() - amount);

        public int GetCapacity() => storageCapacity_.GetCapacity();
    }
}

