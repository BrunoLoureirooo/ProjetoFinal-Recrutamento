using Bunit;
using Moq;
using Radzen;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PRH.Client.Wasm.Services.Contracts;
using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Shared.DataTransferObjets.Campo;
using PRH.Client.Wasm.Pages.Formulario;
using Radzen.Blazor;
using PRH.Shared.DataTransferObjets;

public class FormularioCreateTest : TestContext
{
    [Fact]
    public void Can_Create_Formulario_With_Campos()
    {
        // Arrange
        Services.AddScoped<DialogService>();
        Services.AddScoped<NotificationService>();
        Services.AddScoped<TooltipService>();
        Services.AddScoped<ContextMenuService>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var tipoFormulario = new LookupDto
        {
            Id = Guid.Parse("f3a7d9c2-5b4e-41d8-b0f6-8e1c7a52d9f3"),
            Descricao = "Tipo A"
        };

        var mockCampo = new CampoGetDto
        {
            Id = Guid.NewGuid(),
            Nome = "Campo Teste",
            TipoCampo = new LookupDto
            {
                Id = Guid.Parse("a7d2f9c5-3b4e-41d8-b0f6-8e1c7a52d9f3"),
                Descricao = "Texto"
            }
        };

        var mockFormularioService = new Mock<IFormularioService>();
        var mockCampoService = new Mock<ICampoService>();
        var mockLookupService = new Mock<ILookupService>();

        mockLookupService.Setup(s => s.GetTiposFormularioAsync())
            .ReturnsAsync(new List<LookupDto> { tipoFormulario });

        mockCampoService.Setup(s => s.GetCamposAsync())
            .ReturnsAsync(new List<CampoGetDto> { mockCampo });

        FormularioCreateDto createdFormulario = null;
        mockFormularioService.Setup(s => s.CreateFormularioAsync(It.IsAny<FormularioCreateDto>()))
            .Callback<FormularioCreateDto>(dto => createdFormulario = dto)
            .ReturnsAsync(new FormularioGetOneDto());

        Services.AddSingleton(mockFormularioService.Object);
        Services.AddSingleton(mockCampoService.Object);
        Services.AddSingleton(mockLookupService.Object);

        // Act
        var cut = RenderComponent<Add>();

        // Wait for critical data load
        cut.WaitForState(() =>
            cut.FindComponents<RadzenAutoComplete>().Any() &&
            cut.FindComponent<RadzenAutoComplete>().Instance.Data != null,
            timeout: TimeSpan.FromSeconds(10)
        );

        // Fill name
        var nomeInput = cut.Find("input[aria-label='Nome']");
        nomeInput.Change("Novo Formulario");
        cut.Render();

        // Select tipo formulario
        var tipoDropdown = cut.FindComponent<RadzenDropDown<string>>();
        tipoDropdown.Instance.Value = tipoFormulario.Descricao;
        cut.Render();

        // Select campo using actual autocomplete interaction
        var autoComplete = cut.FindComponent<RadzenAutoComplete>();
        autoComplete.SetParametersAndRender(p =>
            p.Add(ac => ac.SelectedItem, mockCampo)
        );

        // Find and click add button using component reference
        var addButton = cut.FindComponents<RadzenButton>()
            .First(b => b.Instance.Icon == "add");
        addButton.Find("button").Click();

        // Verify campo card using component reference
        cut.WaitForState(() =>
            cut.FindComponents<RadzenCard>().Count == 1,
            timeout: TimeSpan.FromSeconds(10)
        );

        // Submit form
        var submitButton = cut.Find("button:contains('Salvar formulario')");
        submitButton.Click();

        // Assert service call
        cut.WaitForAssertion(() =>
            mockFormularioService.Verify(
                s => s.CreateFormularioAsync(It.Is<FormularioCreateDto>(dto =>
                    dto.Nome == "Novo Formulario" &&
                    dto.FormularioCampos.Count == 1
                )),
                Times.Once
            ),
            timeout: TimeSpan.FromSeconds(5)
        );
    }
}