namespace WashingMachineManagementApi.Application.Common.Exceptions;

public class RevokeRefreshTokenException : Exception
{
    public RevokeRefreshTokenException()
        : base() { }

    public RevokeRefreshTokenException(string message)
        : base(message) { }
}
