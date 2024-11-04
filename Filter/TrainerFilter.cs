using CrocusFitnes.Filter;

namespace GymSphere.Filters;

public record TrainerFilter(
    string? FirstName,
    string? Email,
    string? Phone,
    string? Address,
    int? ExperienceYears,
    string? Specialization) : BaseFilter;