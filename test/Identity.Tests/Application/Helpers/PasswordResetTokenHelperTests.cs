using System;
using Identity.Application.Helpers;
using Xunit;

namespace Identity.Tests.Services.Application.Helpers;

public class PasswordResetTokenHelperTests
{
    [Fact]
    public void Test_GeneratePasswordResetToken_CanGeneratePasswordResetToken()
    {
        // Arrange
        const string fakeEmail = "fake@email.com";
        var expiration = DateTime.UtcNow.Date.AddDays(1);

        // Act
        var token = PasswordResetTokenHelper.GeneratePasswordResetToken(fakeEmail, expiration);

        // Assert
        Assert.NotEmpty(token);
    }

    [Fact]
    public void Test_ReadPasswordResetToken_CanReadAValidPasswordResetToken()
    {
        // Arrange
        const string fakeEmail = "fake@email.com";
        var expiration = DateTime.UtcNow.Date.AddDays(1);
        var token = PasswordResetTokenHelper.GeneratePasswordResetToken(fakeEmail, expiration);

        // Act
        var (succeeded, email) = PasswordResetTokenHelper.ReadPasswordResetToken(token);

        // Assert
        Assert.True(succeeded);
        Assert.Equal(fakeEmail, email);
    }

    [Fact]
    public void Test_ReadPasswordResetToken_CanReadAValidPasswordResetToken2()
    {
        // Arrange
        const string fakeEmail = "fake@email.com";
        var expiration = DateTime.UtcNow.Date;
        var token = PasswordResetTokenHelper.GeneratePasswordResetToken(fakeEmail, expiration);

        // Act
        var (succeeded, email) = PasswordResetTokenHelper.ReadPasswordResetToken(token);

        // Assert
        Assert.False(succeeded);
        Assert.Empty(email);
    }
}
