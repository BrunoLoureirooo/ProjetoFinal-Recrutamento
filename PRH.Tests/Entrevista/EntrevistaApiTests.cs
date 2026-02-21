using PRH.Shared.DataTransferObjets;
using static PRH.Client.Wasm.Services.AuthService;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PRH.Shared.DataTransferObjets.Recrutamento;
using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Shared.DataTransferObjets.Vaga;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.Entrevista;
using PRH.Shared.DataTransferObjets.Candidatura;
using PRH.Shared.DataTransferObjets.Funcionario;

public class EntrevistaApiTests
{
    private HttpClient _httpClient;

    public EntrevistaApiTests()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
    }

    [Fact]
    public async Task GetEntrevistas_Admin()
    {
        await LoginAs_Admin();

        // test get all
        var allEntrevistas = await _httpClient.GetFromJsonAsync<IEnumerable<EntrevistaGetAllDto>>("api/entrevistas");
        Assert.NotNull(allEntrevistas);
        Assert.NotEmpty(allEntrevistas);

        // test get paginated
        int pageNumber = 1, pageSize = 10;
        var paginatedEntrevistas = await _httpClient.GetFromJsonAsync<PagedListDto<EntrevistaGetAllDto>>($"api/entrevistas?pagenumber={pageNumber}&pagesize={pageSize}");
        Assert.NotNull(paginatedEntrevistas);
        Assert.NotNull(paginatedEntrevistas.Items);
        Assert.NotNull(paginatedEntrevistas.MetaData);
        Assert.Equal(pageSize, paginatedEntrevistas.MetaData.PageSize);
        Assert.Equal(pageNumber, paginatedEntrevistas.MetaData.CurrentPage);
        Assert.Equal(allEntrevistas.Count(), paginatedEntrevistas.MetaData.TotalCount);

        // test filter nome candidato
        string nomeCandidato = "a";
        var nomeCandidatoEntrevistas = await _httpClient.GetFromJsonAsync<IEnumerable<EntrevistaGetAllDto>>($"api/entrevistas?nomecandidato={nomeCandidato}");
        Assert.NotNull(nomeCandidatoEntrevistas);
        foreach (var nomeCandidatoEntrevista in nomeCandidatoEntrevistas)
        {
            Assert.True(nomeCandidatoEntrevista.NomeCandidato.ToLower().Contains(nomeCandidato.ToLower()));
        }

        // test filter before
        DateTime now = DateTime.Now.Date;
        var beforeEntrevistas = await _httpClient.GetFromJsonAsync<IEnumerable<EntrevistaGetAllDto>>($"api/entrevistas?before={now.ToString("yyyy-MM-ddTHH:mm:ss")}");
        Assert.NotNull(beforeEntrevistas);
        foreach (var beforeEntrevista in beforeEntrevistas)
        {
            Assert.True(beforeEntrevista.DataHora <= now);
        }

        // test filter after
        var afterEntrevistas = await _httpClient.GetFromJsonAsync<IEnumerable<EntrevistaGetAllDto>>($"api/entrevistas?after={now.ToString("yyyy-MM-ddTHH:mm:ss")}");
        Assert.NotNull(afterEntrevistas);
        foreach (var afterEntrevista in afterEntrevistas)
        {
            Assert.True(afterEntrevista.DataHora >= now);
        }

        Guid resultadoId = Guid.Parse("6f6d7cc0-f4a7-4b7a-8fe4-890467b579f6");
        var resultadoEntrevistas = await _httpClient.GetFromJsonAsync<IEnumerable<EntrevistaGetAllDto>>($"api/entrevistas?resultadoid={resultadoId}");
        Assert.NotNull(resultadoEntrevistas);
        foreach (var resultadoEntrevista in resultadoEntrevistas)
        {
            Assert.True(resultadoId.Equals(resultadoEntrevista.Resultado.Id));
        }
    }

    [Fact]
    public async Task GetEntrevista_Admin()
    {
        await LoginAs_Admin();

        var allEntrevistas = await _httpClient.GetFromJsonAsync<IEnumerable<EntrevistaGetAllDto>>("api/entrevistas");
        Assert.NotNull(allEntrevistas);
        Assert.NotEmpty(allEntrevistas);

        var entrevista = await _httpClient.GetFromJsonAsync<EntrevistaGetOneDto>($"api/entrevistas/{allEntrevistas.First().Id}");
        Assert.NotNull(entrevista);
        Assert.NotNull(entrevista.Candidatura);
        Assert.NotNull(entrevista.Entrevistador);
        Assert.NotNull(entrevista.Resultado);
    }

    [Fact]
    public async Task CreateEntrevista_Admin()
    {
        await LoginAs_Admin();

        var funcs = await _httpClient.GetFromJsonAsync<IEnumerable<FuncionarioGetAllDto>>($"api/funcionarios/");
        Assert.NotNull(funcs);

        var func = funcs.First();
        Assert.NotNull(func);

        var candidaturas = await _httpClient.GetFromJsonAsync<IEnumerable<CandidaturaGetAllDto>>("api/candidaturas");
        Assert.NotNull(candidaturas);
        Assert.NotEmpty(candidaturas);

        var candidatura = candidaturas.Where(c => c.EmpresaNome.ToLower().Equals(func.Empresa.ToLower())).First();
        Assert.NotNull(candidatura);

        EntrevistaCreateDto entrevista = new()
        {
            CandidaturaId = candidatura.Id,
            EntrevistadorId = func.Id,
            DataHora = DateTime.Now.AddDays(1)
        };

        var response = await _httpClient.PostAsJsonAsync("api/entrevistas", entrevista);
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetEntrevistas_Func()
    {
        await LoginAs_Func();

        var allEntrevistas = await _httpClient.GetFromJsonAsync<IEnumerable<EntrevistaGetAllDto>>("api/entrevistas");
        Assert.NotNull(allEntrevistas);
        Assert.NotEmpty(allEntrevistas);
    }

    [Fact]
    public async Task GetEntrevista_Func()
    {
        await LoginAs_Func();

        var allEntrevistas = await _httpClient.GetFromJsonAsync<IEnumerable<EntrevistaGetAllDto>>("api/entrevistas");
        Assert.NotNull(allEntrevistas);
        Assert.NotEmpty(allEntrevistas);

        var entrevista = await _httpClient.GetFromJsonAsync<EntrevistaGetOneDto>($"api/entrevistas/{allEntrevistas.First().Id}");
        Assert.NotNull(entrevista);
        Assert.NotNull(entrevista.Candidatura);
        Assert.NotNull(entrevista.Entrevistador);
        Assert.NotNull(entrevista.Resultado);
    }

    [Fact]
    public async Task CreateEntrevista_Func()
    {
        await LoginAs_Func();

        var candidaturas = await _httpClient.GetFromJsonAsync<IEnumerable<CandidaturaGetAllDto>>("api/candidaturas");
        Assert.NotNull(candidaturas);
        Assert.NotEmpty(candidaturas);

        var candidatura = candidaturas.First();
        Assert.NotNull(candidatura);

        var funcEmpresa = await _httpClient.GetFromJsonAsync<EmpresaDto>("api/empresas/my");
        Assert.NotNull(funcEmpresa);

        var funcs = await _httpClient.GetFromJsonAsync<IEnumerable<FuncionarioGetAllDto>>($"api/funcionarios/empresa/{funcEmpresa.Id}");
        Assert.NotNull(funcs);

        var func = funcs.First();
        Assert.NotNull(func);

        EntrevistaCreateDto entrevista = new()
        {
            CandidaturaId = candidatura.Id,
            EntrevistadorId = func.Id,
            DataHora = DateTime.Now.AddDays(1)
        };

        var response = await _httpClient.PostAsJsonAsync("api/entrevistas", entrevista);
        Assert.True(response.IsSuccessStatusCode);
    }

    private async Task LoginAs_Admin()
    {
        var loginData = new UserForAuthenticationDto() { Username = "admin@admin.pt", Password = "Trigenius2025#" };
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/authentication/login", loginData);
        Assert.True(response.IsSuccessStatusCode);
        var token = await response.Content.ReadFromJsonAsync<LoginResult>();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
    }

    private async Task LoginAs_Func()
    {
        var loginData = new UserForAuthenticationDto() { Username = "cazevedo@pessoal.pt", Password = "Trigenius2025#" };
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/authentication/login", loginData);
        Assert.True(response.IsSuccessStatusCode);
        var token = await response.Content.ReadFromJsonAsync<LoginResult>();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
    }
}

