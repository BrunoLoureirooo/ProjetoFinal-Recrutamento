using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PRH.Server.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarDadosBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                        name: "FK_Funcionarios_Departamentos_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
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

            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { new Guid("732ef83d-1dd0-42c8-a0df-5348b7c4d2ae"), "DEMO B" },
                    { new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), "DEMO A" }
                });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "EmpresaId", "Nome" },
                values: new object[,]
                {
                    { new Guid("104721f3-a3ec-4006-8906-f0204a2fedf2"), new Guid("732ef83d-1dd0-42c8-a0df-5348b7c4d2ae"), "Departamento Comercial" },
                    { new Guid("3b41ce3b-be6e-4e44-8c3e-87f6de12d818"), new Guid("732ef83d-1dd0-42c8-a0df-5348b7c4d2ae"), "Departamento Administrativo" },
                    { new Guid("3ba612d1-4bdb-4585-aeb7-4ab5dbac5c41"), new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), "Direção" },
                    { new Guid("88754e7b-3fd7-4959-98d8-547aa2eb3da3"), new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), "Departamento Comercial" },
                    { new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), "Departamento Administrativo" },
                    { new Guid("e3b46221-33cd-4277-82a7-f1756cfe570c"), new Guid("732ef83d-1dd0-42c8-a0df-5348b7c4d2ae"), "Direção" }
                });

            migrationBuilder.InsertData(
                table: "Estabelecimentos",
                columns: new[] { "Id", "EmpresaId", "Nome" },
                values: new object[,]
                {
                    { new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), "Sede" },
                    { new Guid("a9340a8f-31e2-474e-9372-d564996a33ab"), new Guid("732ef83d-1dd0-42c8-a0df-5348b7c4d2ae"), "Sede" }
                });

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "EmpresaId", "Nome" },
                values: new object[,]
                {
                    { new Guid("37e401f4-8e10-481a-b86f-f1e832490022"), new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), "Técnico" },
                    { new Guid("449ac1d8-71df-42df-9598-eb171d2c436e"), new Guid("732ef83d-1dd0-42c8-a0df-5348b7c4d2ae"), "Técnico" },
                    { new Guid("74b62f70-b001-43e7-8202-e81e7ac48d67"), new Guid("732ef83d-1dd0-42c8-a0df-5348b7c4d2ae"), "Comercial" },
                    { new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a"), new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), "Comercial" },
                    { new Guid("d75b5cb3-b1e4-4c46-9e7e-4ac763ac6d6e"), new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), "Programador" },
                    { new Guid("f8013611-d94d-4684-ba5f-7b887e9e4674"), new Guid("732ef83d-1dd0-42c8-a0df-5348b7c4d2ae"), "Programador" }
                });

            migrationBuilder.InsertData(
                table: "Equipas",
                columns: new[] { "Id", "DepartamentoId", "EmpresaId", "Nome" },
                values: new object[,]
                {
                    { new Guid("44b2bdb4-d9ec-404b-90f9-46876d8c0e87"), new Guid("3ba612d1-4bdb-4585-aeb7-4ab5dbac5c41"), new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), "Equipa C" },
                    { new Guid("5bd7206e-c5b0-4313-9156-bc76805f4773"), new Guid("3b41ce3b-be6e-4e44-8c3e-87f6de12d818"), new Guid("732ef83d-1dd0-42c8-a0df-5348b7c4d2ae"), "Equipa A" },
                    { new Guid("8b77b08c-b28d-4883-ba8f-776015311465"), new Guid("88754e7b-3fd7-4959-98d8-547aa2eb3da3"), new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), "Equipa B" },
                    { new Guid("a31cb43a-41c4-45ef-b437-bd9951dd8dfd"), new Guid("e3b46221-33cd-4277-82a7-f1756cfe570c"), new Guid("732ef83d-1dd0-42c8-a0df-5348b7c4d2ae"), "Direção" },
                    { new Guid("d2fe96ee-8128-43e4-8625-c0a1decd963c"), new Guid("104721f3-a3ec-4006-8906-f0204a2fedf2"), new Guid("732ef83d-1dd0-42c8-a0df-5348b7c4d2ae"), "Departamento Comercial" },
                    { new Guid("d6def626-d3cb-434c-bacd-c19f6b71719a"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("732ef83d-1dd0-42c8-a0df-5348b7c4d2ae"), "Equipa A" }
                });

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "Id", "ApplicationUserId", "DepartamentoId", "Email", "EmpresaId", "EquipaId", "EstabelecimentoId", "FuncaoId" },
                values: new object[,]
                {
                    { new Guid("6664832b-40fd-4ca9-80c5-ff50b2df3ec8"), "024e414c-d9d9-477a-bcb5-c6777bc7d3f9", new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), "cazevedo@empresa_demo_a.pt", new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), new Guid("d6def626-d3cb-434c-bacd-c19f6b71719a"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("d75b5cb3-b1e4-4c46-9e7e-4ac763ac6d6e") },
                    { new Guid("68caf8ab-eecd-45b3-944f-6f4a75b5d226"), "4b93b0ab-aed4-4eb1-a40b-900229359841", new Guid("88754e7b-3fd7-4959-98d8-547aa2eb3da3"), "lneves@empresa_demo_a.pt", new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), new Guid("8b77b08c-b28d-4883-ba8f-776015311465"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("8050f14d-759c-4b2a-ae2a-bdfee3207ecd"), "db21f4f6-0eb4-4905-a604-ae5ab05696a6", new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), "frebelo@empresa_demo_a.pt", new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), new Guid("d6def626-d3cb-434c-bacd-c19f6b71719a"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("8d1d8fab-5bb9-4d42-ae14-683692dcbddd"), "620d12b3-1d00-4d7f-a262-795f004f83fe", new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), "ocunha@empresa_demo_a.pt", new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), new Guid("d6def626-d3cb-434c-bacd-c19f6b71719a"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("9f40195f-5607-49c4-a478-6c942d6ef5a6"), "804eef0f-2ec6-4b19-8b54-3373388f122b", new Guid("3ba612d1-4bdb-4585-aeb7-4ab5dbac5c41"), "malves@empresa_demo_a.pt", new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), new Guid("44b2bdb4-d9ec-404b-90f9-46876d8c0e87"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("ac42383e-9c00-4660-ace9-e71d942396a0"), "e3a5e7de-975c-4d0f-ad17-cd563e62789a", new Guid("3ba612d1-4bdb-4585-aeb7-4ab5dbac5c41"), "apinto@empresa_demo_a.pt", new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), new Guid("44b2bdb4-d9ec-404b-90f9-46876d8c0e87"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("c548fb8e-134e-4902-855a-22a20de02ade"), "f1cc0ba3-1050-43f7-86bd-98d4b7f7e216", new Guid("88754e7b-3fd7-4959-98d8-547aa2eb3da3"), "jfonseca@empresa_demo_a.pt", new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), new Guid("8b77b08c-b28d-4883-ba8f-776015311465"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("c646ceea-1e04-4e33-801b-7a8a29dc8390"), "251e0a52-d589-4845-ada8-daeb08be76ea", new Guid("88754e7b-3fd7-4959-98d8-547aa2eb3da3"), "acasaco@empresa_demo_a.pt", new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), new Guid("8b77b08c-b28d-4883-ba8f-776015311465"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("d4eb5d92-df45-4416-820a-8edeb679c383"), "faf84edb-dd0b-4de9-90f9-3671335d01c7", new Guid("88754e7b-3fd7-4959-98d8-547aa2eb3da3"), "sconceicao@empresa_demo_a.pt", new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), new Guid("8b77b08c-b28d-4883-ba8f-776015311465"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") },
                    { new Guid("de568654-11aa-4dd0-82eb-c699ea722e5f"), "0eae7ece-6671-4f83-9caa-fc2d8eadd9ae", new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), "fdias@empresa_demo_a.pt", new Guid("bd00f150-16ea-48f1-b85e-cda3af329dac"), new Guid("d6def626-d3cb-434c-bacd-c19f6b71719a"), new Guid("9fc5fb53-6945-4587-98bc-ef4bb4926c5e"), new Guid("97c6a146-61de-4d8b-a343-b54d81b0432a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_EmpresaId",
                table: "Departamentos",
                column: "EmpresaId");

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
                name: "IX_Funcionarios_ApplicationUserId",
                table: "Funcionarios",
                column: "ApplicationUserId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Equipas");

            migrationBuilder.DropTable(
                name: "Estabelecimentos");

            migrationBuilder.DropTable(
                name: "Funcoes");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
