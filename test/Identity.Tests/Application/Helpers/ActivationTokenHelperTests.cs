using System;
using Identity.Application.Helpers;
using Xunit;

namespace Identity.Tests.Application.Helpers;

public class ActivationTokenHelperTests
{
    [Fact]
    public void Test_GenerateActivationToken_CanGenerateActivationToken()
    {
        // Arrange
        const string email = "fake@email.com";
        var expiration = DateTime.UtcNow.Date.AddDays(2);

        // Act
        var token = ActivationTokenHelper.GenerateActivationToken(email, expiration);

        // Assert
        Assert.NotEmpty(token);
    }

    [Fact]
    public void Test_ReadActivationToken_CanReadAValidActivationToken()
    {
        // Arrange
        const string fakeEmail = "fake@email.com";
        var expiration = DateTime.UtcNow.Date.AddDays(2);
        var token = ActivationTokenHelper.GenerateActivationToken(fakeEmail, expiration);

        // Act
        var (succeeded, email) = ActivationTokenHelper.ReadActivationToken(token);

        // Assert
        Assert.True(succeeded);
        Assert.Equal(fakeEmail, email);
    }

    [Fact]
    public void Test_ReadActivationToken_CanReadAValidActivationToken2()
    {
        // Arrange
        const string fakeEmail = "fake@email.com";
        var expiration = DateTime.UtcNow.Date;
        var token = ActivationTokenHelper.GenerateActivationToken(fakeEmail, expiration);

        // Act
        var (succeeded, email) = ActivationTokenHelper.ReadActivationToken(token);

        // Assert
        Assert.False(succeeded);
        Assert.Empty(email);
    }
}
