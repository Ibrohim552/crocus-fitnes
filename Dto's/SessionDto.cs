namespace CrocusFitnes.Dto_s;

public readonly record struct SessionBaseInfo(
    string Name,
    string Location,
    TimeSpan Duration,
    int MaxParticipants);

public readonly record struct SessionReadInfo(
    int Id,
    SessionBaseInfo SessionBaseInfo,
    int TrainerId,
    DateTime StartTime);

public readonly record struct SessionCreateInfo(
    SessionBaseInfo SessionBaseInfo,
    int TrainerId,
    DateTime StartTime);

public readonly record struct SessionUpdateInfo(
    SessionBaseInfo SessionBaseInfo,
    int TrainerId,
    DateTime StartTime);