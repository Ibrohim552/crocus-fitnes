using AutoMapper;
using CrocusFitnes;
using CrocusFitnes.Dto_s;
using CrocusFitnes.Entities;
using CrocusFitnes.Filters;
using CrocusFitnes.Responces;
using CrocusFitnes.Result;
using GymSphere.Filters;
using Microsoft.EntityFrameworkCore;

namespace GymSphere.Services.SessionService;

public sealed class SessionService : ISessionService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public SessionService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PagedResponse<IEnumerable<SessionReadInfo>>>> GetSessions(SessionFilter filter)
    {
        IQueryable<Session> sessions = _context.Session;

        if (!string.IsNullOrEmpty(filter.Name))
            sessions = sessions.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

        if (filter.TrainerId.HasValue)
            sessions = sessions.Where(x => x.TrainerId == filter.TrainerId);

        if (!string.IsNullOrEmpty(filter.Location))
            sessions = sessions.Where(x => x.Location.ToLower().Contains(filter.Location.ToLower()));

        if (filter.MaxParticipants.HasValue)
            sessions = sessions.Where(x => x.MaxParticipants <= filter.MaxParticipants);

        if (filter.Duration.HasValue)
            sessions = sessions.Where(x => x.Duration == filter.Duration);

        if (filter.StartTime.HasValue)
            sessions = sessions.Where(x => x.StartTime >= filter.StartTime);

        int count = await sessions.CountAsync();

        var pagedSessions = sessions
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize);

        var sessionDtos = _mapper.ProjectTo<SessionReadInfo>(pagedSessions);

        var response = PagedResponse<IEnumerable<SessionReadInfo>>.Create(filter.PageNumber, filter.PageSize, count, sessionDtos);

        return Result<PagedResponse<IEnumerable<SessionReadInfo>>>.Success(response);
    }

    public async Task<Result<SessionReadInfo>> GetSessionById(int id)
    {
        var session = await _context.Session.FirstOrDefaultAsync(x => x.Id == id);

        return session == null
            ? Result<SessionReadInfo>.Failure(Error.NotFound())
            : Result<SessionReadInfo>.Success(_mapper.Map<SessionReadInfo>(session));
    }

    public async Task<BaseResult> CreateSession(SessionCreateInfo info)
    {
        if (info == null)
            return BaseResult.Failure(Error.NotFound());

        var session = _mapper.Map<Session>(info);
        await _context.Session.AddAsync(session);
        int res = await _context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> UpdateSession(int id, SessionUpdateInfo info)
    {
        var existingSession = await _context.Session.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (existingSession == null)
            return BaseResult.Failure(Error.NotFound());

        _mapper.Map(info, existingSession);
        int res = await _context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteSession(int id)
    {
        var existingSession = await _context.Session.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (existingSession == null)
            return BaseResult.Failure(Error.NotFound());

        _context.Session.Remove(existingSession);
        int res = await _context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not deleted"))
            : BaseResult.Success();
    }
}
