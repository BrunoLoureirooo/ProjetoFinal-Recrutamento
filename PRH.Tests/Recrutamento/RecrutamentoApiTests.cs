using PRH.Shared.DataTransferObjets;
using static PRH.Client.Wasm.Services.AuthService;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PRH.Shared.DataTransferObjets.Recrutamento;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Shared.DataTransferObjets.Cargo;

public class RecrutamentoApiTests
{
    private HttpClient _httpClient;

    public RecrutamentoApiTests()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000"), Timeout = new TimeSpan(0,3,0) };
    }

    [Fact]
    public async Task GetAllRecrutamentos_Admin()
    {
        await LoginAs_Admin();

        // test get all
        var allRecrutamentos = await _httpClient.GetFromJsonAsync<IEnumerable<RecrutamentoGetAllDto>>("api/recrutamentos");
        Assert.NotNull(allRecrutamentos);
        Assert.NotEmpty(allRecrutamentos);

        // test get paginated
        int pageNumber = 1, pageSize = 10;
        var paginatedRecrutamentos = await _httpClient.GetFromJsonAsync<PagedListDto<RecrutamentoGetAllDto>>($"api/recrutamentos?pagenumber={pageNumber}&pagesize={pageSize}");
        Assert.NotNull(paginatedRecrutamentos);
        Assert.NotNull(paginatedRecrutamentos.Items);
        Assert.NotNull(paginatedRecrutamentos.MetaData);
        Assert.Equal(pageSize, paginatedRecrutamentos.MetaData.PageSize);
        Assert.Equal(pageNumber, paginatedRecrutamentos.MetaData.CurrentPage);
        Assert.Equal(allRecrutamentos.Count(), paginatedRecrutamentos.MetaData.TotalCount);

        // test filter nome
        string nome = "demo";
        var nomeRecrutamentos = await _httpClient.GetFromJsonAsync<IEnumerable<RecrutamentoGetAllDto>>($"api/recrutamentos?nome={nome}");
        Assert.NotNull(nomeRecrutamentos);
        foreach(var nomeRecrutamento in nomeRecrutamentos)
        {
            Assert.True(nomeRecrutamento.Nome.Contains(nome, StringComparison.CurrentCultureIgnoreCase));
        }

        // test filter empresa
        string empresaId = "732EF83D-1DD0-42C8-A0DF-5348B7C4D2AE";
        var empresaRecrutamentos = await _httpClient.GetFromJsonAsync<IEnumerable<RecrutamentoGetAllDto>>($"api/recrutamentos?empresaid={empresaId}");
        Assert.NotNull(empresaRecrutamentos);
        foreach (var empresaRecrutamento in empresaRecrutamentos)
        {
            Assert.True(Guid.Parse(empresaId).Equals(empresaRecrutamento.Empresa.Id));
        }

        // test filter before
        DateTime now = DateTime.Now.Date;
        var beforeRecrutamentos = await _httpClient.GetFromJsonAsync<IEnumerable<RecrutamentoGetAllDto>>($"api/recrutamentos?before={now.ToString("yyyy-MM-ddTHH:mm:ss")}");
        Assert.NotNull(beforeRecrutamentos);
        foreach (var beforeRecrutamento in beforeRecrutamentos)
        {
            Assert.True(beforeRecrutamento.Data <= now);
        }

        // test filter after
        var afterRecrutamentos = await _httpClient.GetFromJsonAsync<IEnumerable<RecrutamentoGetAllDto>>($"api/recrutamentos?after={now.ToString("yyyy-MM-ddTHH:mm:ss")}");
        Assert.NotNull(afterRecrutamentos);
        foreach (var afterRecrutamento in afterRecrutamentos)
        {
            Assert.True(afterRecrutamento.Data >= now);
        }
    }

    [Fact]
    public async Task GetRecrutamento_Admin()
    {
        await LoginAs_Admin();

        var allRecrutamentos = await _httpClient.GetFromJsonAsync<IEnumerable<RecrutamentoGetAllDto>>("api/recrutamentos");
        Assert.NotNull(allRecrutamentos);
        Assert.NotEmpty(allRecrutamentos);

        var recrutamento = await _httpClient.GetFromJsonAsync<RecrutamentoGetOneDto>($"api/recrutamentos/{allRecrutamentos.First().Id}");
        Assert.NotNull(recrutamento);
        Assert.NotNull(recrutamento.Vagas);
    }

    [Fact]
    public async Task CreateRecrutamento_Admin()
    {
        await LoginAs_Admin();

        var empresas = await _httpClient.GetFromJsonAsync<IEnumerable<EmpresaDto>>("api/empresas");
        var modalidades = await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/modalidades");
        var cargos = await _httpClient.GetFromJsonAsync<IEnumerable<CargoGetAllDto>>("api/cargos");
        var tiposVaga = await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/tipos-vaga");
        var formularios = await _httpClient.GetFromJsonAsync<IEnumerable<FormularioGetAllDto>>("api/formularios");
        Assert.NotNull(empresas);
        Assert.NotEmpty(empresas);
        Assert.NotNull(modalidades);
        Assert.NotEmpty(modalidades);
        Assert.NotNull(cargos);
        Assert.NotEmpty(cargos);
        Assert.NotNull(tiposVaga);
        Assert.NotEmpty(tiposVaga);
        Assert.NotNull(formularios);
        Assert.NotEmpty(formularios);

        RecrutamentoCreateDto create = new()
        {
            Nome = "CreateRecrutamento_Admin_TEST",
            EmpresaId = empresas.First().Id,
            Vagas =
            [
                new() {
                    CandidaturaDisponivel = true,
                    NumVagas = 5,
                    Descricao = "CreateRecrutamento_Admin_TEST_1",
                    ModalidadeId = modalidades.First().Id,
                    CargoId = cargos.First().Id,
                    TipoVagaId = tiposVaga.First().Id,
                    FormularioIds =
                    [
                        formularios.First().Id
                    ]
                },
                new() {
                    CandidaturaDisponivel = true,
                    NumVagas = 5,
                    Descricao = "CreateRecrutamento_Admin_TEST_2",
                    ModalidadeId = modalidades.First().Id,
                    CargoId = cargos.First().Id,
                    TipoVagaId = tiposVaga.First().Id,
                    FormularioIds =
                    [
                        formularios.First().Id
                    ]
                }
            ]
        };

        var response = await _httpClient.PostAsJsonAsync("api/recrutamentos", create);
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task UpdateRecrutamento_Admin()
    {
        await LoginAs_Admin();

        var allRecrutamentos = await _httpClient.GetFromJsonAsync<PagedListDto<RecrutamentoGetAllDto>>("api/recrutamentos?pagenumber=1"); 
        var empresas = await _httpClient.GetFromJsonAsync<IEnumerable<EmpresaDto>>("api/empresas");
        Assert.NotNull(allRecrutamentos);
        Assert.NotEmpty(allRecrutamentos.Items);
        Assert.NotNull(empresas);
        Assert.NotEmpty(empresas);

        Guid id = allRecrutamentos.Items.First().Id;

        // update name

        string newName = "UpdateRecrutamento_Admin_TEST";
        RecrutamentoUpdateDto updateName = new()
        {
            Nome = newName
        };

        var response = await _httpClient.PatchAsJsonAsync($"api/recrutamentos/{id}", updateName);
        Assert.True(response.IsSuccessStatusCode);

        var recrutamento = await _httpClient.GetFromJsonAsync<RecrutamentoGetOneDto>($"api/recrutamentos/{id}");
        Assert.Equal(newName, recrutamento.Nome);

        // update date

        DateTime newDate = DateTime.Now.Date;
        RecrutamentoUpdateDto updateDate = new()
        {
            EmpresaId = null,
            Nome = null,
            Data = newDate
        };

        response = await _httpClient.PatchAsJsonAsync($"api/recrutamentos/{id}", updateDate);
        Assert.True(response.IsSuccessStatusCode);

        recrutamento = await _httpClient.GetFromJsonAsync<RecrutamentoGetOneDto>($"api/recrutamentos/{id}");
        Assert.Equal(newDate, recrutamento.Data);

        // update empresa

        Guid newEmpresaId = empresas.First().Id;
        RecrutamentoUpdateDto updateEmpresa = new()
        {
            EmpresaId = newEmpresaId,
            Nome = null,
            Data = null
        };

        response = await _httpClient.PatchAsJsonAsync($"api/recrutamentos/{id}", updateEmpresa);
        Assert.True(response.IsSuccessStatusCode);

        recrutamento = await _httpClient.GetFromJsonAsync<RecrutamentoGetOneDto>($"api/recrutamentos/{id}");
        Assert.Equal(newEmpresaId, recrutamento.Empresa.Id);
    }

    [Fact]
    public async Task GetAllRecrutamentos_Func()
    {
        await LoginAs_Func();

        var funcEmpresa = await _httpClient.GetFromJsonAsync<EmpresaDto>("api/empresas/my");
        Assert.NotNull(funcEmpresa);
        var allRecrutamentos = await _httpClient.GetFromJsonAsync<IEnumerable<RecrutamentoGetAllDto>>("api/recrutamentos");
        Assert.NotNull(allRecrutamentos);
        Assert.NotEmpty(allRecrutamentos);
        foreach (var recrutamento in allRecrutamentos)
        {
            Assert.True(recrutamento.Empresa.Id.Equals(funcEmpresa.Id));
        }
    }

    [Fact]
    public async Task GetRecrutamento_Func()
    {
        await LoginAs_Func();

        var allRecrutamentos = await _httpClient.GetFromJsonAsync<IEnumerable<RecrutamentoGetAllDto>>("api/recrutamentos");
        Assert.NotNull(allRecrutamentos);
        Assert.NotEmpty(allRecrutamentos);

        var funcEmpresa = await _httpClient.GetFromJsonAsync<EmpresaDto>("api/empresas/my");
        Assert.NotNull(funcEmpresa);
        var recrutamento = await _httpClient.GetFromJsonAsync<RecrutamentoGetOneDto>($"api/recrutamentos/{allRecrutamentos.First().Id}");
        Assert.NotNull(recrutamento);
        Assert.NotNull(recrutamento.Vagas);
        Assert.True(recrutamento.Empresa.Id.Equals(funcEmpresa.Id));
    }

    [Fact]
    public async Task CreateRecrutamento_Func()
    {
        await LoginAs_Func();

        var funcEmpresa = await _httpClient.GetFromJsonAsync<EmpresaDto>("api/empresas/my");
        var modalidades = await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/modalidades");
        var cargos = await _httpClient.GetFromJsonAsync<IEnumerable<CargoGetAllDto>>("api/cargos");
        var tiposVaga = await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/tipos-vaga");
        var formularios = await _httpClient.GetFromJsonAsync<IEnumerable<FormularioGetAllDto>>("api/formularios");
        Assert.NotNull(funcEmpresa);
        Assert.NotNull(modalidades);
        Assert.NotEmpty(modalidades);
        Assert.NotNull(cargos);
        Assert.NotEmpty(cargos);
        Assert.NotNull(tiposVaga);
        Assert.NotEmpty(tiposVaga);
        Assert.NotNull(formularios);
        Assert.NotEmpty(formularios);

        RecrutamentoCreateDto create = new()
        {
            Nome = "CreateRecrutamento_Admin_TEST",
            EmpresaId = funcEmpresa.Id,
            Vagas =
            [
                new() {
                    CandidaturaDisponivel = true,
                    NumVagas = 5,
                    Descricao = "CreateRecrutamento_Admin_TEST_1",
                    ModalidadeId = modalidades.First().Id,
                    CargoId = cargos.First().Id,
                    TipoVagaId = tiposVaga.First().Id,
                    FormularioIds =
                    [
                        formularios.First().Id
                    ]
                },
                new() {
                    CandidaturaDisponivel = true,
                    NumVagas = 5,
                    Descricao = "CreateRecrutamento_Admin_TEST_2",
                    ModalidadeId = modalidades.First().Id,
                    CargoId = cargos.First().Id,
                    TipoVagaId = tiposVaga.First().Id,
                    FormularioIds =
                    [
                        formularios.First().Id
                    ]
                }
            ]
        };

        var response = await _httpClient.PostAsJsonAsync("api/recrutamentos", create);
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task UpdateRecrutamento_Func()
    {
        await LoginAs_Func();

        var allRecrutamentos = await _httpClient.GetFromJsonAsync<IEnumerable<RecrutamentoGetAllDto>>("api/recrutamentos");
        Assert.NotNull(allRecrutamentos);
        Assert.NotEmpty(allRecrutamentos);

        Guid id = allRecrutamentos.First().Id;

        // update name

        string newName = "UpdateRecrutamento_Func_TEST";
        RecrutamentoUpdateDto updateName = new()
        {
            Nome = newName
        };

        var response = await _httpClient.PatchAsJsonAsync($"api/recrutamentos/{id}", updateName);
        Assert.True(response.IsSuccessStatusCode);

        var recrutamento = await _httpClient.GetFromJsonAsync<RecrutamentoGetOneDto>($"api/recrutamentos/{id}");
        Assert.Equal(newName, recrutamento.Nome);

        // update date

        DateTime newDate = DateTime.Now.Date;
        RecrutamentoUpdateDto updateDate = new()
        {
            Data = newDate,
            Nome = null,
            EmpresaId = null
        };

        response = await _httpClient.PatchAsJsonAsync($"api/recrutamentos/{id}", updateDate);
        Assert.True(response.IsSuccessStatusCode);

        recrutamento = await _httpClient.GetFromJsonAsync<RecrutamentoGetOneDto>($"api/recrutamentos/{id}");
        Assert.Equal(newDate, recrutamento.Data);
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

