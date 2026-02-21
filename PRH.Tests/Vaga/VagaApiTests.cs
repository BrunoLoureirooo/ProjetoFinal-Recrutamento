using PRH.Shared.DataTransferObjets;
using static PRH.Client.Wasm.Services.AuthService;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PRH.Shared.DataTransferObjets.Recrutamento;
using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Shared.DataTransferObjets.Vaga;
using PRH.Shared.DataTransferObjets.RequestFeatures;
using PRH.Shared.DataTransferObjets.Cargo;

public class VagaApiTests
{
    private HttpClient _httpClient;

    public VagaApiTests()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
    }

    [Fact]
    public async Task GetVaga_Admin()
    {
        await LoginAs_Admin();

        var allRecrutamentos = await _httpClient.GetFromJsonAsync<PagedListDto<RecrutamentoGetAllDto>>("api/recrutamentos?pagenumber=1&candidaturasdisponiveis=true");
        Assert.NotNull(allRecrutamentos);
        Assert.NotEmpty(allRecrutamentos.Items);

        var recrutamento = await _httpClient.GetFromJsonAsync<RecrutamentoGetOneDto>($"api/recrutamentos/{allRecrutamentos.Items.First().Id}");
        Assert.NotNull(recrutamento);
        Assert.NotNull(recrutamento.Vagas);

        Guid id = recrutamento.Vagas.First().Id;

        var vaga = await _httpClient.GetFromJsonAsync<VagaGetOneDto>($"api/vagas/{id}");
        Assert.NotNull(vaga);
        Assert.NotNull(vaga.Cargo);
        Assert.NotNull(vaga.Modalidade);
        Assert.NotNull(vaga.TipoVaga);
        Assert.NotNull(vaga.Candidaturas);
        Assert.NotNull(vaga.Formularios);
    }

    [Fact]
    public async Task GetVagasByEmpresa_Admin()
    {
        await LoginAs_Admin();

        var empresas = await _httpClient.GetFromJsonAsync<IEnumerable<EmpresaDto>>("api/empresas");
        Assert.NotNull(empresas);
        Assert.NotEmpty(empresas);

        Guid empresaId = empresas.First().Id;

        var vagas = await _httpClient.GetFromJsonAsync<IEnumerable<VagaGetAllDto>>($"api/vagas/empresa/{empresaId}");
        Assert.NotNull(vagas);
        foreach(var vaga in vagas)
        {
            Assert.NotNull(vaga.Cargo);
            Assert.NotNull(vaga.Modalidade);
            Assert.NotNull(vaga.TipoVaga);
        }
    }

    [Fact]
    public async Task CreateVaga_Admin()
    {
        await LoginAs_Admin();

        var allRecrutamentos = await _httpClient.GetFromJsonAsync<PagedListDto<RecrutamentoGetAllDto>>("api/recrutamentos?pagenumber=1");
        var modalidades = await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/modalidades");
        var cargos = await _httpClient.GetFromJsonAsync<IEnumerable<CargoGetAllDto>>("api/cargos");
        var tiposVaga = await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/tipos-vaga");
        var formularios = await _httpClient.GetFromJsonAsync<IEnumerable<FormularioGetAllDto>>("api/formularios");
        Assert.NotNull(allRecrutamentos);
        Assert.NotEmpty(allRecrutamentos.Items);
        Assert.NotNull(modalidades);
        Assert.NotEmpty(modalidades);
        Assert.NotNull(cargos);
        Assert.NotEmpty(cargos);
        Assert.NotNull(tiposVaga);
        Assert.NotEmpty(tiposVaga);
        Assert.NotNull(formularios);
        Assert.NotEmpty(formularios);

        VagaCreateDto create = new()
        {
            RecrutamentoId = allRecrutamentos.Items.First().Id,
            CandidaturaDisponivel = true,
            NumVagas = 5,
            Descricao = "CreateVaga_Admin_TEST_1",
            ModalidadeId = modalidades.First().Id,
            CargoId = cargos.First().Id,
            TipoVagaId = tiposVaga.First().Id,
            FormularioIds =
            [
                formularios.First().Id
            ]
                
        };

        var response = await _httpClient.PostAsJsonAsync("api/vagas", create);
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetVaga_Func()
    {
        await LoginAs_Func();

        var allRecrutamentos = await _httpClient.GetFromJsonAsync<IEnumerable<RecrutamentoGetAllDto>>("api/recrutamentos?candidaturasdisponiveis=true");
        Assert.NotNull(allRecrutamentos);
        Assert.NotEmpty(allRecrutamentos);

        var recrutamento = await _httpClient.GetFromJsonAsync<RecrutamentoGetOneDto>($"api/recrutamentos/{allRecrutamentos.First().Id}");
        Assert.NotNull(recrutamento);
        Assert.NotNull(recrutamento.Vagas);

        Guid id = recrutamento.Vagas.First().Id;

        var vaga = await _httpClient.GetFromJsonAsync<VagaGetOneDto>($"api/vagas/{id}");
        Assert.NotNull(vaga);
        Assert.NotNull(vaga.Cargo);
        Assert.NotNull(vaga.Modalidade);
        Assert.NotNull(vaga.TipoVaga);
        Assert.NotNull(vaga.Candidaturas);
        Assert.NotNull(vaga.Formularios);
    }

    [Fact]
    public async Task GetVagasByEmpresa_Func()
    {
        await LoginAs_Func();

        var funcEmpresa = await _httpClient.GetFromJsonAsync<EmpresaDto>("api/empresas/my");
        Assert.NotNull(funcEmpresa);

        Guid empresaId = funcEmpresa.Id;

        var vagas = await _httpClient.GetFromJsonAsync<IEnumerable<VagaGetAllDto>>($"api/vagas/empresa/{empresaId}");
        Assert.NotNull(vagas);
        foreach (var vaga in vagas)
        {
            Assert.NotNull(vaga.Cargo);
            Assert.NotNull(vaga.Modalidade);
            Assert.NotNull(vaga.TipoVaga);
        }
    }

    [Fact]
    public async Task CreateVaga_Func()
    {
        await LoginAs_Func();

        var allRecrutamentos = await _httpClient.GetFromJsonAsync<IEnumerable<RecrutamentoGetAllDto>>("api/recrutamentos");
        var modalidades = await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/modalidades");
        var cargos = await _httpClient.GetFromJsonAsync<IEnumerable<CargoGetAllDto>>("api/cargos");
        var tiposVaga = await _httpClient.GetFromJsonAsync<IEnumerable<LookupDto>>("api/lookups/tipos-vaga");
        var formularios = await _httpClient.GetFromJsonAsync<IEnumerable<FormularioGetAllDto>>("api/formularios");
        Assert.NotNull(allRecrutamentos);
        Assert.NotEmpty(allRecrutamentos);
        Assert.NotNull(modalidades);
        Assert.NotEmpty(modalidades);
        Assert.NotNull(cargos);
        Assert.NotEmpty(cargos);
        Assert.NotNull(tiposVaga);
        Assert.NotEmpty(tiposVaga);
        Assert.NotNull(formularios);
        Assert.NotEmpty(formularios);

        VagaCreateDto create = new()
        {
            RecrutamentoId = allRecrutamentos.First().Id,
            CandidaturaDisponivel = true,
            NumVagas = 5,
            Descricao = "CreateVaga_Func_TEST_1",
            ModalidadeId = modalidades.First().Id,
            CargoId = cargos.First().Id,
            TipoVagaId = tiposVaga.First().Id,
            FormularioIds =
            [
                formularios.First().Id
            ]

        };

        var response = await _httpClient.PostAsJsonAsync("api/vagas", create);
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

