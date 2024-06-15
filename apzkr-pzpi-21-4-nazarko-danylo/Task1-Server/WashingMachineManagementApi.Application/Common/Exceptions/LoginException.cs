namespace WashingMachineManagementApi.Application.Common.Exceptions;

public class LoginException : Exception
{
    public LoginException()
        : base() { }

    public LoginException(string message)
        : base(message) { }
}
