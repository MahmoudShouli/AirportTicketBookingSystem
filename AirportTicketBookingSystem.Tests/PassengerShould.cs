using System;
using System.Collections.Generic;
using AirportTicketBookingSystem.Context;
using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repositories;
using AirportTicketBookingSystem.Services.Implementations;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirportTicketBookingSystem.Tests;

public class PassengerShould : IDisposable
{
    private readonly Mock<IFlightRepository> _flightRepoMock;
    private readonly Fixture _fixture;
    private readonly PassengerServicesImpl _sut;
    
    public PassengerShould()
    {
        var passengersRepoMock = new Mock<IPassengersRepository>();
        _flightRepoMock = new Mock<IFlightRepository>();
        _sut = new PassengerServicesImpl(passengersRepoMock.Object, _flightRepoMock.Object);
        _fixture = new Fixture();
    }
    
    [Trait("Booking", "Success")]
    [Fact]
    public void BookSuccessfully_WhenFlightIdIsValid()
    {
        // Arrange
        var flight = _fixture
            .Build<Flight>()
            .With(f => f.IsBooked, false)
            .OmitAutoProperties()
            .Create();
        var currentUser = _fixture.Create<Passenger>();
        
        _flightRepoMock.Setup(x => x.GetFlightById(flight.Id)).Returns(flight);

        // Act
        UserContext.SetCurrentUser(currentUser);
        var booking = _sut.BookFlight(flight.Id);

        // Assert
        currentUser.Bookings.Should().ContainEquivalentOf(booking);
        flight.IsBooked.Should().BeTrue();
    }
    
    [Trait("Booking", "Failure")]
    [Fact]
    public void Throw_WhenFlightIsNotFound_OnBook()
    {
        // Arrange
        var id = _fixture.Create<string>();
        
        _flightRepoMock.Setup(x => x.GetFlightById(id)).Returns((Flight)null);
        
        // Act
        var act = () => _sut.BookFlight(id);

        // Assert
        act.Should().Throw<NotFoundException>()
            .WithMessage("Flight not found.");
    }
    
    [Trait("Booking", "Failure")]
    [Fact]
    public void Throw_When_FlightIsAlreadyBooked_OnBook()
    {
        // Arrange
        var flight = _fixture
            .Build<Flight>()
            .With(f => f.IsBooked, true)
            .OmitAutoProperties()
            .Create();
        
        _flightRepoMock.Setup(x => x.GetFlightById(flight.Id)).Returns(flight);

        // Act
        var act = () => _sut.BookFlight(flight.Id);

        // Assert
        act.Should().Throw<FlightAlreadyBookedException>()
            .WithMessage("Flight is already booked");
    }
    
    [Trait("Cancellation", "Failure")]
    [Fact]
    public void Throw_When_BookingIsNotFound_OnCancel()
    {
        // Arrange
        var flight = _fixture
            .Build<Flight>()
            .With(f => f.Id, "FL001")
            .OmitAutoProperties()
            .Create();
        var bookings = new List<Booking>
        {
            new Booking { FlightId = "FL002", PassengerName = "john" },
        };
        var passenger = _fixture.Build<Passenger>()
            .With(p => p.Name, "john")
            .With(p => p.Bookings, bookings)
            .Create();
        
        _flightRepoMock.Setup(x => x.GetFlightById(flight.Id)).Returns(flight);

        // Act
        UserContext.SetCurrentUser(passenger);
        var act = () => _sut.CancelBooking(flight.Id);

        // Assert
        act.Should().Throw<NotFoundException>()
            .WithMessage("Booking not found.");
    }
    
    public void Dispose()
    {
        UserContext.ResetCurrentUser();
    }
}