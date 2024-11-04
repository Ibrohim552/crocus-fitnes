namespace CrocusFitnes.Dto_s;

public readonly record struct BookingBaseInfo(
    int MemberId,
    int SessionId,
    string Status);

public readonly record struct BookingReadInfo(
    int Id,
    BookingBaseInfo BookingBaseInfo);

public readonly record struct BookingCreateInfo(
    BookingBaseInfo BookingBaseInfo);

public readonly record struct BookingUpdateInfo(
    BookingBaseInfo BookingBaseInfo);