using CrocusFitnes.Entities;

namespace CrocusFitnes.Dto_s;

public readonly record struct TrainerBaseInfo(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Specialization);

public readonly record struct TrainerReadInfo(
    int Id,
    TrainerBaseInfo TrainerBaseInfo,
    double Rating);

public readonly record struct TrainerCreateInfo(
    TrainerBaseInfo TrainerBaseInfo);

public readonly record struct TrainerUpdateInfo(
    TrainerBaseInfo TrainerBaseInfo);
