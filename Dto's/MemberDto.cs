namespace CrocusFitnes.Dto_s;

public readonly record struct MemberBaseInfo(
    string FirstName,
    string LastName,
    string Email,
    string Phone);

public readonly record struct MemberReadInfo(
    int Id,
    MemberBaseInfo MemberBaseInfo,
    DateTime DateOfBirth,
    string Status,
    int MembershipId);

public readonly record struct MemberCreateInfo(
    MemberBaseInfo MemberBaseInfo,
    DateTime DateOfBirth,
    int MembershipId);

public readonly record struct MemberUpdateInfo(
    MemberBaseInfo MemberBaseInfo,
    DateTime DateOfBirth,
    string Status);