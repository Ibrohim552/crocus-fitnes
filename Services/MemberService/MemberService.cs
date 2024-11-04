using AutoMapper;
using AutoMapper.Execution;
using CrocusFitnes;
using CrocusFitnes.Dto_s;
using CrocusFitnes.Entities;
using CrocusFitnes.Responces;
using CrocusFitnes.Result;
using GymSphere.Filters;
using Microsoft.EntityFrameworkCore;

namespace GymSphere.Services.MemberService;

public sealed class MemberService : IMemberService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public MemberService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PagedResponse<IEnumerable<MemberReadInfo>>>> GetMembers(MemberFilter filter)
    {
        IQueryable<Members> members = _context.Members;

        int count = await members.CountAsync();

        var pagedMembers = members
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize);

        var memberDtos = _mapper.ProjectTo<MemberReadInfo>(pagedMembers);

        var response = PagedResponse<IEnumerable<MemberReadInfo>>.Create(filter.PageNumber, filter.PageSize, count, memberDtos);

        return Result<PagedResponse<IEnumerable<MemberReadInfo>>>.Success(response);
    }

    public async Task<Result<MemberReadInfo>> GetMemberById(int id)
    {
        Members? result = await _context.Members.FirstOrDefaultAsync(x=>x.Id==id);

        return result is null
            ? Result<MemberReadInfo>.Failure(Error.NotFound())
            : Result<MemberReadInfo>.Success(_mapper.Map<MemberReadInfo>(result));
    }

    public async Task<BaseResult> CreateMember(MemberCreateInfo info)
    {
        if (info == null)
            return BaseResult.Failure(Error.NotFound());

        var member = _mapper.Map<Members>(info);
        await _context.Members.AddAsync(member);
        int res = await _context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> UpdateMember(int id, MemberUpdateInfo info)
    {
        Members? existingMember = await _context.Members.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (existingMember is null)
            return BaseResult.Failure(Error.NotFound());

        _mapper.Map(info, existingMember);
        int res = await _context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteMember(int id)
    {
        Members? existingMember = await _context.Members.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (existingMember is null)
            return BaseResult.Failure(Error.NotFound());

        _context.Members.Remove(existingMember);
        int res = await _context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not deleted"))
            : BaseResult.Success();
    }
}
