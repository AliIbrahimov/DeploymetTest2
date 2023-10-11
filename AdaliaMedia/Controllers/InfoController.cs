using BusinessLayer.Services.Abstract;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.DTOs.Blog;
using EntityLayer.DTOs.Info;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdaliaMedia.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InfoController : ControllerBase
{
    private readonly IInfoService _infoService;

    public InfoController(IInfoService infoService)
    {
        _infoService = infoService;
    }


    [HttpGet]
    public async Task<IActionResult> GetInfo()
    {
        var result = await _infoService.GetInfoAsync();
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateInfo(int id, [FromForm] InfoPostDto info)
    {
        var result = await _infoService.UpdateInfo(id,info);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}
