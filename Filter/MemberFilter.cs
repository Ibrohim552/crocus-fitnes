using CrocusFitnes.Filter;
namespace GymSphere.Filters;


public record MemberFilter(
    string? FullName,
    int? Age,
    string? Email,
    string? PhoneNumber,
    string? Address,
    DateTimeOffset? RegistrationDate):BaseFilter;