using CrocusFitnes.Filter;

namespace GymSphere.Filters;

public record BookingFilter(
    double? Price,
    DateTimeOffset? BookingDate,
    int? MemberId,
    int? ScheduleId):BaseFilter;