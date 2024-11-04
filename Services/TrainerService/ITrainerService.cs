using CrocusFitnes.Dto_s;
using CrocusFitnes.Responces;
using CrocusFitnes.Result;
using GymSphere.Filters;

namespace GymSphere.Services.TrainerService;

public interface ITrainerService
{
    Task<Result<PagedResponse<IEnumerable<TrainerReadInfo>>>> GetTrainers(TrainerFilter filter);
    Task<Result<TrainerReadInfo>> GetTrainerById(int id);
    Task<BaseResult> CreateTrainer(TrainerCreateInfo info);
    Task<BaseResult> UpdateTrainer(int id,TrainerUpdateInfo info);
    Task<BaseResult> DeleteTrainer(int id);
}