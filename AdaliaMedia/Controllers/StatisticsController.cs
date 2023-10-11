using BusinessLayer.Services.Abstract;
using BusinessLayer.Services.Concrete;
using EntityLayer.DTOs.Blog;
using EntityLayer.DTOs.Statistic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdaliaMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _statisticService.GetStatistic(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPut("update")]

        public async Task<IActionResult> Update(int id, [FromForm] PostStatisticDto statistic)
        {
            var result = await _statisticService.UpdateStatistic(id, statistic);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
