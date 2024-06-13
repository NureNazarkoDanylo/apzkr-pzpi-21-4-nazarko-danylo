namespace WashingMachineManagementApi.Application.Common.Exceptions;

public class RenewAccessTokenException : Exception 
{
    public RenewAccessTokenException()
        : base() { }

    public RenewAccessTokenException(string message)
        : base(message) { }
}

