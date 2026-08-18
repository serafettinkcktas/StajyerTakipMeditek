using Application.Command.Intern;
using Application.Command.Mentor;
using Application.UseCases.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class AdminController(
    CreateRoleUseCase createRoleUseCase,
    AddMentorUseCase addMentorUseCase,
    AddInternUseCase addInternUseCase,
    GetMentorsUseCase getMentorsUseCase,
    GetInternsUseCase getInternsUseCase) : ControllerBase
{
    private readonly CreateRoleUseCase _createRoleUseCase = createRoleUseCase;
    private readonly AddMentorUseCase _addMentorUseCase = addMentorUseCase;
    private readonly AddInternUseCase _addInternUseCase = addInternUseCase;
    private readonly GetMentorsUseCase _getMentorsUseCase = getMentorsUseCase;
    private readonly GetInternsUseCase _getInternsUseCase = getInternsUseCase;

    /// <summary>
    /// Yeni bir rol oluşturur (Admin, Mentor, Stajyer vb.)
    /// </summary>
    [HttpPost]
    // [Authorize(Roles = "Admin")]
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
   // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddMentor([FromBody] CreateMentorCommand command)
    {
        var result = await _addMentorUseCase.AddMentorAsync(command);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Yeni bir stajyer ekler. Admin temel bilgileri girer, sistem otomatik şifre üretir.
    /// </summary>
    [HttpPost]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddIntern([FromBody] CreateInternCommand command)
    {
        var result = await _addInternUseCase.AddInternAsync(command);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Tüm aktif mentorları listeler.
    /// </summary>
    [HttpGet]
    // [Authorize(Roles = "Admin,Mentor")]
    public async Task<IActionResult> GetMentors()
    {
        var result = await _getMentorsUseCase.GetMentorsAsync();
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Tüm aktif stajyerleri listeler.
    /// </summary>
    [HttpGet]
    // [Authorize(Roles = "Admin,Mentor")]
    public async Task<IActionResult> GetInterns()
    {
        var result = await _getInternsUseCase.GetInternsAsync();
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }
}
