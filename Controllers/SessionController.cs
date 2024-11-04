using AutoMapper;
using CrocusFitnes.Dto_s;
using CrocusFitnes.Entities;
using CrocusFitnes.Filters;
using CrocusFitnes.Responces;
using CrocusFitnes.Result;
using GymSphere.Filters;
using GymSphere.Services.SessionService;
using Microsoft.AspNetCore.Mvc;

namespace GymSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public SessionController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSessions([FromQuery] SessionFilter filter)
        {
            var result = await _sessionService.GetSessions(filter);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSessionById(int id)
        {
            var result = await _sessionService.GetSessionById(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] SessionCreateInfo sessionCreateInfo)
        {
            var result = await _sessionService.CreateSession(sessionCreateInfo);
            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSession(int id, [FromBody] SessionUpdateInfo sessionUpdateInfo)
        {
            var result = await _sessionService.UpdateSession(id, sessionUpdateInfo);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var result = await _sessionService.DeleteSession(id);
            return result.ToActionResult();
        }
    }
}
