using CrocusFitnes.Filter;

namespace CrocusFitnes.Filters;

public record SessionFilter(
    string? Name,
    int? TrainerId,
    string? Location,
    int? MaxParticipants,
    DateTime? Duration,
    DateTime? StartTime) : BaseFilter;
