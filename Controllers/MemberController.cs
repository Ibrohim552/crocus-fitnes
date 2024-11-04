using AutoMapper;
using CrocusFitnes.Dto_s;
using CrocusFitnes.Entities;
using CrocusFitnes.Responces;
using CrocusFitnes.Result;
using GymSphere.Filters;
using GymSphere.Services.MemberService;
using Microsoft.AspNetCore.Mvc;

namespace GymSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public MemberController(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers([FromQuery] MemberFilter filter)
        {
            var result = await _memberService.GetMembers(filter);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var result = await _memberService.GetMemberById(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember([FromBody] MemberCreateInfo memberCreateInfo)
        {
            var result = await _memberService.CreateMember(memberCreateInfo);
            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] MemberUpdateInfo memberUpdateInfo)
        {
            var result = await _memberService.UpdateMember(id, memberUpdateInfo);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var result = await _memberService.DeleteMember(id);
            return result.ToActionResult();
        }
    }
}
