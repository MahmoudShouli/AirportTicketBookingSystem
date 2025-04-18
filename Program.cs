﻿using AirportTicketBookingSystem.Repository;
using AirportTicketBookingSystem.Services;
using AirportTicketBookingSystem.Utils;

namespace AirportTicketBookingSystem;

internal static class Program
{
    private static void Main()
    {
        var flightRepo = new FlightFileRepository();
        var passengerRepo = new PassengersListRepository();
        
        var flightService = new FlightService(flightRepo);
        var passengerService = new PassengerServices(passengerRepo, flightRepo);
        var managerService = new ManagerServices(flightService, passengerRepo);
        
        var printer = new Printer(passengerService, flightService, managerService);
        
        printer.PrintMainMenu();
    }
}