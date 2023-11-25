using d02_ex01.Tasks;

namespace d02_ex01
{
    class Program
    {
        static private Tasks.Task engine = new();
        static void Main(string[] args)
        {
            PrintHelp();
            while (ProgrammLoop())
            {

            }
        }

        static private void PrintHelp()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("Type add to add task to list.");
            Console.WriteLine("-> Title required, Description optional,");
            Console.WriteLine("-> Deadline optional, Type required (Work, Study or Personal)");
            Console.WriteLine("-> Priority optional (Normal on default)");
            Console.WriteLine("Type done to mark task as done.");
            Console.WriteLine("Type wontdo to mark task as irrelevant.");
            Console.WriteLine("Type list to show list of your tasks.");
            Console.WriteLine("Type q or quit to exit from program.");
            Console.WriteLine("=======================================");
        }

        static private bool ProgrammLoop()
        {
            string? input = Console.ReadLine();
            if (input is not null)
            {
                if (input.ToLower() == "add")
                {
                    engine.AddTask();
                }
                else if (input.ToLower() == "list")
                {
                    engine.PrintList();
                }
                else if (input.ToLower() == "done")
                {
                    Console.WriteLine("> Enter a title:");
                    string? title = Console.ReadLine();
                    if (title is not null)
                    {
                        engine.ChangeState(title, TaskState.State.Completed);
                    }
                }
                else if (input.ToLower() == "wontdo")
                {
                    Console.WriteLine("> Enter a title:");
                    string? title = Console.ReadLine();
                    if (title is not null)
                    {
                        engine.ChangeState(title, TaskState.State.Irrelevant);
                    }
                }
                else if (input.ToLower() == "quit" || input.ToLower() == "q")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Input error. Check the input data and repeat the request.");
                }
            }
            return true;
        }
    }
}