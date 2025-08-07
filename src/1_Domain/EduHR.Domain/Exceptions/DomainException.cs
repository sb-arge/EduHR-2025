namespace EduHR.Domain.Exceptions;

/// <summary>
/// Represents errors that occur during domain logic execution (business rule violations).
/// This serves as a base class for all custom domain exceptions.
/// </summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }

    protected DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}