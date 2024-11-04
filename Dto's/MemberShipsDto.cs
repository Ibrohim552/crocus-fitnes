namespace CrocusFitnes.Dto_s;

public readonly record struct MembershipBaseInfo(
    string Type,
    decimal Price,
    int DurationInDays);

public readonly record struct MembershipReadInfo(
    int Id,
    MembershipBaseInfo MembershipBaseInfo);

public readonly record struct MembershipCreateInfo(
    MembershipBaseInfo MembershipBaseInfo);

public readonly record struct MembershipUpdateInfo(
    MembershipBaseInfo MembershipBaseInfo);