namespace Application.Common.Models;

public enum ResultCode
{
    Success = 1,
    EmailExists = 2,
    RoleNotFound = 3,
    RoleAlreadyExists = 4,
    NotFound = 5,
    ValidationError = 6,
    UnexpectedError = 7,
    Unauthorized = 8
}