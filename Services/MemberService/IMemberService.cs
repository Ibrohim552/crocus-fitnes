using CrocusFitnes.Dto_s;
using CrocusFitnes.Responces;
using CrocusFitnes.Result;
using GymSphere.Filters;

namespace GymSphere.Services.MemberService;

public interface IMemberService
{
    Task<Result<PagedResponse<IEnumerable<MemberReadInfo>>>> GetMembers(MemberFilter filter);
    Task<Result<MemberReadInfo>> GetMemberById(int id);
    Task<BaseResult> CreateMember(MemberCreateInfo info);
    Task<BaseResult> UpdateMember(int id, MemberUpdateInfo info);
    Task<BaseResult> DeleteMember(int id);
}