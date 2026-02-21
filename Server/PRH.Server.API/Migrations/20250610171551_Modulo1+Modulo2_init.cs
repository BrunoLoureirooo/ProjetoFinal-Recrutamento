using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PRH.Server.API.Migrations
{
    /// <inheritdoc />
    public partial class Modulo1Modulo2_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lookups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Assinavel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departamentos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estabelecimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estabelecimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estabelecimentos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funcoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcoes_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GestorClientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GestorClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GestorClientes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GestorClientes_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recrutamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recrutamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recrutamentos_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomeUnico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsOpcional = table.Column<bool>(type: "bit", nullable: false),
                    TipoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campos_Lookups_TipoId",
                        column: x => x.TipoId,
                        principalTable: "Lookups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Formularios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomeUnico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formularios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formularios_Lookups_TipoId",
                        column: x => x.TipoId,
                        principalTable: "Lookups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentosNecessarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CargoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoDocumentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosNecessarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentosNecessarios_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentosNecessarios_TipoDocumentos_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaTipoDocumentos",
                columns: table => new
                {
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoDocumentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaTipoDocumentos", x => new { x.EmpresaId, x.TipoDocumentoId });
                    table.ForeignKey(
                        name: "FK_EmpresaTipoDocumentos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresaTipoDocumentos_TipoDocumentos_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoDocumentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Caminho = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Preenchivel = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Templates_TipoDocumentos_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipas_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vagas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecrutamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidaturaDisponivel = table.Column<bool>(type: "bit", maxLength: 1, nullable: false),
                    TipoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModalidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumVagas = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CargoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vagas_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vagas_Lookups_ModalidadeId",
                        column: x => x.ModalidadeId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vagas_Lookups_TipoId",
                        column: x => x.TipoId,
                        principalTable: "Lookups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vagas_Recrutamentos_RecrutamentoId",
                        column: x => x.RecrutamentoId,
                        principalTable: "Recrutamentos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OpcoesCampoLookups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcoesCampoLookups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcoesCampoLookups_Campos_CampoId",
                        column: x => x.CampoId,
                        principalTable: "Campos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FormularioCampo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormularioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormularioCampo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormularioCampo_Campos_CampoId",
                        column: x => x.CampoId,
                        principalTable: "Campos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormularioCampo_Formularios_FormularioId",
                        column: x => x.FormularioId,
                        principalTable: "Formularios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Caminho = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CandidaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoDocumentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentoAssinado = table.Column<bool>(type: "bit", nullable: false),
                    CamposPreenchidos = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentos_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documentos_TipoDocumentos_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumentos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstabelecimentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuncaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Funcionarios_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Funcionarios_Equipas_EquipaId",
                        column: x => x.EquipaId,
                        principalTable: "Equipas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Funcionarios_Estabelecimentos_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "Estabelecimentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Funcionarios_Funcoes_FuncaoId",
                        column: x => x.FuncaoId,
                        principalTable: "Funcoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Candidaturas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VagaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NIFcandidato = table.Column<int>(type: "int", maxLength: 9, nullable: false),
                    NomeCandidato = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactoCandidato = table.Column<int>(type: "int", maxLength: 9, nullable: false),
                    EmailCandidato = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidaturas_Lookups_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Lookups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidaturas_Vagas_VagaId",
                        column: x => x.VagaId,
                        principalTable: "Vagas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VagaFormularios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormularioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VagaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VagaFormularios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VagaFormularios_Formularios_FormularioId",
                        column: x => x.FormularioId,
                        principalTable: "Formularios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VagaFormularios_Vagas_VagaId",
                        column: x => x.VagaId,
                        principalTable: "Vagas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Entrevistas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntrevistadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notas = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ResultadoEntrevistaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrevistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entrevistas_Candidaturas_CandidaturaId",
                        column: x => x.CandidaturaId,
                        principalTable: "Candidaturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entrevistas_Funcionarios_EntrevistadorId",
                        column: x => x.EntrevistadorId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entrevistas_Lookups_ResultadoEntrevistaId",
                        column: x => x.ResultadoEntrevistaId,
                        principalTable: "Lookups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FormularioRespondidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CandidaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormularioRespondidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormularioRespondidos_Candidaturas_CandidaturaId",
                        column: x => x.CandidaturaId,
                        principalTable: "Candidaturas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CampoRespondidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Resposta = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FormularioRespondidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampoRespondidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampoRespondidos_FormularioRespondidos_FormularioRespondidoId",
                        column: x => x.FormularioRespondidoId,
                        principalTable: "FormularioRespondidos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a3e57c14-2b8f-4e9f-9d4a-7a2b172b0e48", null, "GestorCliente", "GESTORCLIENTE" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2e0f5a9b-9c94-4e3e-b8b0-8fd60b6ec7ce", 0, "6db9e8e1-5e87-4c3b-9e09-6b85ffb81e9f", "gestor@trigenius.pt", true, false, null, "Gestor Trigénius", "GESTOR@TRIGENIUS.PT", null, "AQAAAAIAAYagAAAAEB5OdZcAoQdlpOaOhtC96TOIrAozjSt9ycEICcr36ZFCs5IBpl8wWtoDgFKI2cvGAA==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3a4f7d5e-1c2a-4c73-8136-04e02b71a627", false, null });

            migrationBuilder.InsertData(
                table: "Cargos",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { new Guid("5251ef65-3843-461e-a0fc-417e27cd6c0f"), "Atendente" },
                    { new Guid("5bbef10e-f9e6-45ad-abcb-bc63bbf13fbe"), "Engenheiro de Software" },
                    { new Guid("66a77ab9-a4d7-4c11-9b2d-928782b3bd95"), "Contador" },
                    { new Guid("67dbb27d-53d7-4962-ab44-8f7a2c23a301"), "Recrutador" },
                    { new Guid("77552b58-f64b-4f22-8197-cbffd2aa7bfa"), "Assistente Administrativo" },
                    { new Guid("7f10ab2e-a96c-4bbb-9ebc-c8a2b5811b89"), "Especialista em Recursos Humanos" },
                    { new Guid("c3d91f85-7e42-4a6b-b2d0-8f1e37ac59b4"), "Porteiro" },
                    { new Guid("dc8faf27-b7b3-406e-82c1-ca1f9339a7ea"), "Comercial de Vendas" },
                    { new Guid("f0d844f3-8c29-4c6d-8042-9473ec8191aa"), "Analista de Dados" },
                    { new Guid("ffed9c07-3294-4b32-8c21-2de2bf2dbd32"), "Técnico de Manutenção" }
                });

            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { new Guid("0758d87d-1a91-48e5-bfbb-ff7ab7b12171"), "HEALTHCARE SOLUTION" },
                    { new Guid("1aaa8376-c563-4d60-8260-0f376660593e"), "MARKET X" },
                    { new Guid("35827598-d25d-4d08-b504-134c2331221d"), "MARKET LEADERS" },
                    { new Guid("57c4dfc3-afa3-4975-a682-b6f4ceeb9d32"), "CLEANING CO" },
                    { new Guid("5c23d6bf-ecdb-4b98-99bb-20447f5bc13c"), "TECH CORP" },
                    { new Guid("8588bf94-6622-4b1d-8f9f-d1b97ed7dc29"), "TECH INNOVATORS" },
                    { new Guid("b03e1560-9c6a-4881-9224-362e1775da64"), "FINANCE PLUS" },
                    { new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), "TRIGÉNIUS" },
                    { new Guid("fb717a87-3f6b-4e5f-97d6-9676945f3c0c"), "EDU INNOVATE" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Descricao", "Tipo" },
                values: new object[,]
                {
                    { new Guid("014533bb-205a-4f33-9ce3-2e7fc96dca99"), "Hibrido", "Modalidade" },
                    { new Guid("387604b5-a3a8-4646-b7e8-eebe825e4b1e"), "Reprovado", "Resultado" },
                    { new Guid("3b6a96f0-4b0b-447c-b906-fd7539d00f99"), "Encaminhado para proxima fase", "Resultado" },
                    { new Guid("3bdd48c4-ad73-4241-8823-17792de8f311"), "Virtual", "Modalidade" },
                    { new Guid("4facf236-b3fc-4cab-bdef-5214f674dfec"), "A Decorrer", "Estado" },
                    { new Guid("6cf2183a-8658-4382-811e-620072e1e3f8"), "Entrevista Agendada", "Resultado" },
                    { new Guid("6f6d7cc0-f4a7-4b7a-8fe4-890467b579f6"), "Aprovado", "Resultado" },
                    { new Guid("7f42b081-0924-487f-bef0-8edfb8b89766"), "Admitido", "Estado" },
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), "Experiencia Profissional", "Formulario" },
                    { new Guid("a7d2f9c5-3b4e-41d8-b0f6-8e1c7a52d9f3"), "string", "Campo" },
                    { new Guid("b2c3d4e5-f678-90ab-cdef-234567890123"), "Anexos", "Formulario" },
                    { new Guid("b91e4d76-2c3a-45f8-ae7d-58f2b0d9c184"), "Full-Time", "Vaga" },
                    { new Guid("b9b4c3d8-7a6f-41d9-a0e5-6b4c8d75f2e3"), "booleano", "Campo" },
                    { new Guid("c1c5d6e7-8f9a-42b1-b0d2-7a8b9c0d1e2f"), "lookup", "Campo" },
                    { new Guid("c9f7a5d2-4b3e-48d6-a0f8-7e1c2a59d3b4"), "anexo", "Campo" },
                    { new Guid("d5f1a8c3-79e2-4b0d-b6f7-91c4de38fa25"), "Part-Time", "Vaga" },
                    { new Guid("d5f20edc-3290-417f-b536-b5e053cdf376"), "Presencial", "Modalidade" },
                    { new Guid("d9c7a5f2-3b4e-48f6-a0d8-7f1e2c4a19b3"), "Dados Pessoais", "Formulario" },
                    { new Guid("e7f2a3d6-8b4c-49d7-e0f9-4d5c9b74e2c9"), "número", "Campo" },
                    { new Guid("f3a7d9c2-5b4e-41d8-b0f6-8e1c7a52d9f3"), "Contratação", "Formulario" },
                    { new Guid("f812d64f-118a-4905-8242-f6790aacb1f0"), "Rejeitado", "Estado" },
                    { new Guid("f8a3b2d7-9c5e-48d7-f1e0-3d6c9b85e4d1"), "data", "Campo" }
                });

            migrationBuilder.InsertData(
                table: "TipoDocumentos",
                columns: new[] { "Id", "Assinavel", "Nome" },
                values: new object[,]
                {
                    { new Guid("0085fcf5-3248-9296-8117-196221fc621b"), false, "Manual de Utilização" },
                    { new Guid("0e5f31a3-24d3-9154-7255-2f49881605ec"), false, "Contrato com Termo" },
                    { new Guid("1dfb7d05-ea51-4bfb-e44b-f690c0d267bb"), false, "Comprovativo médico e de segurança no trabalho" },
                    { new Guid("4ef64bb2-a5e3-aec9-7b0d-9402eb49e31a"), false, "Política de Privacidade" },
                    { new Guid("729b81db-7211-2292-33fe-d36d245a9ebb"), true, "Regulamento Interno" },
                    { new Guid("83648738-2354-20f9-841d-f04127fa7980"), true, "Termo de Responsabilidade" },
                    { new Guid("9519986b-c52a-73a6-ee45-3a46de2d84ea"), false, "Anexo" },
                    { new Guid("b28f569d-3505-a309-48f6-dc9bf13d4ff1"), true, "Contrato de Trabalho" },
                    { new Guid("b620e5f7-4752-c363-efd9-5d01b182c88d"), false, "Termo de Confidencialidade (NDA)" },
                    { new Guid("dd921d99-6365-f210-e2ce-cad7fc07eb54"), false, "Contrato de Prestação de Serviços" },
                    { new Guid("e5f5b964-d722-6f31-d6dd-6748f8011375"), false, "Política de Recursos Humanos" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a3e57c14-2b8f-4e9f-9d4a-7a2b172b0e48", "2e0f5a9b-9c94-4e3e-b8b0-8fd60b6ec7ce" });

            migrationBuilder.InsertData(
                table: "Campos",
                columns: new[] { "Id", "IsOpcional", "Nome", "NomeUnico", "TipoId" },
                values: new object[,]
                {
                    { new Guid("2699d419-f8f2-4862-b0e6-add5469c3b48"), false, "Correio Eletronico", "CORREIO_ELETRONICO", new Guid("a7d2f9c5-3b4e-41d8-b0f6-8e1c7a52d9f3") },
                    { new Guid("9de6573f-bebd-4277-a43f-02c74737ec9e"), false, "Nome Completo", "NOME_COMPLETO", new Guid("a7d2f9c5-3b4e-41d8-b0f6-8e1c7a52d9f3") },
                    { new Guid("b4c7d2a5-5e3f-49d8-b1f7-9e2c8a62e4d6"), false, "Curriculum Vitae", "CV", new Guid("c9f7a5d2-4b3e-48d6-a0f8-7e1c2a59d3b4") },
                    { new Guid("c5d8a4b3-6f2e-41d9-c0e5-6b3c7d52a9e7"), true, "Meses de Experiencia", "MESES_DE_EXPERIENCIA", new Guid("e7f2a3d6-8b4c-49d7-e0f9-4d5c9b74e2c9") },
                    { new Guid("d3a9f7c2-5b4e-41d8-b0f6-8e1c7a52d9f3"), true, "Data de Nascimento", "DATA_DE_NASCIMENTO", new Guid("f8a3b2d7-9c5e-48d7-f1e0-3d6c9b85e4d1") },
                    { new Guid("e2f1c7d5-3b4e-41d8-b0f6-8e1c7a52d9f4"), false, "Contacto telefonico", "CONTACTO_TELEFONICO", new Guid("e7f2a3d6-8b4c-49d7-e0f9-4d5c9b74e2c9") },
                    { new Guid("f3a2b6c4-4d3e-48d6-a0f8-7e1c2a59d3b5"), true, "Morada de Residencia", "MORADA_DE_RESIDENCIA", new Guid("a7d2f9c5-3b4e-41d8-b0f6-8e1c7a52d9f3") }
                });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "EmpresaId", "Nome" },
                values: new object[,]
                {
                    { new Guid("0c7a22c9-5c78-4b7a-9e6b-12c38b7a94be"), new Guid("1aaa8376-c563-4d60-8260-0f376660593e"), "Departamento Comercial" },
                    { new Guid("12cc5d4a-2b83-427b-b6ec-7ca25f3c8311"), new Guid("1aaa8376-c563-4d60-8260-0f376660593e"), "Direção" },
                    { new Guid("2344f768-3897-4014-a703-b218a90b8883"), new Guid("b03e1560-9c6a-4881-9224-362e1775da64"), "Departamento Administrativo" },
                    { new Guid("2c6b82b4-8a44-48e6-b54e-8ec317e0b8fe"), new Guid("b03e1560-9c6a-4881-9224-362e1775da64"), "Direção" },
                    { new Guid("3b05f51c-bdb4-428d-b9a7-0d5fa17e8f83"), new Guid("0758d87d-1a91-48e5-bfbb-ff7ab7b12171"), "Departamento Comercial" },
                    { new Guid("3ba612d1-4bdb-4585-aeb7-4ab5dbac5c41"), new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), "Direção" },
                    { new Guid("49716a7d-60c5-4692-b1b5-815ef290e7b1"), new Guid("0758d87d-1a91-48e5-bfbb-ff7ab7b12171"), "Departamento Administrativo" },
                    { new Guid("5c68f7d0-d756-464f-b601-938d8fd957ba"), new Guid("fb717a87-3f6b-4e5f-97d6-9676945f3c0c"), "Departamento Administrativo" },
                    { new Guid("70de2ea0-d67e-47b1-9094-b2392d8bbdf7"), new Guid("b03e1560-9c6a-4881-9224-362e1775da64"), "Departamento Comercial" },
                    { new Guid("71a47f99-c88a-4a9b-b902-95ec7208b18a"), new Guid("57c4dfc3-afa3-4975-a682-b6f4ceeb9d32"), "Departamento Administrativo" },
                    { new Guid("779b7c9f-f02b-4b7f-bdfd-d7e290438b09"), new Guid("0758d87d-1a91-48e5-bfbb-ff7ab7b12171"), "Direção" },
                    { new Guid("88754e7b-3fd7-4959-98d8-547aa2eb3da3"), new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), "Departamento Comercial" },
                    { new Guid("91a99023-034e-4bfc-9471-ef3493b0a87c"), new Guid("57c4dfc3-afa3-4975-a682-b6f4ceeb9d32"), "Departamento Comercial" },
                    { new Guid("98b3da93-caf0-4bfe-b0f3-6597d98e5458"), new Guid("57c4dfc3-afa3-4975-a682-b6f4ceeb9d32"), "Direção" },
                    { new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), "Departamento Administrativo" },
                    { new Guid("a1a08f28-76f4-4fa0-a445-bf8615b9e5a4"), new Guid("8588bf94-6622-4b1d-8f9f-d1b97ed7dc29"), "Departamento Administrativo" },
                    { new Guid("a7b278ea-e441-4422-bd5e-505f10ab5180"), new Guid("35827598-d25d-4d08-b504-134c2331221d"), "Departamento Administrativo" },
                    { new Guid("ad6d7d65-9d91-4bb1-b201-7d4590790416"), new Guid("fb717a87-3f6b-4e5f-97d6-9676945f3c0c"), "Departamento Comercial" },
                    { new Guid("b3e0131b-34e7-4685-bd8b-d38da0a47467"), new Guid("8588bf94-6622-4b1d-8f9f-d1b97ed7dc29"), "Departamento Comercial" },
                    { new Guid("b6286791-274b-4731-99fe-2f5485a7a97e"), new Guid("5c23d6bf-ecdb-4b98-99bb-20447f5bc13c"), "Departamento Comercial" },
                    { new Guid("bfed76f8-bb0b-4029-b06b-b27c4c00f937"), new Guid("5c23d6bf-ecdb-4b98-99bb-20447f5bc13c"), "Direção" },
                    { new Guid("c0c13e5d-6bb2-406f-a426-99b47d498b8d"), new Guid("1aaa8376-c563-4d60-8260-0f376660593e"), "Departamento Administrativo" },
                    { new Guid("cd290d3b-4292-4c79-95fb-767602b40843"), new Guid("35827598-d25d-4d08-b504-134c2331221d"), "Departamento Comercial" },
                    { new Guid("d11b81a2-e593-45b2-9b1e-d87052a6d51d"), new Guid("8588bf94-6622-4b1d-8f9f-d1b97ed7dc29"), "Direção" },
                    { new Guid("df428847-bff2-4f72-a226-9c8b6fe8cde6"), new Guid("5c23d6bf-ecdb-4b98-99bb-20447f5bc13c"), "Departamento Administrativo" },
                    { new Guid("e5cf4ff5-0a32-41b5-a82f-22639cfd7c55"), new Guid("fb717a87-3f6b-4e5f-97d6-9676945f3c0c"), "Direção" },
                    { new Guid("e6d07c10-9285-4087-b698-7bfb5365635b"), new Guid("35827598-d25d-4d08-b504-134c2331221d"), "Direção" }
                });

            migrationBuilder.InsertData(
                table: "Documentos",
                columns: new[] { "Id", "Caminho", "CamposPreenchidos", "CandidaturaId", "DocumentoAssinado", "TemplateId", "TipoDocumentoId" },
                values: new object[,]
                {
                    { new Guid("0e9a32ec-01cf-4c88-9cf3-3db59a4df057"), "/uploads/documentos/documento1.pdf", null, new Guid("3f71c2d1-66c5-464d-90d5-9da11d02dc2c"), false, null, new Guid("b28f569d-3505-a309-48f6-dc9bf13d4ff1") },
                    { new Guid("1a8e2b19-2278-4565-b530-4e79a0d74a0f"), "/uploads/documentos/documento3.pdf", null, new Guid("e7a9b382-218f-4267-8f19-db9b6f564d92"), false, null, new Guid("9519986b-c52a-73a6-ee45-3a46de2d84ea") },
                    { new Guid("3f71c2d1-66c5-464d-90d5-9da11d02dc2c"), "/uploads/documentos/documento1.pdf", null, new Guid("f5d01a84-7b92-4b2f-acbd-c74999e326f2"), false, null, new Guid("b28f569d-3505-a309-48f6-dc9bf13d4ff1") },
                    { new Guid("4be54f78-3793-4a0b-bf2b-145c37db5db4"), "/uploads/documentos/documento2.pdf", null, new Guid("f5d01a84-7b92-4b2f-acbd-c74999e326f2"), false, null, new Guid("83648738-2354-20f9-841d-f04127fa7980") },
                    { new Guid("84e8d5a0-e7a0-4fa1-a3b4-896517ce3cb1"), "/uploads/documentos/documento2.pdf", null, new Guid("3f71c2d1-66c5-464d-90d5-9da11d02dc2c"), false, null, new Guid("83648738-2354-20f9-841d-f04127fa7980") },
                    { new Guid("9d7a9c13-dff6-4a23-93b9-4f53d3a13a6c"), "/uploads/documentos/documento3.pdf", null, new Guid("3f71c2d1-66c5-464d-90d5-9da11d02dc2c"), false, null, new Guid("9519986b-c52a-73a6-ee45-3a46de2d84ea") },
                    { new Guid("c373cd33-e77e-473c-8c0b-73aeb85955ef"), "/uploads/documentos/documento1.pdf", null, new Guid("c373cd33-e77e-473c-8c0b-73aeb85955ef"), false, null, new Guid("b28f569d-3505-a309-48f6-dc9bf13d4ff1") },
                    { new Guid("dc3f1cf2-870c-4c09-bf9e-75cf18c6c8b7"), "/uploads/documentos/documento1.pdf", null, new Guid("e7a9b382-218f-4267-8f19-db9b6f564d92"), false, null, new Guid("b28f569d-3505-a309-48f6-dc9bf13d4ff1") },
                    { new Guid("dcb4a1d2-cd43-48c2-bbb2-2e6f0be62e63"), "/uploads/documentos/documento2.pdf", null, new Guid("e7a9b382-218f-4267-8f19-db9b6f564d92"), false, null, new Guid("83648738-2354-20f9-841d-f04127fa7980") },
                    { new Guid("e7a9b382-218f-4267-8f19-db9b6f564d92"), "/uploads/documentos/documento3.pdf", null, new Guid("c373cd33-e77e-473c-8c0b-73aeb85955ef"), false, null, new Guid("9519986b-c52a-73a6-ee45-3a46de2d84ea") },
                    { new Guid("f5d01a84-7b92-4b2f-acbd-c74999e326f2"), "/uploads/documentos/documento2.pdf", null, new Guid("c373cd33-e77e-473c-8c0b-73aeb85955ef"), false, null, new Guid("83648738-2354-20f9-841d-f04127fa7980") },
                    { new Guid("f75de97b-5ac1-4f58-91f4-1e1b0bb5d421"), "/uploads/documentos/documento3.pdf", null, new Guid("f5d01a84-7b92-4b2f-acbd-c74999e326f2"), false, null, new Guid("9519986b-c52a-73a6-ee45-3a46de2d84ea") }
                });

            migrationBuilder.InsertData(
                table: "DocumentosNecessarios",
                columns: new[] { "Id", "CargoId", "TipoDocumentoId" },
                values: new object[,]
                {
                    { new Guid("240062af-6a14-4f8a-a641-aa0a68f34b8d"), new Guid("67dbb27d-53d7-4962-ab44-8f7a2c23a301"), new Guid("1dfb7d05-ea51-4bfb-e44b-f690c0d267bb") },
                    { new Guid("495307df-4b60-422a-bf5b-05b6f2519b4b"), new Guid("7f10ab2e-a96c-4bbb-9ebc-c8a2b5811b89"), new Guid("0e5f31a3-24d3-9154-7255-2f49881605ec") },
                    { new Guid("59d1ff72-3a46-4a85-88cd-6f7bcbe4fd19"), new Guid("dc8faf27-b7b3-406e-82c1-ca1f9339a7ea"), new Guid("b28f569d-3505-a309-48f6-dc9bf13d4ff1") },
                    { new Guid("5f10eb2e-26ef-4d4d-813c-8c0ed511f4d4"), new Guid("77552b58-f64b-4f22-8197-cbffd2aa7bfa"), new Guid("0085fcf5-3248-9296-8117-196221fc621b") },
                    { new Guid("76fd2228-5a8d-43bc-bcda-94b0a89693e5"), new Guid("f0d844f3-8c29-4c6d-8042-9473ec8191aa"), new Guid("83648738-2354-20f9-841d-f04127fa7980") },
                    { new Guid("a02f5b5e-e6cb-4e17-8d5c-d4c4d6e3b0a1"), new Guid("ffed9c07-3294-4b32-8c21-2de2bf2dbd32"), new Guid("b28f569d-3505-a309-48f6-dc9bf13d4ff1") },
                    { new Guid("c4d6e2bb-6b8e-4fa0-9249-cd7b9e3fc89c"), new Guid("dc8faf27-b7b3-406e-82c1-ca1f9339a7ea"), new Guid("83648738-2354-20f9-841d-f04127fa7980") },
                    { new Guid("c7a36717-bdc5-4ef1-b837-237c21478420"), new Guid("5251ef65-3843-461e-a0fc-417e27cd6c0f"), new Guid("e5f5b964-d722-6f31-d6dd-6748f8011375") },
                    { new Guid("ccd164bd-9c33-4691-91c8-4296f8bf96f9"), new Guid("66a77ab9-a4d7-4c11-9b2d-928782b3bd95"), new Guid("dd921d99-6365-f210-e2ce-cad7fc07eb54") },
                    { new Guid("d7b4e9c1-3a5f-48f2-a6d0-8c12f7a59e3d"), new Guid("c3d91f85-7e42-4a6b-b2d0-8f1e37ac59b4"), new Guid("b28f569d-3505-a309-48f6-dc9bf13d4ff1") },
                    { new Guid("f2a9c7d4-5b3e-41d8-b0f6-8e1d3a72c9b5"), new Guid("5bbef10e-f9e6-45ad-abcb-bc63bbf13fbe"), new Guid("b28f569d-3505-a309-48f6-dc9bf13d4ff1") },
                    { new Guid("f81cbd56-82f6-4122-9619-c985f5f55c41"), new Guid("5bbef10e-f9e6-45ad-abcb-bc63bbf13fbe"), new Guid("83648738-2354-20f9-841d-f04127fa7980") }
                });

            migrationBuilder.InsertData(
                table: "Estabelecimentos",
                columns: new[] { "Id", "EmpresaId", "Nome" },
                values: new object[] { new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), "Sede" });

            migrationBuilder.InsertData(
                table: "Formularios",
                columns: new[] { "Id", "Nome", "NomeUnico", "TipoId" },
                values: new object[,]
                {
                    { new Guid("52083369-c174-469b-a7ed-844c23bbd948"), "Formulario de Contratação", "FORMULARIO_DE_CONTRATACAO", new Guid("f3a7d9c2-5b4e-41d8-b0f6-8e1c7a52d9f3") },
                    { new Guid("9f8d6c82-6a47-4c13-a2b6-cc7e16f1b3a4"), "Dados Pessoais", "DADOS_PESSOAIS", new Guid("d9c7a5f2-3b4e-48f6-a0d8-7f1e2c4a19b3") }
                });

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "EmpresaId", "Nome" },
                values: new object[,]
                {
                    { new Guid("37e401f4-8e10-481a-b86f-f1e832490022"), new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), "Técnico" },
                    { new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a"), new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), "Comercial" },
                    { new Guid("d75b5cb3-b1e4-4c46-9e7e-4ac763ac6d6e"), new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), "Programador" }
                });

            migrationBuilder.InsertData(
                table: "GestorClientes",
                columns: new[] { "Id", "ApplicationUserId", "EmpresaId" },
                values: new object[] { new Guid("b7f1dc79-3f3d-4d42-a6a4-d27e015f1d73"), "2e0f5a9b-9c94-4e3e-b8b0-8fd60b6ec7ce", new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79") });

            migrationBuilder.InsertData(
                table: "Recrutamentos",
                columns: new[] { "Id", "Data", "EmpresaID", "Nome" },
                values: new object[,]
                {
                    { new Guid("a3c9e742-5fbd-4d7e-9c12-ef8a6b1d34f7"), new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), "Recrutamento Engenheiro de Software Trigenius" },
                    { new Guid("f28d7c91-3bae-45e6-a9f4-7cde812b65a3"), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), "Recrutamento Comercial Trigenius" }
                });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "Caminho", "EmpresaID", "Preenchivel", "TipoDocumentoId" },
                values: new object[,]
                {
                    { new Guid("01832c81-1b3b-a883-a89d-00962a84f13c"), "/uploads/templates/template4.docx", new Guid("57c4dfc3-afa3-4975-a682-b6f4ceeb9d32"), true, new Guid("4ef64bb2-a5e3-aec9-7b0d-9402eb49e31a") },
                    { new Guid("038c531d-d03e-d4cf-1200-64a7fbe1e810"), "/uploads/templates/template4.docx", new Guid("fb717a87-3f6b-4e5f-97d6-9676945f3c0c"), true, new Guid("dd921d99-6365-f210-e2ce-cad7fc07eb54") },
                    { new Guid("14d14eb6-8147-b093-b225-293bf3c5f8f6"), "/uploads/templates/template3.docx", new Guid("35827598-d25d-4d08-b504-134c2331221d"), true, new Guid("83648738-2354-20f9-841d-f04127fa7980") },
                    { new Guid("1b40d243-3954-7af7-b147-18be34a3613e"), "/uploads/templates/template3.docx", new Guid("fb717a87-3f6b-4e5f-97d6-9676945f3c0c"), false, new Guid("729b81db-7211-2292-33fe-d36d245a9ebb") },
                    { new Guid("3d32560e-fd6e-7c05-217b-9d12ac7162b7"), "/uploads/templates/template1.docx", new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), false, new Guid("b28f569d-3505-a309-48f6-dc9bf13d4ff1") },
                    { new Guid("68198395-86fb-a4a9-7cd8-2e2f1a0a54e4"), "/uploads/templates/template1.docx", new Guid("57c4dfc3-afa3-4975-a682-b6f4ceeb9d32"), true, new Guid("e5f5b964-d722-6f31-d6dd-6748f8011375") },
                    { new Guid("73d2ba3b-6f85-e10b-e65b-efe1059d36cd"), "/uploads/templates/template2.docx", new Guid("8588bf94-6622-4b1d-8f9f-d1b97ed7dc29"), true, new Guid("e5f5b964-d722-6f31-d6dd-6748f8011375") },
                    { new Guid("84997a3e-f837-564a-2ed9-edd636b07eb7"), "/uploads/templates/template4.docx", new Guid("1aaa8376-c563-4d60-8260-0f376660593e"), false, new Guid("83648738-2354-20f9-841d-f04127fa7980") },
                    { new Guid("9a40d225-0b23-e48e-74e9-1706c3ce6c73"), "/uploads/templates/template5.docx", new Guid("0758d87d-1a91-48e5-bfbb-ff7ab7b12171"), false, new Guid("e5f5b964-d722-6f31-d6dd-6748f8011375") },
                    { new Guid("cc3d50b8-fc2b-0ca8-d637-4dfac7c389d1"), "/uploads/templates/template4.docx", new Guid("57c4dfc3-afa3-4975-a682-b6f4ceeb9d32"), false, new Guid("1dfb7d05-ea51-4bfb-e44b-f690c0d267bb") },
                    { new Guid("cf7ac3e3-4723-b135-b642-026cda42aa0f"), "/uploads/templates/template1.docx", new Guid("fb717a87-3f6b-4e5f-97d6-9676945f3c0c"), false, new Guid("83648738-2354-20f9-841d-f04127fa7980") },
                    { new Guid("cfaefd3a-ed5d-bd76-f482-681184062cf4"), "/uploads/templates/template1.docx", new Guid("fb717a87-3f6b-4e5f-97d6-9676945f3c0c"), true, new Guid("1dfb7d05-ea51-4bfb-e44b-f690c0d267bb") },
                    { new Guid("d9813c37-5237-4205-4640-3caa17bc1010"), "/uploads/templates/template4.docx", new Guid("b03e1560-9c6a-4881-9224-362e1775da64"), false, new Guid("e5f5b964-d722-6f31-d6dd-6748f8011375") },
                    { new Guid("d9cb8b06-df63-08dd-0f23-65084bd45517"), "/uploads/templates/template1.docx", new Guid("1aaa8376-c563-4d60-8260-0f376660593e"), true, new Guid("0085fcf5-3248-9296-8117-196221fc621b") },
                    { new Guid("ea552d4c-2afa-8614-db5f-b6a5a184fa3b"), "/uploads/templates/template1.docx", new Guid("35827598-d25d-4d08-b504-134c2331221d"), true, new Guid("b28f569d-3505-a309-48f6-dc9bf13d4ff1") }
                });

            migrationBuilder.InsertData(
                table: "Equipas",
                columns: new[] { "Id", "DepartamentoId", "EmpresaId", "Nome" },
                values: new object[,]
                {
                    { new Guid("44b2bdb4-d9ec-404b-90f9-46876d8c0e87"), new Guid("3ba612d1-4bdb-4585-aeb7-4ab5dbac5c41"), new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), "Equipa A" },
                    { new Guid("8b77b08c-b28d-4883-ba8f-776015311465"), new Guid("88754e7b-3fd7-4959-98d8-547aa2eb3da3"), new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), "Equipa A" },
                    { new Guid("d6def626-d3cb-434c-bacd-c19f6b71719a"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), "Equipa A" }
                });

            migrationBuilder.InsertData(
                table: "FormularioCampo",
                columns: new[] { "Id", "Ativo", "CampoId", "FormularioId" },
                values: new object[,]
                {
                    { new Guid("20459860-6dd8-43f6-98bc-73f251d4e421"), true, new Guid("f3a2b6c4-4d3e-48d6-a0f8-7e1c2a59d3b5"), new Guid("9f8d6c82-6a47-4c13-a2b6-cc7e16f1b3a4") },
                    { new Guid("23267585-5c70-4dd1-8a4a-96261de2f57f"), true, new Guid("c5d8a4b3-6f2e-41d9-c0e5-6b3c7d52a9e7"), new Guid("52083369-c174-469b-a7ed-844c23bbd948") },
                    { new Guid("37cb687c-f15b-4a58-8065-2a4e1ed298dc"), true, new Guid("9de6573f-bebd-4277-a43f-02c74737ec9e"), new Guid("9f8d6c82-6a47-4c13-a2b6-cc7e16f1b3a4") },
                    { new Guid("4894eba1-14a5-4b90-b3d8-49ae0412eaec"), true, new Guid("d3a9f7c2-5b4e-41d8-b0f6-8e1c7a52d9f3"), new Guid("9f8d6c82-6a47-4c13-a2b6-cc7e16f1b3a4") },
                    { new Guid("7db5701f-e2cd-49a4-9aaa-ba3baf3b5e39"), true, new Guid("e2f1c7d5-3b4e-41d8-b0f6-8e1c7a52d9f4"), new Guid("9f8d6c82-6a47-4c13-a2b6-cc7e16f1b3a4") },
                    { new Guid("8fe4996d-2391-4926-ba4d-5fa108924e0f"), true, new Guid("2699d419-f8f2-4862-b0e6-add5469c3b48"), new Guid("9f8d6c82-6a47-4c13-a2b6-cc7e16f1b3a4") },
                    { new Guid("baf89140-a4b2-424b-ad69-308c98067072"), true, new Guid("b4c7d2a5-5e3f-49d8-b1f7-9e2c8a62e4d6"), new Guid("52083369-c174-469b-a7ed-844c23bbd948") }
                });

            migrationBuilder.InsertData(
                table: "Vagas",
                columns: new[] { "Id", "CandidaturaDisponivel", "CargoId", "Descricao", "ModalidadeId", "NumVagas", "RecrutamentoId", "TipoId" },
                values: new object[,]
                {
                    { new Guid("b7e4d12f-8c3a-49f6-a5d9-02c8b1e37f4a"), true, new Guid("dc8faf27-b7b3-406e-82c1-ca1f9339a7ea"), "A TRIGENIUS está em busca de um(a) profissional dinâmico(a) e motivado(a) para integrar nossa equipe de vendas. O(a) Representante Comercial será responsável por prospectar novos clientes, apresentar soluções personalizadas e construir relacionamentos de longo prazo que impulsionem o crescimento da empresa.\r\n\r\nPrincipais responsabilidades:\r\n\r\n    Identificar oportunidades de negócio e prospectar novos clientes.\r\n\r\n    Realizar apresentações comerciais e negociações, buscando o fechamento de contratos.\r\n\r\n    Acompanhar o pós-venda para garantir a satisfação dos clientes.\r\n\r\n    Trabalhar em conjunto com o time de marketing para potencializar as estratégias comerciais.\r\n\r\nRequisitos:\r\n\r\n    Experiência prévia em vendas ou atendimento ao cliente.\r\n\r\n    Boa comunicação e habilidade de negociação.\r\n\r\n    Proatividade, organização e foco em resultados.", new Guid("014533bb-205a-4f33-9ce3-2e7fc96dca99"), 5, new Guid("f28d7c91-3bae-45e6-a9f4-7cde812b65a3"), new Guid("d5f1a8c3-79e2-4b0d-b6f7-91c4de38fa25") },
                    { new Guid("f12a9c84-3d7e-45b6-b0f2-8e5d1c73a9d8"), true, new Guid("5bbef10e-f9e6-45ad-abcb-bc63bbf13fbe"), "Estamos em busca de profissionais apaixonados por tecnologia para integrar nossa equipe de desenvolvimento de software na TRIGENIUS. Atuando em projetos desafiadores, você será responsável por projetar, desenvolver e implementar soluções inovadoras, trabalhando em colaboração com times multidisciplinares para atender às demandas de clientes em diversos setores.\r\n\r\nPrincipais responsabilidades:\r\n\r\n    Desenvolver e manter aplicações de alta performance e escalabilidade.\r\n\r\n    Participar ativamente das decisões de arquitetura e design de sistemas.\r\n\r\n    Garantir a qualidade do código, aplicando boas práticas de engenharia de software.\r\n\r\n    Colaborar com equipes de produto e design para entregar soluções que gerem impacto real.\r\n\r\nRequisitos:\r\n\r\n    Experiência prévia com desenvolvimento em C#, .NET ou tecnologias similares.\r\n\r\n    Capacidade de trabalhar de forma colaborativa em ambientes ágeis.\r\n\r\n    Desejável conhecimento em arquitetura de microsserviços e boas práticas de DevOps.", new Guid("d5f20edc-3290-417f-b536-b5e053cdf376"), 12, new Guid("a3c9e742-5fbd-4d7e-9c12-ef8a6b1d34f7"), new Guid("b91e4d76-2c3a-45f8-ae7d-58f2b0d9c184") }
                });

            migrationBuilder.InsertData(
                table: "Candidaturas",
                columns: new[] { "Id", "ContactoCandidato", "EmailCandidato", "EstadoId", "NIFcandidato", "NomeCandidato", "VagaId" },
                values: new object[,]
                {
                    { new Guid("3f71c2d1-66c5-464d-90d5-9da11d02dc2c"), 912345678, "ana.martins@mail.pt", new Guid("4facf236-b3fc-4cab-bdef-5214f674dfec"), 220913315, "Ana Martins", new Guid("b7e4d12f-8c3a-49f6-a5d9-02c8b1e37f4a") },
                    { new Guid("c373cd33-e77e-473c-8c0b-73aeb85955ef"), 910582225, "manuel.silva@mail.pt", new Guid("4facf236-b3fc-4cab-bdef-5214f674dfec"), 255101791, "Manuel Da Silva", new Guid("f12a9c84-3d7e-45b6-b0f2-8e5d1c73a9d8") },
                    { new Guid("e7a9b382-218f-4267-8f19-db9b6f564d92"), 917890123, "filipe.ribeiro@mail.pt", new Guid("4facf236-b3fc-4cab-bdef-5214f674dfec"), 224977270, "Filipe Ribeiro", new Guid("b7e4d12f-8c3a-49f6-a5d9-02c8b1e37f4a") },
                    { new Guid("f5d01a84-7b92-4b2f-acbd-c74999e326f2"), 913456789, "jose.ferreira@mail.pt", new Guid("4facf236-b3fc-4cab-bdef-5214f674dfec"), 219324964, "José Ferreira", new Guid("f12a9c84-3d7e-45b6-b0f2-8e5d1c73a9d8") }
                });

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "Id", "ApplicationUserId", "DepartamentoId", "Email", "EmpresaId", "EquipaId", "EstabelecimentoId", "FuncaoId" },
                values: new object[,]
                {
                    { new Guid("6664832b-40fd-4ca9-80c5-ff50b2df3ec8"), "024e414c-d9d9-477a-bcb5-c6777bc7d3f9", new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), "cazevedo@Empresa_demo_a.pt", new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), new Guid("d6def626-d3cb-434c-bacd-c19f6b71719a"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("d75b5cb3-b1e4-4c46-9e7e-4ac763ac6d6e") },
                    { new Guid("68caf8ab-eecd-45b3-944f-6f4a75b5d226"), "4b93b0ab-aed4-4eb1-a40b-900229359841", new Guid("88754e7b-3fd7-4959-98d8-547aa2eb3da3"), "lneves@Empresa_demo_a.pt", new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), new Guid("8b77b08c-b28d-4883-ba8f-776015311465"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("8050f14d-759c-4b2a-ae2a-bdfee3207ecd"), "db21f4f6-0eb4-4905-a604-ae5ab05696a6", new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), "frebelo@Empresa_demo_a.pt", new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), new Guid("d6def626-d3cb-434c-bacd-c19f6b71719a"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("8d1d8fab-5bb9-4d42-ae14-683692dcbddd"), "620d12b3-1d00-4d7f-a262-795f004f83fe", new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), "ocunha@Empresa_demo_a.pt", new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), new Guid("d6def626-d3cb-434c-bacd-c19f6b71719a"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("9f40195f-5607-49c4-a478-6c942d6ef5a6"), "804eef0f-2ec6-4b19-8b54-3373388f122b", new Guid("3ba612d1-4bdb-4585-aeb7-4ab5dbac5c41"), "malves@Empresa_demo_a.pt", new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), new Guid("44b2bdb4-d9ec-404b-90f9-46876d8c0e87"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("ac42383e-9c00-4660-ace9-e71d942396a0"), "e3a5e7de-975c-4d0f-ad17-cd563e62789a", new Guid("3ba612d1-4bdb-4585-aeb7-4ab5dbac5c41"), "apinto@Empresa_demo_a.pt", new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), new Guid("44b2bdb4-d9ec-404b-90f9-46876d8c0e87"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("c548fb8e-134e-4902-855a-22a20de02ade"), "f1cc0ba3-1050-43f7-86bd-98d4b7f7e216", new Guid("88754e7b-3fd7-4959-98d8-547aa2eb3da3"), "jfonseca@Empresa_demo_a.pt", new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), new Guid("8b77b08c-b28d-4883-ba8f-776015311465"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("c646ceea-1e04-4e33-801b-7a8a29dc8390"), "251e0a52-d589-4845-ada8-daeb08be76ea", new Guid("88754e7b-3fd7-4959-98d8-547aa2eb3da3"), "acasaco@Empresa_demo_a.pt", new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), new Guid("8b77b08c-b28d-4883-ba8f-776015311465"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("d4eb5d92-df45-4416-820a-8edeb679c383"), "faf84edb-dd0b-4de9-90f9-3671335d01c7", new Guid("88754e7b-3fd7-4959-98d8-547aa2eb3da3"), "sconceicao@Empresa_demo_a.pt", new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), new Guid("8b77b08c-b28d-4883-ba8f-776015311465"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("de568654-11aa-4dd0-82eb-c699ea722e5f"), "0eae7ece-6671-4f83-9caa-fc2d8eadd9ae", new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), "fdias@Empresa_demo_a.pt", new Guid("f9c6e3f2-4a41-4b88-8d6a-58ad6a0c1d79"), new Guid("d6def626-d3cb-434c-bacd-c19f6b71719a"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") }
                });

            migrationBuilder.InsertData(
                table: "VagaFormularios",
                columns: new[] { "Id", "FormularioId", "VagaId" },
                values: new object[,]
                {
                    { new Guid("3e1a9c4f-bc98-4a27-9d9b-1c8f8ea3b582"), new Guid("9f8d6c82-6a47-4c13-a2b6-cc7e16f1b3a4"), new Guid("b7e4d12f-8c3a-49f6-a5d9-02c8b1e37f4a") },
                    { new Guid("d5fd6166-b990-4c8e-8747-f033563d1fb1"), new Guid("52083369-c174-469b-a7ed-844c23bbd948"), new Guid("f12a9c84-3d7e-45b6-b0f2-8e5d1c73a9d8") },
                    { new Guid("f8414f9e-1ab6-4262-8331-8343b73a49a7"), new Guid("52083369-c174-469b-a7ed-844c23bbd948"), new Guid("b7e4d12f-8c3a-49f6-a5d9-02c8b1e37f4a") }
                });

            migrationBuilder.InsertData(
                table: "Entrevistas",
                columns: new[] { "Id", "CandidaturaId", "DataHora", "EntrevistadorId", "Notas", "ResultadoEntrevistaId" },
                values: new object[,]
                {
                    { new Guid("5a5b435c-91af-4d85-a36a-dee386a272c8"), new Guid("f5d01a84-7b92-4b2f-acbd-c74999e326f2"), new DateTime(2025, 3, 29, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c646ceea-1e04-4e33-801b-7a8a29dc8390"), "Precisa melhorar na comunicação.", new Guid("3b6a96f0-4b0b-447c-b906-fd7539d00f99") },
                    { new Guid("913eb7f7-3fd4-4e62-889e-5c174ca4eeaf"), new Guid("3f71c2d1-66c5-464d-90d5-9da11d02dc2c"), new DateTime(2025, 4, 1, 15, 20, 0, 0, DateTimeKind.Unspecified), new Guid("de568654-11aa-4dd0-82eb-c699ea722e5f"), "Não atendeu aos requisitos mínimos.", new Guid("387604b5-a3a8-4646-b7e8-eebe825e4b1e") },
                    { new Guid("e6c52afb-7368-469a-a130-6faa8875a533"), new Guid("e7a9b382-218f-4267-8f19-db9b6f564d92"), new DateTime(2025, 3, 30, 9, 45, 0, 0, DateTimeKind.Unspecified), new Guid("6664832b-40fd-4ca9-80c5-ff50b2df3ec8"), "Bom desempenho técnico.", new Guid("6f6d7cc0-f4a7-4b7a-8fe4-890467b579f6") },
                    { new Guid("f7a2c9d4-3b5e-41d8-b0f6-8e1d7a52c9f3"), new Guid("c373cd33-e77e-473c-8c0b-73aeb85955ef"), new DateTime(2025, 8, 20, 14, 30, 0, 0, DateTimeKind.Unspecified), new Guid("ac42383e-9c00-4660-ace9-e71d942396a0"), "", new Guid("6cf2183a-8658-4382-811e-620072e1e3f8") }
                });

            migrationBuilder.InsertData(
                table: "FormularioRespondidos",
                columns: new[] { "Id", "CandidaturaId", "Nome", "Tipo" },
                values: new object[,]
                {
                    { new Guid("14ee9f25-03b1-4460-8bc6-84ae47fa8422"), new Guid("c373cd33-e77e-473c-8c0b-73aeb85955ef"), "Formulario de Contratação", "Contratação" },
                    { new Guid("484157bd-2f6e-400e-93e2-c21018107514"), new Guid("f5d01a84-7b92-4b2f-acbd-c74999e326f2"), "Formulario de Contratação", "Contratação" },
                    { new Guid("7f3c9126-56d4-4cb5-82df-3a27fc8e064f"), new Guid("3f71c2d1-66c5-464d-90d5-9da11d02dc2c"), "Formulario de Contratação", "Contratação" },
                    { new Guid("8fafc343-e3a1-41fa-a70d-dd5f671beeaa"), new Guid("e7a9b382-218f-4267-8f19-db9b6f564d92"), "Formulario de Contratação", "Contratação" },
                    { new Guid("a43c2f48-664d-4a5b-b17f-bb36fdde71c4"), new Guid("e7a9b382-218f-4267-8f19-db9b6f564d92"), "Dados Pessoais", "Dados Pessoais" },
                    { new Guid("e96e91a1-cc6f-4639-b1b1-05e04d5f3b79"), new Guid("3f71c2d1-66c5-464d-90d5-9da11d02dc2c"), "Dados Pessoais", "Dados Pessoais" }
                });

            migrationBuilder.InsertData(
                table: "CampoRespondidos",
                columns: new[] { "Id", "FormularioRespondidoId", "Nome", "Resposta", "Tipo" },
                values: new object[,]
                {
                    { new Guid("0c1492d8-fc0d-446a-b13a-e11e9efdc0e2"), new Guid("a43c2f48-664d-4a5b-b17f-bb36fdde71c4"), "Contacto telefonico", "917890123", "número" },
                    { new Guid("0dfba58e-bf6e-4e36-a13f-8a759a07db3f"), new Guid("7f3c9126-56d4-4cb5-82df-3a27fc8e064f"), "Meses de Experiencia", "24", "int" },
                    { new Guid("2e0c92b3-048e-460c-bc8e-16a0f93f7350"), new Guid("e96e91a1-cc6f-4639-b1b1-05e04d5f3b79"), "Nome Completo", "Ana Maria Antunes Martins", "string" },
                    { new Guid("44fd408d-5ff3-4c4e-bdd1-c5d415592533"), new Guid("484157bd-2f6e-400e-93e2-c21018107514"), "Curriculum Vitae", "Anexo enviado!", "anexo" },
                    { new Guid("45e2e56b-9b17-49c7-8472-11fc70b29591"), new Guid("e96e91a1-cc6f-4639-b1b1-05e04d5f3b79"), "Data de Nascimento", "10-03-1998", "data" },
                    { new Guid("5761aad9-1794-4bb4-8094-49b46dc4d028"), new Guid("484157bd-2f6e-400e-93e2-c21018107514"), "Meses de Experiencia", "24", "int" },
                    { new Guid("6c582d39-98ec-43d4-bd06-061674261f4d"), new Guid("e96e91a1-cc6f-4639-b1b1-05e04d5f3b79"), "Contacto telefonico", "912345678", "número" },
                    { new Guid("7092f6a9-678a-4e4f-8fba-2934849c40d9"), new Guid("a43c2f48-664d-4a5b-b17f-bb36fdde71c4"), "Correio Eletronico", "filipe.ribeiro@mail.pt", "string" },
                    { new Guid("764da4b2-4463-44e5-a076-4c74a7e81967"), new Guid("a43c2f48-664d-4a5b-b17f-bb36fdde71c4"), "Nome Completo", "Filipe Alberto Antunes Ribeiro", "string" },
                    { new Guid("96788bfe-c6eb-4770-bfe2-843a1b6f5617"), new Guid("a43c2f48-664d-4a5b-b17f-bb36fdde71c4"), "Morada de Residencia", "Rua de Leiria, nº2, 2440-400, Leiria", "string" },
                    { new Guid("a0923d06-5f13-4573-84f0-e3e280536036"), new Guid("8fafc343-e3a1-41fa-a70d-dd5f671beeaa"), "Meses de Experiencia", "48", "int" },
                    { new Guid("a1fcd194-81e7-40e5-97f4-9859d899cbe4"), new Guid("7f3c9126-56d4-4cb5-82df-3a27fc8e064f"), "Curriculum Vitae", "Anexo enviado!", "anexo" },
                    { new Guid("a3b0aab6-1c63-44be-84aa-8be4f04f8721"), new Guid("8fafc343-e3a1-41fa-a70d-dd5f671beeaa"), "Curriculum Vitae", "Anexo enviado!", "anexo" },
                    { new Guid("af366e8c-3f71-4b3e-8316-e3bbcf4fe8e8"), new Guid("a43c2f48-664d-4a5b-b17f-bb36fdde71c4"), "Data de Nascimento", "02-02-2000", "data" },
                    { new Guid("bf7d4145-7d93-4ec9-bd20-6b182a943228"), new Guid("e96e91a1-cc6f-4639-b1b1-05e04d5f3b79"), "Morada de Residencia", "Rua de Leiria, nº3, 2440-400, Leiria", "string" },
                    { new Guid("e44c70e7-51d3-4c90-a80f-56f2cfcf24e0"), new Guid("e96e91a1-cc6f-4639-b1b1-05e04d5f3b79"), "Correio Eletronico", "ana.martins@mail.pt", "string" },
                    { new Guid("e6fd314e-094b-4985-89a2-a4353fb4367f"), new Guid("14ee9f25-03b1-4460-8bc6-84ae47fa8422"), "Curriculum Vitae", "Anexo enviado!", "anexo" },
                    { new Guid("fbe325e0-1ea4-4353-b435-b603e11c166f"), new Guid("14ee9f25-03b1-4460-8bc6-84ae47fa8422"), "Meses de Experiencia", "92", "int" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampoRespondidos_FormularioRespondidoId",
                table: "CampoRespondidos",
                column: "FormularioRespondidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Campos_NomeUnico",
                table: "Campos",
                column: "NomeUnico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campos_TipoId",
                table: "Campos",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_EstadoId",
                table: "Candidaturas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_VagaId",
                table: "Candidaturas",
                column: "VagaId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_EmpresaId",
                table: "Departamentos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_TemplateId",
                table: "Documentos",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_TipoDocumentoId",
                table: "Documentos",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosNecessarios_CargoId",
                table: "DocumentosNecessarios",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosNecessarios_TipoDocumentoId",
                table: "DocumentosNecessarios",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaTipoDocumentos_TipoDocumentoId",
                table: "EmpresaTipoDocumentos",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrevistas_CandidaturaId",
                table: "Entrevistas",
                column: "CandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrevistas_EntrevistadorId",
                table: "Entrevistas",
                column: "EntrevistadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrevistas_ResultadoEntrevistaId",
                table: "Entrevistas",
                column: "ResultadoEntrevistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipas_DepartamentoId",
                table: "Equipas",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipas_EmpresaId",
                table: "Equipas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Estabelecimentos_EmpresaId",
                table: "Estabelecimentos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_FormularioCampo_CampoId",
                table: "FormularioCampo",
                column: "CampoId");

            migrationBuilder.CreateIndex(
                name: "IX_FormularioCampo_FormularioId",
                table: "FormularioCampo",
                column: "FormularioId");

            migrationBuilder.CreateIndex(
                name: "IX_FormularioRespondidos_CandidaturaId",
                table: "FormularioRespondidos",
                column: "CandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Formularios_NomeUnico",
                table: "Formularios",
                column: "NomeUnico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Formularios_TipoId",
                table: "Formularios",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_ApplicationUserId",
                table: "Funcionarios",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_DepartamentoId",
                table: "Funcionarios",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_EmpresaId",
                table: "Funcionarios",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_EquipaId",
                table: "Funcionarios",
                column: "EquipaId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_EstabelecimentoId",
                table: "Funcionarios",
                column: "EstabelecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_FuncaoId",
                table: "Funcionarios",
                column: "FuncaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcoes_EmpresaId",
                table: "Funcoes",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_GestorClientes_ApplicationUserId",
                table: "GestorClientes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GestorClientes_EmpresaId",
                table: "GestorClientes",
                column: "EmpresaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpcoesCampoLookups_CampoId",
                table: "OpcoesCampoLookups",
                column: "CampoId");

            migrationBuilder.CreateIndex(
                name: "IX_Recrutamentos_EmpresaID",
                table: "Recrutamentos",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_EmpresaID",
                table: "Templates",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_TipoDocumentoId",
                table: "Templates",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_VagaFormularios_FormularioId",
                table: "VagaFormularios",
                column: "FormularioId");

            migrationBuilder.CreateIndex(
                name: "IX_VagaFormularios_VagaId",
                table: "VagaFormularios",
                column: "VagaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_CargoId",
                table: "Vagas",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_ModalidadeId",
                table: "Vagas",
                column: "ModalidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_RecrutamentoId",
                table: "Vagas",
                column: "RecrutamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_TipoId",
                table: "Vagas",
                column: "TipoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampoRespondidos");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "DocumentosNecessarios");

            migrationBuilder.DropTable(
                name: "EmpresaTipoDocumentos");

            migrationBuilder.DropTable(
                name: "Entrevistas");

            migrationBuilder.DropTable(
                name: "FormularioCampo");

            migrationBuilder.DropTable(
                name: "GestorClientes");

            migrationBuilder.DropTable(
                name: "OpcoesCampoLookups");

            migrationBuilder.DropTable(
                name: "VagaFormularios");

            migrationBuilder.DropTable(
                name: "FormularioRespondidos");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Campos");

            migrationBuilder.DropTable(
                name: "Formularios");

            migrationBuilder.DropTable(
                name: "Candidaturas");

            migrationBuilder.DropTable(
                name: "TipoDocumentos");

            migrationBuilder.DropTable(
                name: "Equipas");

            migrationBuilder.DropTable(
                name: "Estabelecimentos");

            migrationBuilder.DropTable(
                name: "Funcoes");

            migrationBuilder.DropTable(
                name: "Vagas");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Lookups");

            migrationBuilder.DropTable(
                name: "Recrutamentos");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a3e57c14-2b8f-4e9f-9d4a-7a2b172b0e48", "2e0f5a9b-9c94-4e3e-b8b0-8fd60b6ec7ce" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3e57c14-2b8f-4e9f-9d4a-7a2b172b0e48");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2e0f5a9b-9c94-4e3e-b8b0-8fd60b6ec7ce");
        }
    }
}
