using pt.portugal.eid;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
var app = builder.Build();
app.UseCors();

app.MapPost("/assinar", async (HttpRequest req, HttpResponse resp) =>
{
    using var msIn = new MemoryStream();
    await req.Body.CopyToAsync(msIn);
    var input = msIn.ToArray();

    var tempIn = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.pdf");
    var tempOut = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.pdf");
    await File.WriteAllBytesAsync(tempIn, input);

    try
    {
        PTEID_ReaderSet.initSDK();
        var reader = PTEID_ReaderSet.instance().getReader();
        if (reader == null)
        {
            throw new PTEID_ExNoReader();
        }
        if (!reader.isCardPresent())
        {
            throw new PTEID_ExNoCardPresent();
        }
        var card = reader.getEIDCard();

        var sig = new PTEID_PDFSignature(tempIn);
        sig.setSignatureLevel(PTEID_SignatureLevel.PTEID_LEVEL_TIMESTAMP);
        int pagina = 1; 
        double posX = 100;
        double posY = 500;
        string localizacao = "Leiria, Portugal";
        string motivo = "Aprovação do Documento";
        card.SignPDF(sig, pagina, posX, posY, localizacao, motivo, tempOut);

        PTEID_ReaderSet.releaseSDK();

        var signed = await File.ReadAllBytesAsync(tempOut);
        return Results.File(signed, "application/pdf", "signed.pdf");
    }
    catch (PTEID_ExNoReader ex)
    {
        return Results.Problem("Nenhum leitor de cartão encontrado. Por favor, conecte um leitor e tente novamente.");
    }
    catch (PTEID_ExNoCardPresent ex)
    {
        return Results.Problem("Nenhum cartão encontrado. Por favor, insira um cartão e tente novamente.");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro na assinatura: {ex.Message}");
    }
    finally
    {
        File.Delete(tempIn);
        File.Delete(tempOut);
    }
});

app.Run("http://localhost:5004");
