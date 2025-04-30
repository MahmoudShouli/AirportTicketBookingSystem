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
    private readonly Mock<IPassengersRepository> _mockRepo;
    private readonly AuthServiceImpl _sut;
    public UserShould()
    {
        _mockRepo = new();
        _sut = new(_mockRepo.Object);
    }
    
    [Trait("Login", "Success")]
    [Fact]
    public void Login_Successfully_AsManager_WhenCredentialsAreValid()
    {
        // Arrange
        const string name = "mahmoud";
        const string password = "123";
        
        _mockRepo.Setup(x => x.SearchPassengerByName(name)).Returns(() => null);

        // Act
        var result = _sut.Login(name, password);
        
        // Assert
        result.Name.Should().Be(name);
        result.Password.Should().Be(password);
        UserContext.CurrentUser.Should().BeSameAs(result);
        UserContext.GetCurrentUserType().Should().Be(UserType.Manager);
    }
    
    [Trait("Login", "Success")]
    [Theory]
    [InlineData("john", "123")]
    [InlineData("alice", "321")]
    public void Login_Successfully_AsPassenger_WhenCredentialsAreValid(string name, string password)
    {
        // Arrange
        _mockRepo.Setup(x => x.SearchPassengerByName(name)).Returns(new Passenger {Name = name, Password = password});

        // Act
        var result = _sut.Login(name, password);
        
        // Assert
        result.Name.Should().Be(name);
        result.Password.Should().Be(password);
        UserContext.CurrentUser.Should().BeSameAs(result);
        UserContext.GetCurrentUserType().Should().Be(UserType.Passenger);
    }

    [Trait("Login", "Failure")]
    [Fact]
    public void FailToLogin_WhenNameIsNotFound()
    {
        // Arrange
        var fixture = new Fixture();
        var name = fixture.Create<string>();
        var password = fixture.Create<string>();
        
       _mockRepo.Setup(x => x.SearchPassengerByName(name)).Returns(() => null);
        
        // Act
        var act = () => _sut.Login(name, password);
        
        // Assert
        act.Should().Throw<NotFoundException>()
            .WithMessage($"Passenger with name {name} not found.");
        UserContext.CurrentUser.Should().Be(null);
    }
    
    [Trait("Login", "Failure")]
    [Fact]
    public void FailToLogin_WhenPasswordIsInvalid()
    {
        // Arrange
        const string name = "john";
        const string password = "123";
        const string wrongPassword = "321";
        
        _mockRepo.Setup(x => x.SearchPassengerByName(name)).Returns(new Passenger {Name = name, Password = password});
        
        // Act
        var act = () => _sut.Login(name, wrongPassword);
        
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