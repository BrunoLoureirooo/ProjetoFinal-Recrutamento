using PRH.Server.API.Extensions;
using PRH.Server.API.Handlers;
using PRH.Presentation.ActionFilters;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;
using Microsoft.Extensions.FileProviders;
using PRH.Server.API;
using PRH.Server.Repository.Contracts;
using PRH.Server.Repository;
using PRH.Server.Seeding;
using PRH.Server.Repository.Configuration;


if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "uploads")))
{
    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "uploads"));
    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "uploads", "templates"));
    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "uploads", "documentos"));
}


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.development.config"));

builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.ConfigureCors(builder.Configuration);
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



//NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
//    new ServiceCollection()
//        .AddLogging()
//        .AddMvc()
//        .AddNewtonsoftJson()
//            .Services.BuildServiceProvider()
//        .GetRequiredService<IOptions<MvcOptions>>()
//            .Value.InputFormatters.OfType<NewtonsoftJsonPatchInputFormatter>()
//                .First();
static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
{
    var builder = new ServiceCollection()
        .AddLogging()
        .AddMvc()
        .AddNewtonsoftJson()
            .Services.BuildServiceProvider();

    return builder
        .GetRequiredService<IOptions<MvcOptions>>()
        .Value
        .InputFormatters
        .OfType<NewtonsoftJsonPatchInputFormatter>()
        .First();
}

builder.Services.AddScoped<ValidationFilterAttribute>();

//builder.Services.ConfigureResponseCaching();
builder.Services.ConfigureOutputCaching();

//builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    //config.ReturnHttpNotAcceptable = true;
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());

    //config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
    //{
    //    Duration = 120
    //});
})
    //.AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(PRH.Presentation.AssemblyReference).Assembly);

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);

builder.WebHost.ConfigureKestrel(opts =>
{
    opts.ListenAnyIP(5000);    // expõe a porta 5000 em 0.0.0.0
});


var app = builder.Build();

//var logger = app.Services.GetRequiredService<ILoggerManager>();
//app.ConfigureExceptionHandler(logger);

app.UseExceptionHandler(opt => { });

if (app.Environment.IsProduction())
    app.UseHsts();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
    RequestPath = "/uploads"
});



