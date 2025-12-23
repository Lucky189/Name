using System;
using System.Threading;

class Program
{
    static void Main()
    {
        var airport = new Airport();
        airport.AddInitialFlights();

        while (true)
        {
            airport.Tick();
            Thread.Sleep(1000); // 1 тік = 1 секунда
        }
    }
}