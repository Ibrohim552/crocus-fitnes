using AutoMapper;
using CrocusFitnes;
using CrocusFitnes.Dto_s;
using CrocusFitnes.Entities;
using CrocusFitnes.Responces;
using CrocusFitnes.Result;
using GymSphere.Filters;
using Microsoft.EntityFrameworkCore;

namespace GymSphere.Services.TrainerService;

public sealed class TrainerService : ITrainerService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public TrainerService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PagedResponse<IEnumerable<TrainerReadInfo>>>> GetTrainers(TrainerFilter filter)
    {
        IQueryable<Trainers> trainers = _context.Trainers;

        if (!string.IsNullOrEmpty(filter.FirstName))
            trainers = trainers.Where(x => x.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
           
        if (!string.IsNullOrEmpty(filter.Email))
            trainers = trainers.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));
        
        if (!string.IsNullOrEmpty(filter.Phone))
            trainers = trainers.Where(x => x.Phone.ToLower().Contains(filter.Phone.ToLower()));
       
        if (filter.ExperienceYears.HasValue)
            trainers = trainers.Where(x => x.ExperienceYears == filter.ExperienceYears);

        int count = await trainers.CountAsync();

        var pagedTrainers = trainers
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize);

        var trainerDtos = _mapper.ProjectTo<TrainerReadInfo>(pagedTrainers);

        var response = PagedResponse<IEnumerable<TrainerReadInfo>>.Create(filter.PageNumber, filter.PageSize, count, trainerDtos);

        return Result<PagedResponse<IEnumerable<TrainerReadInfo>>>.Success(response);
    }

    public async Task<Result<TrainerReadInfo>> GetTrainerById(int id)
    {
        Trainers? result = await _context.Trainers.FirstOrDefaultAsync(x => x.Id == id);

        return result is null
            ? Result<TrainerReadInfo>.Failure(Error.NotFound())
            : Result<TrainerReadInfo>.Success(_mapper.Map<TrainerReadInfo>(result));
    }

    public async Task<BaseResult> CreateTrainer(TrainerCreateInfo info)
    {
        if (info == null)
            return BaseResult.Failure(Error.NotFound());

        var trainer = _mapper.Map<Trainers>(info);
        await _context.Trainers.AddAsync(trainer);
        int res = await _context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> UpdateTrainer(int id, TrainerUpdateInfo info)
    {
        Trainers? existingTrainer = await _context.Trainers.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (existingTrainer is null)
            return BaseResult.Failure(Error.NotFound());

        _mapper.Map(info, existingTrainer);
        int res = await _context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteTrainer(int id)
    {
        Trainers? existingTrainer = await _context.Trainers.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (existingTrainer is null)
            return BaseResult.Failure(Error.NotFound());

        _context.Trainers.Remove(existingTrainer);
        int res = await _context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not deleted"))
            : BaseResult.Success();
    }
}
