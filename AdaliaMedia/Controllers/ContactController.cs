using BusinessLayer.Services.Abstract;
using EntityLayer.DTOs.Blog;
using EntityLayer.DTOs.Contact;
using Microsoft.AspNetCore.Mvc;

namespace AdaliaMedia.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }
    [HttpGet("contacts")]

    public async Task<IActionResult> GetAll()
    {
        var result = await _contactService.GetAllContacts();
        if (result.Success) return Ok(result);
        return BadRequest();
    }
    [HttpGet("contact/{page}/{size}")]

    public async Task<IActionResult> GetContactsPaginated(int page,int size)
    {
        var result = await _contactService.GetAllPaginateAsync(page,size);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
    [HttpGet("contact/{id}")]

    public async Task<IActionResult> GetById(int id)
    {
        var result = await _contactService.GetByIdAsync(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
    [HttpPost("add")]

    public async Task<IActionResult> Add([FromForm] ContactPostDTO contact)
    {
        var result = await _contactService.CreateContact(contact);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
    [HttpDelete("delete{id}")]

    public async Task<IActionResult> HardDelete(int id)
    {
        var result = await _contactService.HardDeleteByIdAsync(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}
