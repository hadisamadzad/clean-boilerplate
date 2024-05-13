using System;
using Identity.Application.Helpers;
using Identity.Application.Types.Configs;
using Xunit;

namespace Identity.Tests.Services.Application.Helpers;

public class JwtHelperTests
{
    public JwtHelperTests()
    {
        JwtHelper.Config = new SecurityTokenConfig
        {
            AccessTokenSecretKey = "@fake-access$security",
            AccessTokenLifetime = new TimeSpan(),
            RefreshTokenSecretKey = "@fake-refresh$security",
            RefreshTokenLifetime = new TimeSpan(),
            Issuer = "fake-issuer",
            Audience = "fake-audience"
        };
    }

    [Fact]
    public void Test_CreateJwtAccessToken_CanGenerateValidAccessToken()
    {
        // Arrange
        const int userId = 1;
        const string email = "fake-email";

        // Act
        var token = JwtHelper.CreateJwtAccessToken(userId, email);
        var isValid = JwtHelper.IsValidJwtAccessToken(token);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Test_CreateJwtRefreshToken_CanGenerateValidRefreshToken()
    {
        // Arrange
        const int userId = 1;
        const string email = "fake-email";

        // Act
        var token = JwtHelper.CreateJwtRefreshToken(userId, email);
        var isValid = JwtHelper.IsValidJwtRefreshToken(token);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Test_IsValidJwtAccessToken_ReturnFalseWhenInvalidTokenIsProvided()
    {
        // Act
        var isValid = JwtHelper.IsValidJwtAccessToken("a-fake-jwt-token");

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void Test_IsValidJwtRefreshToken_ReturnFalseWhenInvalidTokenIsProvided()
    {
        // Act
        var isValid = JwtHelper.IsValidJwtRefreshToken("a-fake-jwt-token");

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void Test_IsValidJwtAccessToken_ReturnFalseWhenValidRefreshTokenIsProvided()
    {
        // Arrange
        const int userId = 1;
        const string email = "fake-email";

        // Act
        var token = JwtHelper.CreateJwtRefreshToken(userId, email);
        var isValid = JwtHelper.IsValidJwtAccessToken(token);

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void Test_IsValidJwtRefreshToken_ReturnFalseWhenValidAccessTokenIsProvided()
    {
        // Arrange
        const int userId = 1;
        const string email = "fake-email";

        // Act
        var token = JwtHelper.CreateJwtAccessToken(userId, email);
        var isValid = JwtHelper.IsValidJwtRefreshToken(token);

        // Assert
        Assert.False(isValid);
    }
}
