using Identity.Application.Helpers;
using Xunit;

namespace Identity.Tests.Services.Application.Helpers;

public class PasswordHelperTests
{
    private const string SamplePassword = "Te$tPa$$w0rD";

    [Fact]
    public void Test_CheckPasswordHash_ShouldReturnTrue_WhenHashedPasswordIsCorrect()
    {
        // Arrange
        var hash = PasswordHelper.Hash(SamplePassword);

        // Act
        var checkResult = PasswordHelper.CheckPasswordHash(hash, SamplePassword);

        // Assert
        Assert.True(checkResult);
    }

    [Theory]
    [InlineData("1234")]
    [InlineData("123456")]
    public void Test_CheckPasswordStrength_ShouldReturnVeryWeakPasswordScore(string password)
    {
        // Arrange

        // Act
        var score = PasswordHelper.CheckStrength(password);

        // Assert
        Assert.Equal(PasswordScore.VeryWeak, score);
    }

    [Theory]
    [InlineData("Aa11369")]
    public void Test_CheckPasswordStrength_ShouldReturnWeakPasswordScore(string password)
    {
        // Arrange

        // Act
        var score = PasswordHelper.CheckStrength(password);

        // Assert
        Assert.Equal(PasswordScore.Weak, score);
    }

    [Theory]
    [InlineData("Pa$$123")]
    [InlineData("Pass12!")]
    [InlineData("Pass1234")]
    public void Test_CheckPasswordStrength_ShouldReturnMediumPasswordScore(string password)
    {
        // Arrange

        // Act
        var score = PasswordHelper.CheckStrength(password);

        // Assert
        Assert.Equal(PasswordScore.Medium, score);
    }

    [Theory]
    [InlineData("PaS$12!#")]
    public void Test_CheckPasswordStrength_ShouldReturnStrongPasswordScore(string password)
    {
        // Arrange

        // Act
        var score = PasswordHelper.CheckStrength(password);

        // Assert
        Assert.Equal(PasswordScore.Strong, score);
    }

    [Theory]
    [InlineData("PaS$123456!#")]
    public void Test_CheckPasswordStrength_ShouldReturnVeryStrongPasswordScore(string password)
    {
        // Arrange

        // Act
        var score = PasswordHelper.CheckStrength(password);

        // Assert
        Assert.Equal(PasswordScore.VeryStrong, score);
    }
}
