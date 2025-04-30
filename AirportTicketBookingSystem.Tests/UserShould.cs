using System;
using AirportTicketBookingSystem.Context;
using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repositories;
using AirportTicketBookingSystem.Services.Implementations;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirportTicketBookingSystem.Tests;

public class UserShould : IDisposable
{
    [Fact]
    public void Login_Successfully_AsManager_WhenCredentialsAreValid()
    {
        // Arrange
        const string name = "mahmoud";
        const string password = "123";
        
        var passengersRepoMock = new Mock<IPassengersRepository>();
        passengersRepoMock.Setup(x => x.SearchPassengerByName(name)).Returns((Passenger)(null));
        
        var sut = new AuthServiceImpl(passengersRepoMock.Object);

        // Act
        var act = () => sut.Login(name, password);
        
        // Assert
        act.Should().NotThrow<PassengerNotFoundException>();
        act.Should().NotThrow<InvalidPasswordException>();
        UserContext.GetCurrentUserType().Should().Be(UserType.Manager);
    }
    
    [Theory]
    [InlineData("john", "123")]
    [InlineData("alice", "321")]
    public void Login_Successfully_AsPassenger_WhenCredentialsAreValid(string name, string password)
    {
        // Arrange
        var passengersRepoMock = new Mock<IPassengersRepository>();
        passengersRepoMock.Setup(x => x.SearchPassengerByName(name)).Returns(new Passenger {Name = name, Password = password});
        
        var sut = new AuthServiceImpl(passengersRepoMock.Object);

        // Act
        var act = () => sut.Login(name, password);
        
        // Assert
        act.Should().NotThrow<PassengerNotFoundException>();
        act.Should().NotThrow<InvalidPasswordException>();
        UserContext.GetCurrentUserType().Should().Be(UserType.Passenger);
    }

    [Fact]
    public void FailToLogin_WhenNameIsNotFound()
    {
        // Arrange
        var fixture = new Fixture();
        var name = fixture.Create<string>();
        var password = fixture.Create<string>();
        
        var passengersRepoMock = new Mock<IPassengersRepository>();
        passengersRepoMock.Setup(x => x.SearchPassengerByName(name)).Returns((Passenger)null);
        
        var sut = new AuthServiceImpl(passengersRepoMock.Object);

        // Act
        var act = () => sut.Login(name, password);
        
        // Assert
        act.Should().Throw<PassengerNotFoundException>()
            .WithMessage($"Passenger '{name}' not found.");
        UserContext.CurrentUser.Should().Be(null);
    }
    
    [Fact]
    public void FailToLogin_WhenPasswordIsInvalid()
    {
        // Arrange
        const string name = "john";
        const string password = "123";
        const string wrongPassword = "321";
        
        var passengersRepoMock = new Mock<IPassengersRepository>();
        passengersRepoMock.Setup(x => x.SearchPassengerByName(name)).Returns(new Passenger {Name = name, Password = password});
        
        var sut = new AuthServiceImpl(passengersRepoMock.Object);

        // Act
        var act = () => sut.Login(name, wrongPassword);
        
        // Assert
        act.Should().Throw<InvalidPasswordException>()
            .WithMessage("Invalid password.");
        UserContext.CurrentUser.Should().Be(null);
    }


    public void Dispose()
    {
        UserContext.ResetCurrentUser();
    }
}