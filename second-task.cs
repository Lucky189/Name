using System;
using System.Collections.Generic;

namespace SecondTaskApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 10;
            double b = 3.5;
            Console.WriteLine($"Сума: {Add(a, (int)b)}");
            Console.WriteLine($"Множення: {Multiply(a, b)}");
            
            if (a > b && b > 0)
            {
                Console.WriteLine("a більше b і b більше 0");
            }
            else if (a == b || b < 0)
            {
                Console.WriteLine("a дорівнює b або b менше 0");
            }
            else
            {
                Console.WriteLine("Жодна умова не виконана");
            }
            
            string result = (a > b) ? "a більше b" : "a менше або дорівнює b";
            Console.WriteLine(result);
            
            int[] numbers = {1, 2, 3, 4, 5};
            Console.WriteLine("for");
            for (int i = 0; i < numbers.Length; i++)
                Console.Write(numbers[i] + " ");
            Console.WriteLine();

            Console.WriteLine("foreach");
            foreach (var num in numbers)
                Console.Write(num + " ");
            Console.WriteLine();
            
            List<string> names = new List<string>() {"Tom", "Bob", "Sam"};
            int index = 0;
            Console.WriteLine("while");
            while (index < names.Count)
            {
                Console.Write(names[index] + " ");
                index++;
            }
            Console.WriteLine();

            index = 0;
            Console.WriteLine("do-while");
            do
            {
                Console.Write(names[index] + " ");
                index++;
            } while (index < names.Count);
            Console.WriteLine();
        }
        
        static int Add(int x, int y) => x + y;
        static double Multiply(double x, double y) => x * y;
    }
}