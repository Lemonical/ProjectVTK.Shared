using ProjectVTK.Shared.Commands;
using ProjectVTK.Shared.Commands.Data;
using ProjectVTK.Shared.Helpers;
using System.Text.Json;

namespace ProjectVTK.Shared.Tests;

public class SerializationTests
{
    // Arrange
    // Act
    // Assert

    [Theory]
    [InlineData("usernotuser", "thatPassword")]
    [InlineData("Testing", "verySecurePassword")]
    public void CreateLoginRequest(string username, string password)
    {
        // Arrange
        var loginData = new LoginCommandData
        {
            Username = username,
            Password = password
        };

        // Act
        var command = Command.CreateRequest(loginData);

        // Assert
        Assert.NotNull(command);
        Assert.DoesNotContain($"\"status\":\"null\"", command);
        Assert.DoesNotContain($"\"error_message\":\"null\"", command);
        Assert.DoesNotMatch($"\"id\":\"{Guid.Empty}\"", command);
        Assert.Contains($"\"protocol\":\"login\"", command);
        Assert.Contains($"\"username\":\"{username}\"", command);
        Assert.Contains($"\"password\":\"{password}\"", command);
        Assert.DoesNotContain($"\"session_id\":\"null\"", command);
    }

    [Theory]
    [InlineData("usernotuser", "thatPassword", CommandStatusCode.Success)]
    [InlineData("Testing", "verySecurePassword", CommandStatusCode.Failed)]
    public void DeserializeAndCreateLoginResponse(string username, string password, CommandStatusCode status)
    {
        // Arrange
        var loginData = new LoginCommandData
        {
            Username = username,
            Password = password
        };
        var json = Command.CreateRequest(loginData);

        // Act
        var cmdObject = JsonSerializer.Deserialize<Command>(json, JsonHelper.GetSerializerOptions());
        var response = Command.CreateResponse(cmdObject.Id.GetValueOrDefault(), cmdObject.Protocol, status,
            status == CommandStatusCode.Failed ? "Incorrect args" : null);

        // Assert
        Assert.NotNull(response);
        Assert.Contains($"\"status\":\"{status.ToString().ToLower()}\"", response);
        Assert.Contains($"\"id\":\"{cmdObject.Id}\"", response);
        Assert.Contains($"\"protocol\":\"login\"", response);
    }
}
