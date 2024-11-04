using CrocusFitnes.Dto_s;
using CrocusFitnes.Filters;
using CrocusFitnes.Responces;
using CrocusFitnes.Result;
using GymSphere.Filters;

namespace GymSphere.Services.SessionService;

public interface ISessionService
{
    Task<Result<PagedResponse<IEnumerable<SessionReadInfo>>>> GetSessions(SessionFilter filter);
    Task<Result<SessionReadInfo>> GetSessionById(int id);
    Task<BaseResult> CreateSession(SessionCreateInfo info);
    Task<BaseResult> UpdateSession(int id, SessionUpdateInfo info);
    Task<BaseResult> DeleteSession(int id);
}
