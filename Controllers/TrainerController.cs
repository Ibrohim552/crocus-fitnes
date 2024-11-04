using AutoMapper;
using CrocusFitnes.Dto_s;
using CrocusFitnes.Entities;
using CrocusFitnes.Responces;
using CrocusFitnes.Result;
using GymSphere.Filters;
using GymSphere.Services.TrainerService;
using Microsoft.AspNetCore.Mvc;

namespace GymSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        private readonly IMapper _mapper;

        public TrainerController(ITrainerService trainerService, IMapper mapper)
        {
            _trainerService = trainerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrainers([FromQuery] TrainerFilter filter)
        {
            var result = await _trainerService.GetTrainers(filter);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainerById(int id)
        {
            var result = await _trainerService.GetTrainerById(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrainer([FromBody] TrainerCreateInfo trainerCreateInfo)
        {
            var result = await _trainerService.CreateTrainer(trainerCreateInfo);
            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainer(int id, [FromBody] TrainerUpdateInfo trainerUpdateInfo)
        {
            var result = await _trainerService.UpdateTrainer(id, trainerUpdateInfo);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var result = await _trainerService.DeleteTrainer(id);
            return result.ToActionResult();
        }
    }
}
