public class Passenger
{
    public string Name { get; set; }
    public string FlightNumber { get; set; }
    public bool HasTicket { get; set; }
    public bool PassedSecurity { get; set; }
    public bool IsOnBoard { get; set; }

    public Passenger(string name, string flightNumber)
    {
        Name = name;
        FlightNumber = flightNumber;
        HasTicket = false;
        PassedSecurity = false;
        IsOnBoard = false;
    }
}