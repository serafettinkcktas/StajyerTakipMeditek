using Application.Command.Mentor;
using Application.UseCases.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class AdminController(CreateRoleUseCase createRoleUseCase, AddMentorUseCase addMentorUseCase) : ControllerBase
{
    private readonly CreateRoleUseCase _createRoleUseCase = createRoleUseCase;
    private readonly AddMentorUseCase _addMentorUseCase = addMentorUseCase;

    /// <summary>
    /// Yeni bir rol oluşturur (Admin, Mentor, Stajyer vb.)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        var result = await _createRoleUseCase.CreateRole(roleName);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Yeni bir mentor ekler. Admin sadece ad, soyad ve email girer.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddMentor([FromBody] CreateMentorCommand command)
    {
        var result = await _addMentorUseCase.AddMentorAsync(command);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }
}