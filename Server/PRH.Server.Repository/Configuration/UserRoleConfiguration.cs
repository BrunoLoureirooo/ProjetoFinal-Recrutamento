using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRH.Server.Entities.Models;

namespace PRH.Server.Repository.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                UserId = UserConfiguration.ADMIN.ToString(),
                RoleId = RoleConfiguration.ROLE_ADMINISTRADOR.ToString(),                
            },
            new IdentityUserRole<string>
            {
                UserId = UserConfiguration.ANA_PINTO.ToString(),
                RoleId = RoleConfiguration.ROLE_FUNCIONARIO.ToString()
            },
            new IdentityUserRole<string>
            {
                UserId = UserConfiguration.ANA_CASACO.ToString(),
                RoleId = RoleConfiguration.ROLE_FUNCIONARIO.ToString()
            },
            new IdentityUserRole<string>
            {
                UserId = UserConfiguration.CARLA_AZEVEDO.ToString(),
                RoleId = RoleConfiguration.ROLE_FUNCIONARIO.ToString()
            },
            new IdentityUserRole<string>
            {
                UserId = UserConfiguration.FILIPE_DIAS.ToString(),
                RoleId = RoleConfiguration.ROLE_FUNCIONARIO.ToString()
            },
            new IdentityUserRole<string>
            {
                UserId = UserConfiguration.FILOMENA_REBELO.ToString(),
                RoleId = RoleConfiguration.ROLE_FUNCIONARIO.ToString()
            },
            new IdentityUserRole<string>
            {
                UserId = UserConfiguration.JOAO_FONSECA.ToString(),
                RoleId = RoleConfiguration.ROLE_FUNCIONARIO.ToString()
            },
            new IdentityUserRole<string>
            {
                UserId = UserConfiguration.MARIA_ALVES.ToString(),
                RoleId = RoleConfiguration.ROLE_FUNCIONARIO.ToString()
            },
            new IdentityUserRole<string>
            {
                UserId = UserConfiguration.SANDRA_CONCEICAO.ToString(),
                RoleId = RoleConfiguration.ROLE_FUNCIONARIO.ToString()
            },
            new IdentityUserRole<string>
            {
                UserId = UserConfiguration.LUIS_NEVES.ToString(),
                RoleId = RoleConfiguration.ROLE_FUNCIONARIO.ToString()
            },
            new IdentityUserRole<string>
            {
                UserId = UserConfiguration.OLGA_CUNHA.ToString(),
                RoleId = RoleConfiguration.ROLE_FUNCIONARIO.ToString()
            }
        );
    }
}
