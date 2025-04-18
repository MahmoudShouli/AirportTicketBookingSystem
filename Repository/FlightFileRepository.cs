﻿using System.ComponentModel.Design;
using AirportTicketBookingSystem.Models;
using System.Globalization;
using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Repository;

public class FlightFileRepository : IFlightRepository
{
    private const string FilePath = @"D:\Computer Engineering\5th Year 2nd Sem\Internship\AirportTicketBookingSystem\Data\Flights.csv";

    public List<Flight> LoadFlights()
    {
        return FileServices.ConvertFileToFlights(FilePath);
    }

    public void SaveFlights(List<Flight> flights)
    {
        var lines = new List<string>
        {
            "FlightId,Price,DepartureCountry,DestinationCountry,DepartureDate,DepartureAirport,DestinationAirport,Class,IsBooked"
        };

        foreach (var flight in flights)
        {
            var line = $"{flight.Id},{flight.Price},{flight.DepartureCountry},{flight.DestinationCountry}," +
                       $"{flight.DepartureDate:yyyy-MM-ddTHH:mm:ss},{flight.DepartureAirport},{flight.DestinationAirport}," +
                       $"{flight.Class},{flight.IsBooked}";
            lines.Add(line);
        }

        File.WriteAllLines(FilePath, lines);
    }
}