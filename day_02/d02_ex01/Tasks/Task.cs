using System;
namespace d02_ex01.Tasks
{
    public class Task
    {
        private struct TaskNode
        {
            public TaskNode()
            {
                title = null;
                summary = null;
                dueDate = null;
                type = TaskType.Type.Work;
                priority = TaskPriority.Priority.Normal;
                status = TaskState.State.New;
            }

            public string? title { get; set; }
            public string? summary { get; set; }
            public DateTime? dueDate { get; set; }
            public TaskType.Type type { get; set; }
            public TaskPriority.Priority priority { get; set; }
            public TaskState.State status { get; set; }

            public override string ToString()
            {
                string output = $"{title}\n[{type}] [{status}]\nPriority: {priority}";
                if (dueDate is not null)
                {
                    output += $", Due till {dueDate,-10:MM/dd/yyyy}";
                }
                if (summary is not null)
                {
                    output += $"\n{summary}";
                }
                return output;
            }
        }

        List<TaskNode> taskList = new();
        public Task()
        {
        }

        public void AddTask()
        {
            TaskNode taskNode = new();
            Console.WriteLine("> Enter a title");
            string? input;
            if ((input = Console.ReadLine()) is null || input == "")
            {
                Console.WriteLine("Input error. Check the input data and repeat the request.");
                return;
            }
            taskNode.title = input;
            Console.WriteLine("> Enter a description");
            input = Console.ReadLine();
            if (input == "")
            {
                taskNode.summary = null;
            }
            else
            {
                taskNode.summary = input;
            }
            Console.WriteLine("> Enter the deadline");
            input = Console.ReadLine();
            if (input is not null && input != "")
            {
                if (!DateTime.TryParse(input, out DateTime parsedDate))
                {
                    Console.WriteLine("Input error. Check the input data and repeat the request.");
                    return;
                }
                else
                {
                    taskNode.dueDate = parsedDate;
                }
            }
            Console.WriteLine("> Enter the type");
            input = Console.ReadLine();
            if (input is not null)
            {
                try
                {
                    TaskType.Type enumValue = (TaskType.Type)Enum.Parse(typeof(TaskType.Type), input);
                    taskNode.type = enumValue;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Input error. Check the input data and repeat the request.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Input error. Check the input data and repeat the request.");
                return;
            }
            Console.WriteLine("> Assign the priority");
            input = Console.ReadLine();
            if (input is not null && input != "")
            {
                try
                {
                    TaskPriority.Priority enumValue = (TaskPriority.Priority)Enum.Parse(typeof(TaskPriority.Priority), input);
                    taskNode.priority = enumValue;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Input error. Check the input data and repeat the request.");
                    return;
                }
            }
            taskList.Add(taskNode);
            Console.WriteLine(taskNode);
        }

        public void PrintList()
        {
            if (taskList.Count > 0)
            {
                foreach (var node in taskList)
                {
                    Console.WriteLine("- " + node + "\n");
                }
            }
            else
            {
                Console.WriteLine("The task list is still empty.");
            }
        }

        public void ChangeState(string title, TaskState.State state)
        {
            int index = taskList.FindIndex(x => x.title == title);

            if (index >= 0)
            {
                TaskNode editNode = taskList[index];

                if (state == TaskState.State.Irrelevant && editNode.status == TaskState.State.Completed)
                {
                    Console.WriteLine("Completed tasks can't be marked as Irrelevant.");
                }
                else if (state == editNode.status)
                {
                    Console.WriteLine($"{editNode.title} already marked as {state}");
                }
                else
                {
                    editNode.status = state;
                    taskList.RemoveAt(index);
                    taskList.Add(editNode);
                    Console.WriteLine($"The task [{editNode.title}] is now {editNode.status}");
                }
            }
            else
            {
                Console.WriteLine("Input error. The task with this title was not found");
            }
        }
    }
}

