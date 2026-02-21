using Bunit;
using Moq;
using Radzen;
using Microsoft.Extensions.DependencyInjection;
using PRH.Client.Wasm.Services.Contracts;
using PRH.Client.Wasm.Pages.Formulario;
using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Shared.DataTransferObjets.RequestFeatures;

public class FormularioListAllTest : TestContext
{
    [Fact]
    public void Component_DisplaysListOfFormularios_WhenDataIsAvailable()
    {
        //Registar servicos
        Services.AddScoped<DialogService>();
        Services.AddScoped<NotificationService>();
        Services.AddScoped<TooltipService>();
        Services.AddScoped<ContextMenuService>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        //Mockup dos servicos
        var mockFormularioService = new Mock<IFormularioService>();
        var mockCampoService = new Mock<ICampoService>();
        var mockLookupService = new Mock<ILookupService>();
        var mockFormularioCampoService = new Mock<IFormularioCampoService>(); // New mock

        var mockData = new List<FormularioGetAllDto>
        {
            new() { Nome = "Formulário A" },
            new() { Nome = "Formulário B" }
        };

        var pagedResult = new PagedListDto<FormularioGetAllDto>
        {
            Items = mockData,
            MetaData = new MetaDataDto
            {
                TotalCount = mockData.Count,
                PageSize = 10,
                CurrentPage = 1,
                TotalPages = 1
            }
        };

        //Configuracao de respostas de servico
        mockFormularioService.Setup(s => s.GetFormulariosPaginatedAsync(
            It.IsAny<int>(),
            It.IsAny<int>()
        )).ReturnsAsync(pagedResult);

        //Registo de Servicos
        Services.AddSingleton(mockFormularioService.Object);
        Services.AddSingleton(mockCampoService.Object);
        Services.AddSingleton(mockLookupService.Object);
        Services.AddSingleton(mockFormularioCampoService.Object); // New registration

        //Renderizacao do componente -> teste
        var cut = RenderComponent<ListAll>();

        //Confirmacao de resultados
        cut.Markup.Contains("Formulário - Formulário A");
        cut.Markup.Contains("Formulário - Formulário B");
    }
}