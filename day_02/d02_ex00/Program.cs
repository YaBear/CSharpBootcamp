using d02_ex00.Models;

namespace d02_ex00
{
    public class Program
    {
        static void Main(string[] args)
        {
            Exchanger exch = new(args[0], args[1]);
            Console.WriteLine(exch);
        }
    }
}