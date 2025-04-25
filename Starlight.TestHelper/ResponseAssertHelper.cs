using Starlight.Models;

using System.Net;

using Xunit;

namespace Starlight.TestHelper;

public static class ResponseAssertHelper
{
    public static void AssertSuccessResponse(DefaultResponse response)
    {
        Assert.Equal(HttpStatusCode.OK, response.HttpStatus);
        Assert.True(string.IsNullOrWhiteSpace(response.Message));
    }

    public static void AssertSuccessResponse(DefaultResponse response, HttpStatusCode expectedStatus)
    {
        Assert.Equal(expectedStatus, response.HttpStatus);
    }

    public static T AssertSuccessResponse<T>(DefaultResponse response)
    {
        Assert.Equal(HttpStatusCode.OK, response.HttpStatus);
        Assert.True(string.IsNullOrWhiteSpace(response.Message));
        Assert.True(response.ObjectResponse is T);

        return (T)response.ObjectResponse;
    }

    public static void AssertSuccessResponse<T>(DefaultResponse response, T expected, HttpStatusCode status, string message)
    {
        Assert.Equal(status, response.HttpStatus);
        Assert.Equal(message, response.Message);
        Assert.IsType<T>(response.ObjectResponse);
        Assert.Equal(expected, response.ObjectResponse);
    }

    public static void AssertSuccessResponse(DefaultResponse response, HttpStatusCode status, string message)
    {
        Assert.Equal(status, response.HttpStatus);
        Assert.Equal(message, response.Message);
    }

    public static void AssertErrorResponse(DefaultResponse response, HttpStatusCode expectedStatus, string expectedMessage)
    {
        Assert.NotNull(response);
        Assert.Equal(expectedStatus, response.HttpStatus);
        Assert.Equal(expectedMessage, response.Message);
        Assert.Null(response.ObjectResponse);
    }

    public static void AssertErrorResponse(DefaultResponse response, HttpStatusCode expectedStatus)
    {
        Assert.NotNull(response);
        Assert.Equal(expectedStatus, response.HttpStatus);
    }
}
