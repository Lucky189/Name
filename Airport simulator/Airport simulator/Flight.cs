using System.Collections.Generic;

public class Flight
{
    public string FlightNumber { get; set; }
    public string Destination { get; set; }
    public int DepartureTime { get; set; }
    public FlightStatus Status { get; set; }
    public int Capacity { get; set; }

    public List<Passenger> OnBoardPassengers { get; set; } = new();

    public Flight(string number, string destination, int departureTime, int capacity)
    {
        FlightNumber = number;
        Destination = destination;
        DepartureTime = departureTime;
        Capacity = capacity;
        Status = FlightStatus.OnTime;
    }
}
