class Program
{
    static async Task Main(string[] args)
    {
        MyClass obj = new MyClass();
       

        await RunTasks();
        await RunTasksReturnFirst();
    }

    static async Task RunTasks()
    {
        Random rnd = new Random();
        Task[] tasks = new Task[3];

        for (int i = 0; i < 3; i++)
        {
            int taskNum = i + 1;
            tasks[i] = Task.Run(async () =>
            {
                await Task.Delay(rnd.Next(1000, 3000));
                Console.WriteLine($"Task {taskNum}");
            });
        }

        await Task.WhenAll(tasks);
    }

    static async Task RunTasksReturnFirst()
    {
        Random rnd = new Random();
        Task<string>[] tasks = new Task<string>[3];

        for (int i = 0; i < 3; i++)
        {
            int taskNum = i + 1;
            tasks[i] = Task.Run(async () =>
            {
                await Task.Delay(rnd.Next(1000, 3000));
                return $"Task {taskNum}";
            });
        }

        var first = await Task.WhenAny(tasks);
        Console.WriteLine($"Першим завершився: {await first}");
    }
}

internal class MyClass
{
}