app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");
//app.UseResponseCaching();
app.UseOutputCache();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RepositoryContext>();

    /*
    if (context.Campos.Count() < 100)
    {
        var fakeCampos = CampoSeederHelper.GenerateCampos(100, context);
        context.Campos.AddRange(fakeCampos);
        context.SaveChanges();
    }

    if (context.Campos.Any() && context.OpcoesCampoLookups.Count() < 100)
    {
        var fakeOpcoes = OpcoesCampoLookupSeederHelper.GenerateOpcoes(1000, context);
        context.OpcoesCampoLookups.AddRange(fakeOpcoes);
        context.SaveChanges();
    }

    if (context.Formularios.Count() < 100)
    {
        var fakeFormularios = FormularioSeederHelper.GenerateFormularios(100, context);
        context.Formularios.AddRange(fakeFormularios);
        context.SaveChanges();
    }

    if (context.Formularios.Any() && context.Campos.Any() && context.FormularioCampos.Count() < 100)
    {
        var fakeFormulariosCampos = FormularioCampoSeederHelper.GenerateFormularioCampos(500, context);
        context.FormularioCampos.AddRange(fakeFormulariosCampos);
        context.SaveChanges();


    if (context.Cargos.Count() < 10)
    {
        var fakeCargos = CargoSeederHelper.GenerateCargos(1000);
        context.Cargos.AddRange(fakeCargos);
        context.SaveChanges();
    }

    if (context.Empresas.Count() < 100)
    {
        var fakeEmpresas = EmpresaSeederHelper.GenerateEmpresas(100);
        context.Empresas.AddRange(fakeEmpresas);
        context.SaveChanges();
    }

    if (context.Empresas.Any() && context.Recrutamentos.Count() < 100)
    {
        var fakeRecrutamentos = RecrutamentoSeederHelper.GenerateRecrutamentos(20, context);
        context.Recrutamentos.AddRange(fakeRecrutamentos);
        context.SaveChanges();
    }

    if (context.Recrutamentos.Any() && context.Cargos.Any() && context.Vagas.Count() < 100)
    {
        // Chama o helper para gerar as vagas, passando o contexto para acessar os recrutamentos no banco
        var fakeVagas = VagaSeederHelper.GenerateVagas(60, context);
        context.Vagas.AddRange(fakeVagas);
        context.SaveChanges();
    }

    if (context.Vagas.Any() && context.Candidaturas.Count() < 100)
    {
        // Chama o helper para gerar as vagas, passando o contexto para acessar os recrutamentos no banco
        var fakeCandidaturas = CandidaturaSeederHelper.GenerateCandidaturas(500, context);
        context.Candidaturas.AddRange(fakeCandidaturas);
        context.SaveChanges();
    }

    if (context.Vagas.Any() && context.Candidaturas.Any() && context.Entrevistas.Count() < 100)
    {
        var fakeEntrevistas = EntrevistaSeederHelper.GenerateEntrevistas(500, context);
        context.Entrevistas.AddRange(fakeEntrevistas);
        context.SaveChanges();
    }

    if (context.FormularioRespondidos.Count() < 100)
    {
        var fakeFormulariosRespondidos = FormularioRespondidoSeederHelper.GenerateFormulariosRespondidos(200, context);
        context.FormularioRespondidos.AddRange(fakeFormulariosRespondidos);
        context.SaveChanges();
    }

    if (context.FormularioRespondidos.Any() && context.CampoRespondidos.Count() < 100)
    {
        var fakeCamposRespondidos = CampoRespondidoSeederHelper.GenerateCamposRespondidos(400, context);
        context.CampoRespondidos.AddRange(fakeCamposRespondidos);
        context.SaveChanges();
    }

        if (context.TipoDocumentos != null && !context.TipoDocumentos.Any())
        {
            var tipoDocumento = TipoDocumentoSeeder.GetTipoDocumentos();
            context.TipoDocumentos.AddRange(tipoDocumento);
            context.SaveChanges();
        }
        if (context.EmpresaTipoDocumentos != null && !context.EmpresaTipoDocumentos.Any())
        {
            var empresaTipoDocumentos = EmpresaTipoDocumentoSeeder.GetEmpresaTipoDocumentos();
            context.EmpresaTipoDocumentos.AddRange(empresaTipoDocumentos);
            context.SaveChanges();
        }
        if (context.Templates != null && !context.Templates.Any())
        {
            var templates = TemplateSeeder.GetTemplates();
            context.Templates.AddRange(templates);
            context.SaveChanges();
        }

        if (context.Documentos != null && !context.Documentos.Any())
        {
            var documentos = DocumentoSeeder.GenerateDocumentos(30, context);
            context.Documentos.AddRange(documentos);
            context.SaveChanges();
        }
    }

    if (context.Departamentos.Count() < 100)
    {
        var fakeDepartamentos = DepartamentoSeederHelper.GenerateDepartamentos(5, context);
        context.Departamentos.AddRange(fakeDepartamentos);
        context.SaveChanges();
    }

    if (context.Equipas.Count() < 100)
    {
        var fakeEquipas = EquipaSeederHelper.GenerateEquipas(5, context);
        context.Equipas.AddRange(fakeEquipas);
        context.SaveChanges();
    }

    if (context.Estabelecimentos.Count() < 100)
    {
        var fakeEstablecimentos = EstabelecimentoSeederHelper.GenerateEstabelecimentos(100, context);
        context.Estabelecimentos.AddRange(fakeEstablecimentos);
        context.SaveChanges();
    }

    if (context.Funcoes.Count() < 100)
    {
        var fakeFuncoes = FuncaoSeederHelper.GenerateFuncoes(100, context);
        context.Funcoes.AddRange(fakeFuncoes);
        context.SaveChanges();
    }

    if (context.Users.Count() < 100)
    {
        var fakeUsers = UserSeederHelper.GenerateUsers(1000, context);
        context.Users.AddRange(fakeUsers);

        // Gera roles para cada novo usuário
        var userRoles = fakeUsers
            .Select(u => UserRoleConfiguration.MakeFuncionario(Guid.Parse(u.Id)))
            .ToList();

        context.UserRoles.AddRange(userRoles);

        context.SaveChanges();
    }

    if (context.Funcionarios.Count() < 100)
    {
        var fakeFuncionarios = FuncionarioSeederHelper.GenerateFuncionarios(1000, context);
        context.Funcionarios.AddRange(fakeFuncionarios);
        context.SaveChanges();
    }*/

    //Example 1
    //app.Run(async context =>
    //{
    //    await context.Response.WriteAsync("Hello from the middleware component.");
    //});

    //Example 2
    //app.Use(async (context, next) =>
    //{
    //    Console.WriteLine($"Logic before executing the next delegate in the Use method");
    //    await next.Invoke();
    //    Console.WriteLine($"Logic after executing the next delegate in the Use method");
    //});
    //app.Run(async context =>
    //{
    //    Console.WriteLine($"Writing the response to the client in the Run method");
    //    context.Response.StatusCode = 200;
    //    await context.Response.WriteAsync("Hello from the middleware component.");
    //});

    //Example 3
    //app.Use(async (context, next) =>
    //{
    //    await context.Response.WriteAsync("Hello from the middleware component.");
    //    await next.Invoke();
    //    Console.WriteLine($"Logic after executing the next delegate in the Use method");
    //});
    //app.Run(async context =>
    //{
    //    Console.WriteLine($"Writing the response to the client in the Run method");
    //    context.Response.StatusCode = 200;
    //    await context.Response.WriteAsync("Hello from the middleware component.");
    //});

    //Example 4
    //app.Use(async (context, next) =>  
    //{
    //    Console.WriteLine($"Logic before executing the next delegate in the Use method");
    //    await next.Invoke();
    //    Console.WriteLine($"Logic after executing the next delegate in the Use method");
    //});

    //app.Map("/usingmapbranch", builder =>
    //{
    //    builder.Use(async (context, next) =>
    //    {
    //        Console.WriteLine("Map branch logic in the Use method before the next delegate");

    //        await next.Invoke();
    //        Console.WriteLine("Map branch logic in the Use method after the next delegate");
    //    });
    //    builder.Run(async context =>
    //    {
    //        Console.WriteLine($"Map branch response to the client in the Run method");
    //        await context.Response.WriteAsync("Hello from the map branch.");
    //    });
    //});
    //app.Run(async context =>
    //{
    //    Console.WriteLine($"Writing the response to the client in the Run method");
    //    await context.Response.WriteAsync("Hello from the middleware component.");
    //});


    app.MapControllers();

    app.Run();

}
