using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PRH.Server.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarDadosUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "418b0ac3-4dba-4ccf-883e-63fdd7ac62a9", null, "Administrador", "ADMINISTRADOR" },
                    { "52c71224-fe99-4ec0-9130-0f0de200eec9", null, "Funcionario", "FUNCIONARIO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "024e414c-d9d9-477a-bcb5-c6777bc7d3f9", 0, "5701278b-d7ec-40ba-9cd6-708c44563c16", "cazevedo@pessoal.pt", true, false, null, "Carla Alexandra Gomes Azevedo", "CAZEVEDO@PESSOAL.PT", null, "AQAAAAIAAYagAAAAEA/Jq/pyjdaYZPHHJpHbqp+i+5+OsgUefuEAQkvooVZb58I6rxZyxF8urU87vWNi3w==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "75da7095-038e-4aca-970a-23122ef0e9c7", false, null },
                    { "0eae7ece-6671-4f83-9caa-fc2d8eadd9ae", 0, "e5607436-cab6-4d49-bf0a-2a3f595fd1f7", "fdias@pessoal.pt", true, false, null, "Filipe Manuel Marques da Silva Dias", "FDIAS@PESSOAL.PT", null, "AQAAAAIAAYagAAAAEPndwjT6ExopmCYz9Aq9PrmtPPkq9lUQdddp76wbeRyhTVzEbGN+74hauKGZUWrVng==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bf601b92-1997-477c-a5b3-8958adc58474", false, null },
                    { "251e0a52-d589-4845-ada8-daeb08be76ea", 0, "1635a002-94d3-44ef-86bb-57748d6b5654", "acasaco@pessoal.pt", true, false, null, "Ana Cristina Leite Casaco", "ACASACO@PESSOAL.PT", null, "AQAAAAIAAYagAAAAEIDVzMG1GxuV1nz0VAKeh2s5+6SNWxHgrMQVsfltRGRgB30T5Svv8i96lxbNy6O5ew==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9dce8fcf-555e-475b-a676-0336b4a2886e", false, null },
                    { "4b93b0ab-aed4-4eb1-a40b-900229359841", 0, "c02f62ed-c493-4c6b-9925-7ed27623346f", "lneves@pessoal.pt", true, false, null, "Luís Augusto Pinto Teixeira Neves", "LNEVES@PESSOAL.PT", null, "AQAAAAIAAYagAAAAEG9Qpe1DBFgyk9AfdJGGl6tzKtSDzBGv47Q6lSlf4vzR06PNT7XDSBirnaOeXgpq8w==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "fb09937d-40b0-4999-ae4b-8c551449e296", false, null },
                    { "620d12b3-1d00-4d7f-a262-795f004f83fe", 0, "9a7c70d8-ebed-4706-bb62-5d80432d9f83", "OCUNHA@PESSOAL.PT", true, false, null, "Olga Maria Casal Melo da Cunha", "ocunha@pessoal.pt", null, "AQAAAAIAAYagAAAAEIF2ty76GVZXBhL7Qm24f0guFjidd1vxOxXEXyFZH08hSenVZc1w3ph7suumFnvVZA==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2b90ef1a-830b-4f31-9c62-df18c07961e8", false, null },
                    { "804eef0f-2ec6-4b19-8b54-3373388f122b", 0, "57635711-b231-4e09-866c-cb8970cab382", "malves@pessoal.pt", true, false, null, "Maria João Pereira Alves", "MALVES@PESSOAL.PT", null, "AQAAAAIAAYagAAAAEDOqoY/A9Id4uSg0FZ2Pn1IFcP59oG/uARhSavAgtFEgJ7ZcvoQkQVjjCvzc+BFQnQ==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "214c00b2-682f-4310-84ba-8cd7f466f20d", false, null },
                    { "db21f4f6-0eb4-4905-a604-ae5ab05696a6", 0, "85ce73c8-afd1-4679-bd8b-8cf37c891da0", "frebelo@pessoal.pt", true, false, null, "Filomena Maria Cruz Rebelo", "FREBELO@PESSOAL.PT", null, "AQAAAAIAAYagAAAAEBDhOkl+BmTH6/kLA+Lwjzg9lgE3khGaNJ13UhPR2EM5dQLs/k7mjif9+3KvB7i/wQ==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "6db4e253-246d-4ed7-b833-e0d896805c07", false, null },
                    { "e3a5e7de-975c-4d0f-ad17-cd563e62789a", 0, "e0981140-1b7c-4f33-a556-8ce519876d66", "apinto@pessoal.pt", true, false, null, "Ana Alexandra Valença Pinto", "APINTO@PESSOAL.PT", null, "AQAAAAIAAYagAAAAENLLmW3rVmFlf8TaNCL5neMQ2dlb4Tqdmk8wmyech/l1LR0NrhCyYPNg5GVPixZE9Q==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "82a360c3-37f9-42da-9dd2-e8af93b864fc", false, null },
                    { "f1cc0ba3-1050-43f7-86bd-98d4b7f7e216", 0, "5e9c546c-5c8b-446b-834d-89d3fb270ac9", "jfonseca@pessoal.pt", true, false, null, "João Carlos Araújo Fonseca", "JFONSECA@PESSOAL.PT", null, "AQAAAAIAAYagAAAAENmFh/knB5U+tb/bOSLPIHkBqqkgHUv0msS8BrGdstihIWLsJom+6pAK5ijyjJ2yFA==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "c4225b9c-3e5c-4526-a267-b55fc1dbdde8", false, null },
                    { "faf84edb-dd0b-4de9-90f9-3671335d01c7", 0, "e4e88c59-a13a-44b2-bedf-11fcb65c5d2b", "sconceicao@pessoal.pt", true, false, null, "Sandra Regina Queirós Azevedo Conceição", "SCONCEICAO@PESSOAL.PT", null, "AQAAAAIAAYagAAAAEBunlXGl4cP+eVgXp5N8WTrsPdSoFKudcKMZg47f2Ql3eJVJvOXqaYWopgeERwKVMw==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1d4e98cc-c78e-4abe-8164-aa41358c89b0", false, null },
                    { "fb874eba-9c48-4340-a690-bebb2195d379", 0, "be7e12e8-b4d4-4136-8be0-a022c5ae1b77", "admin@admin.pt", true, false, null, "Admin", "ADMIN@ADMIN.PT", null, "AQAAAAIAAYagAAAAEKCqOA4Ilzwt3W3BSnRTmiTH4ahIz+YHJYBKCC8o/a/Mj4G0InEWw244yJamcd8tCA==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "8f15dade-0a81-4238-ae5c-cad2ca8d0aae", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "52c71224-fe99-4ec0-9130-0f0de200eec9", "024e414c-d9d9-477a-bcb5-c6777bc7d3f9" },
                    { "52c71224-fe99-4ec0-9130-0f0de200eec9", "0eae7ece-6671-4f83-9caa-fc2d8eadd9ae" },
                    { "52c71224-fe99-4ec0-9130-0f0de200eec9", "251e0a52-d589-4845-ada8-daeb08be76ea" },
                    { "52c71224-fe99-4ec0-9130-0f0de200eec9", "4b93b0ab-aed4-4eb1-a40b-900229359841" },
                    { "52c71224-fe99-4ec0-9130-0f0de200eec9", "620d12b3-1d00-4d7f-a262-795f004f83fe" },
                    { "52c71224-fe99-4ec0-9130-0f0de200eec9", "804eef0f-2ec6-4b19-8b54-3373388f122b" },
                    { "52c71224-fe99-4ec0-9130-0f0de200eec9", "db21f4f6-0eb4-4905-a604-ae5ab05696a6" },
                    { "52c71224-fe99-4ec0-9130-0f0de200eec9", "e3a5e7de-975c-4d0f-ad17-cd563e62789a" },
                    { "52c71224-fe99-4ec0-9130-0f0de200eec9", "f1cc0ba3-1050-43f7-86bd-98d4b7f7e216" },
                    { "52c71224-fe99-4ec0-9130-0f0de200eec9", "faf84edb-dd0b-4de9-90f9-3671335d01c7" },
                    { "418b0ac3-4dba-4ccf-883e-63fdd7ac62a9", "fb874eba-9c48-4340-a690-bebb2195d379" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52c71224-fe99-4ec0-9130-0f0de200eec9", "024e414c-d9d9-477a-bcb5-c6777bc7d3f9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52c71224-fe99-4ec0-9130-0f0de200eec9", "0eae7ece-6671-4f83-9caa-fc2d8eadd9ae" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52c71224-fe99-4ec0-9130-0f0de200eec9", "251e0a52-d589-4845-ada8-daeb08be76ea" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52c71224-fe99-4ec0-9130-0f0de200eec9", "4b93b0ab-aed4-4eb1-a40b-900229359841" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52c71224-fe99-4ec0-9130-0f0de200eec9", "620d12b3-1d00-4d7f-a262-795f004f83fe" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52c71224-fe99-4ec0-9130-0f0de200eec9", "804eef0f-2ec6-4b19-8b54-3373388f122b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52c71224-fe99-4ec0-9130-0f0de200eec9", "db21f4f6-0eb4-4905-a604-ae5ab05696a6" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52c71224-fe99-4ec0-9130-0f0de200eec9", "e3a5e7de-975c-4d0f-ad17-cd563e62789a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52c71224-fe99-4ec0-9130-0f0de200eec9", "f1cc0ba3-1050-43f7-86bd-98d4b7f7e216" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52c71224-fe99-4ec0-9130-0f0de200eec9", "faf84edb-dd0b-4de9-90f9-3671335d01c7" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "418b0ac3-4dba-4ccf-883e-63fdd7ac62a9", "fb874eba-9c48-4340-a690-bebb2195d379" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "418b0ac3-4dba-4ccf-883e-63fdd7ac62a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52c71224-fe99-4ec0-9130-0f0de200eec9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "024e414c-d9d9-477a-bcb5-c6777bc7d3f9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0eae7ece-6671-4f83-9caa-fc2d8eadd9ae");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "251e0a52-d589-4845-ada8-daeb08be76ea");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4b93b0ab-aed4-4eb1-a40b-900229359841");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "620d12b3-1d00-4d7f-a262-795f004f83fe");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "804eef0f-2ec6-4b19-8b54-3373388f122b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "db21f4f6-0eb4-4905-a604-ae5ab05696a6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e3a5e7de-975c-4d0f-ad17-cd563e62789a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f1cc0ba3-1050-43f7-86bd-98d4b7f7e216");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "faf84edb-dd0b-4de9-90f9-3671335d01c7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fb874eba-9c48-4340-a690-bebb2195d379");
        }
    }
}
