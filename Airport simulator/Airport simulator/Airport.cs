using System;
using System.Collections.Generic;
using System.Linq;

public class Airport
{
    public List<Flight> Flights = new();
    public List<Passenger> Passengers = new();

    public Queue<Passenger> RegistrationQueue = new();
    public Queue<Passenger> SecurityQueue = new();

    private Random rand = new();
    public int CurrentTime { get; private set; } = 0;

    private const int RegistrationDesks = 3;
    private const int SecurityPoints = 2;
    private const int BoardingSpeed = 5;

    public void AddInitialFlights()
    {
        Flights.Add(new Flight("PS101", "Kyiv", 10, 20));
        Flights.Add(new Flight("LH202", "Berlin", 15, 15));
        Flights.Add(new Flight("FR303", "London", 20, 25));
    }

    public void Tick()
    {
        CurrentTime++;

        GeneratePassenger();
        ProcessRegistration();
        ProcessSecurity();
        UpdateFlights();
        BoardPassengers();

        PrintStatus();
    }

    private void GeneratePassenger()
    {
        if (rand.NextDouble() < 0.4 && Flights.Any())
        {
            var flight = Flights[rand.Next(Flights.Count)];
            var passenger = new Passenger($"Passenger{rand.Next(1000)}", flight.FlightNumber);

            Passengers.Add(passenger);
            RegistrationQueue.Enqueue(passenger);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Новий пасажир {passenger.Name} на рейс {flight.FlightNumber}");
            Console.ResetColor();
        }
    }

    private void ProcessRegistration()
    {
        for (int i = 0; i < RegistrationDesks; i++)
        {
            if (RegistrationQueue.Count == 0) return;

            var p = RegistrationQueue.Dequeue();
            p.HasTicket = true;
            SecurityQueue.Enqueue(p);
        }
    }

    private void ProcessSecurity()
    {
        for (int i = 0; i < SecurityPoints; i++)
        {
            if (SecurityQueue.Count == 0) return;

            var p = SecurityQueue.Dequeue();
            p.PassedSecurity = true;
        }
    }

    private void UpdateFlights()
    {
        foreach (var flight in Flights)
        {
            if (flight.Status == FlightStatus.OnTime && CurrentTime >= flight.DepartureTime - 2)
                flight.Status = FlightStatus.Boarding;

            if (CurrentTime >= flight.DepartureTime)
                flight.Status = FlightStatus.Departed;
        }
    }

    private void BoardPassengers()
    {
        foreach (var flight in Flights.Where(f => f.Status == FlightStatus.Boarding))
        {
            var readyPassengers = Passengers
                .Where(p => p.FlightNumber == flight.FlightNumber &&
                            p.HasTicket &&
                            p.PassedSecurity &&
                            !p.IsOnBoard)
                .Take(BoardingSpeed);

            foreach (var p in readyPassengers)
            {
                if (flight.OnBoardPassengers.Count >= flight.Capacity) break;

                p.IsOnBoard = true;
                flight.OnBoardPassengers.Add(p);
            }
        }

        foreach (var flight in Flights.Where(f => f.Status == FlightStatus.Departed))
        {
            Passengers.RemoveAll(p => p.FlightNumber == flight.FlightNumber && p.IsOnBoard);
        }
    }

    private void PrintStatus()
    {
        Console.WriteLine("\n==============================");
        Console.WriteLine($"Час симуляції: {CurrentTime}");

        foreach (var flight in Flights)
        {
            Console.ForegroundColor = flight.Status switch
            {
                FlightStatus.OnTime => ConsoleColor.White,
                FlightStatus.Boarding => ConsoleColor.Yellow,
                FlightStatus.Departed => ConsoleColor.Red,
                _ => ConsoleColor.Gray
            };

            Console.WriteLine($"{flight.FlightNumber} → {flight.Destination} | {flight.Status}");
            Console.ResetColor();
        }

        Console.WriteLine($"Черга на реєстрацію: {RegistrationQueue.Count}");
        Console.WriteLine($"Черга на контроль: {SecurityQueue.Count}");

        int waiting = Passengers.Count(p => p.PassedSecurity && !p.IsOnBoard);
        Console.WriteLine($"Очікують у зоні вильоту: {waiting}");
        Console.WriteLine("==============================\n");
    }
}
