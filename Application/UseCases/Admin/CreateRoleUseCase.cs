using Application.Common.Models;
using Application.DTOs;
using Domain.Entity;
using Domain.Interface;

namespace Application.UseCases.Admin;

public class CreateRoleUseCase(IRoleRepository roleRepository)
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    /// <summary>
    /// Yeni bir rol oluşturur. Aynı isimde rol varsa hata döner.
    /// </summary>
    public async Task<Result<RoleDto>> CreateRole(string roleName)
    {
        var exists = await _roleRepository.GetRoleByNameAsync(roleName);
        if (exists is not null)
            return Result<RoleDto>.Failure(ResultCode.RoleAlreadyExists, "Bu rol zaten mevcut");

        var role = new Role
        {
            Id = Guid.NewGuid(),
            Name = roleName
        };

        var isCreated = await _roleRepository.CreateRoleAsync(role);
        if (!isCreated)
            return Result<RoleDto>.Failure(ResultCode.UnexpectedError, "Rol oluşturulamadı");

        var roleDto = new RoleDto
        {
            Id = role.Id,
            Name = role.Name
        };

        return Result<RoleDto>.Success(roleDto, "Rol başarıyla oluşturuldu");
    }
}