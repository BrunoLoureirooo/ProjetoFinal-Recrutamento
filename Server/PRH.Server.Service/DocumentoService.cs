using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using PRH.Server.Contracts;
using PRH.Server.Entities.Models;
using PRH.Server.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using System.Globalization;
using OpenXmlPowerTools;
using System.Xml.Linq;
using OpenHtmlToPdf;
using System.Security;
using System.Text.Json;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Drawing;
using PRH.Server.Repository;

namespace PRH.Server.Service
{
    public class DocumentoService : IDocumentoService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public DocumentoService(
            IRepositoryManager repository,
            ILoggerManager logger,
            IMapper mapper,
            IWebHostEnvironment env)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _env = env;
        }

        public async Task<DocumentoDto> CreateDocumentoAsync(DocumentoDto dto, bool trackChanges)
        {
            if (dto == null)
            {
                _logger.LogError("Documento vazio não permitido.");
                throw new ArgumentException("Documento vazio não permitido.");
            }

            if (!await CheckIfTipoDocumentoExists(dto.TipoDocumentoId, trackChanges))
            {
                _logger.LogError("Empresa com o id '" + dto.TipoDocumentoId + "' não existe.");
                throw new ArgumentException("Empresa com o id '" + dto.TipoDocumentoId + "' não existe.");
            }

            Guid newDocumentoId = Guid.NewGuid();
            var documento = new Documento
            {
                Id = newDocumentoId,
                TipoDocumentoId = dto.TipoDocumentoId,
                Caminho = dto.Caminho,
                CandidaturaId = dto.CandidaturaId,
                TemplateId = dto.TemplateId,
                DocumentoAssinado = false,

            };
            await _repository.Documento.CreateDocumentoAsync(documento, trackChanges);

            var addedDocumentoDto = _mapper.Map<DocumentoDto>(documento);

            return addedDocumentoDto;
        }

        public async Task<IEnumerable<DocumentoDto>> GetDocumentosByCandidaturaAsync(Guid candidaturaId)
        {
            var entities = await _repository.Documento
                .GetDocumentosByCandidaturaAsync(candidaturaId, trackChanges: false);
            return _mapper.Map<IEnumerable<DocumentoDto>>(entities);
        }

        public async Task<DocumentoDto> GetDocumentoByIdAsync(Guid id)
        {
            var entity = await _repository.Documento
                .GetDocumentoByIdAsync(id, trackChanges: false);
            if (entity == null)
            {
                _logger.LogError($"Documento {id} não encontrado.");
                throw new ArgumentException($"Documento {id} não encontrado.");
            }
            return _mapper.Map<DocumentoDto>(entity);
        }

        public async Task<byte[]?> ObterBytesDocumentoAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError($"Id do documento não pode ser Guid vazio - {id}");
                throw new ArgumentException($"Id do documento não pode ser Guid vazio - {id}");
            }

            var conteudo = await _repository.Documento.GetBytesByIdAsync(id);
            return conteudo;
        }

        public async Task<DocumentoDto> UploadDocumentoAsync(Guid candidaturaId, IFormFile file, Guid tipoDocumentoId)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Ficheiro inválido.");

            var uploadsFolder = Path.Combine(_env.ContentRootPath, "uploads", "documentos");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(uploadsFolder, fileName);
            await using (var stream = new FileStream(fullPath, FileMode.Create))
                await file.CopyToAsync(stream);

            var caminhoRelativo = $"/uploads/documentos/{fileName}";
            var documento = new Documento
            {
                Id = Guid.NewGuid(),
                CandidaturaId = candidaturaId,
                TipoDocumentoId = tipoDocumentoId,
                Caminho = caminhoRelativo,
                TemplateId = null,
                DocumentoAssinado = false,
                CamposPreenchidos = ""
            };

            await _repository.Documento.CreateDocumentoAsync(documento, trackChanges: false);
            await _repository.SaveAsync();

            return _mapper.Map<DocumentoDto>(documento);
        }

        public async Task<DocumentoDto> CreateDocumentoFromTemplateAsync(Guid candidaturaId, Guid templateId, Dictionary<string, string>? extras = null)
        {
            var tpl = await _repository.Template.GetTemplateByIdAsync(templateId, trackChanges: false);
            if (tpl == null)
            {
                _logger.LogError($"Template {templateId} não encontrado.");
                throw new ArgumentException($"Template {templateId} não encontrado.");
            }

            var uploadsFolder = Path.Combine(_env.ContentRootPath, "uploads", "documentos");
            Directory.CreateDirectory(uploadsFolder);
            var sourcePath = Path.Combine(_env.ContentRootPath, tpl.Caminho.TrimStart('/'));
            var docxName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{Path.GetExtension(sourcePath)}";
            var docxPath = Path.Combine(uploadsFolder, docxName);
            File.Copy(sourcePath, docxPath);

            var pdfName = Path.ChangeExtension(docxName, ".pdf");
            var pdfPath = Path.Combine(uploadsFolder, pdfName);

            var valores = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (tpl.Preenchivel)
            {

                if (tpl.Empresa != null) valores["Empresa"] = tpl.Empresa.Nome;


                var cand = await _repository.Candidatura.GetCandidaturaByIdAsync(candidaturaId, trackChanges: false);
                if (cand != null)
                {
                    valores["NIFcandidato"] = cand.NIFcandidato.ToString();
                    valores["NomeCandidato"] = cand.NomeCandidato;
                    valores["ContactoCandidato"] = cand.ContactoCandidato.ToString();
                    valores["EmailCandidato"] = cand.EmailCandidato;

                    var vaga = await _repository.Vaga.GetVagaByIdAsync(cand.VagaId, trackChanges: false);
                    if (vaga != null)
                    {
                        var cargo = await _repository.Cargo.GetCargoByIdAsync(vaga.CargoId, trackChanges: false);
                        if (cargo != null)
                            valores["NomeCargo"] = cargo.Nome;
                    }
                }

                var resp = await _repository.FormularioRespondido.GetRespostasByCandidaturaAsync(candidaturaId, trackChanges: false);
                if (resp != null)
                {
                    foreach (var r in resp)
                    {
                        foreach (var c in r.CampoRespondidos)
                        {
                            valores[c.Nome] = c.Resposta;

                            if (c.Tipo == "Data")
                            {
                                DateTime data;
                                if (DateTime.TryParse(c.Resposta, out data))
                                {
                                    valores[r.Nome + "..dataSemana"] = data.ToString("dddd", new CultureInfo("pt-PT"));
                                    valores[r.Nome + "..dataExtenso"] = data.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-PT"));
                                    valores[r.Nome + "..data"] = data.ToString("dd/MM/yyyy");
                                    valores[r.Nome + "..ano"] = data.Year.ToString();
                                    valores[r.Nome + "..mes"] = data.ToString("MMMM", new CultureInfo("pt-PT"));
                                }
                            }
                            if (c.Tipo == "Número")
                            {
                                int valor;
                                if (int.TryParse(c.Resposta, out valor))
                                {
                                    valores[r.Nome + "..extenso"] = NumeroPorExtenso(valor);
                                }
                            }
                        }
                    }
                }

                var agora = DateTime.Now;
                valores["hoje..dataSemana"] = agora.ToString("dddd, dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-PT"));
                valores["hoje..dataExtenso"] = agora.ToString("dd 'de' MMMM yyyy", new CultureInfo("pt-PT"));
                valores["hoje"] = agora.ToString("dd/MM/yyyy");
                valores["hoje..ano"] = agora.Year.ToString();
                valores["hoje..mes"] = agora.ToString("MMMM", new CultureInfo("pt-PT"));

                valores["assinatura"] = "assinatura";

                if (extras != null)
                    foreach (var kv in extras)
                        valores[kv.Key] = kv.Value;

                ConvertDocxToPdfFillingPlaceholders(docxPath, pdfPath, valores);
            }
            else
            {
                ConvertDocxToPdf(docxPath, pdfPath);
            }

            try
            {
                File.Delete(docxPath);
                var caminhoRelativo = $"/uploads/documentos/{pdfName}";
                var entity = new Documento
                {
                    Id = Guid.NewGuid(),
                    CandidaturaId = candidaturaId,
                    TemplateId = templateId,
                    TipoDocumentoId = tpl.TipoDocumentoId,
                    Caminho = caminhoRelativo,
                    CamposPreenchidos = valores == null ? null : CryptoHelper.Encrypt(JsonSerializer.Serialize(valores)),
                    DocumentoAssinado = false
                };
                await _repository.Documento.CreateDocumentoAsync(entity, trackChanges: false);
                await _repository.SaveAsync();
                return _mapper.Map<DocumentoDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Error: {ex.Message}");
                throw new Exception("Erro ao tentar excluir o arquivo associado ao template.", ex);
            }
        }

        public async Task UpdateDocumentoAsync(Guid id, DocumentoDto dto)
        {
            var entity = await _repository.Documento.GetDocumentoByIdAsync(id, trackChanges: true);

            if (entity == null)
                throw new Exception("Documento não encontrado");


            try
            {
                if (!string.IsNullOrEmpty(entity.Caminho))
                {
                    var filePath = Path.Combine(_env.ContentRootPath, entity.Caminho.TrimStart('/'));
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao tentar excluir o arquivo associado ao template: {ex.Message}");
                throw new Exception("Erro ao tentar excluir o arquivo associado ao template.", ex);
            }

            entity.TipoDocumentoId = dto.TipoDocumentoId;
            entity.Caminho = dto.Caminho;
            entity.DocumentoAssinado = dto.DocumentoAssinado;

            await _repository.Documento.UpdateDocumentoAsync(entity);
        }

        public async Task DeleteDocumentoAsync(Guid id)
        {
            try
            {
                var entity = await _repository.Documento.GetDocumentoByIdAsync(id, trackChanges: true);
                if (entity == null)
                {
                    _logger.LogError($"Documento {id} não encontrado.");
                    throw new ArgumentException($"Documento {id} não encontrado.");
                }

                if (!string.IsNullOrEmpty(entity.Caminho))
                {
                    var filePath = Path.Combine(_env.ContentRootPath, entity.Caminho.TrimStart('/'));
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            File.Delete(filePath);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Falha ao apagar o ficheiro {filePath}: {ex.Message}");
                        }
                    }
                }
                await _repository.Documento.DeleteDocumentoAsync(entity);
                await _repository.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Erro ao apagar documento {Id}", id);
                throw;
            }
        }

        private async Task<bool> CheckIfTipoDocumentoExists(Guid tipoDocumentoId, bool trackChanges)
        {
            try
            {
                var tipoDocumento = await _repository.TipoDocumento.GetTipoDocumentoByIdAsync(tipoDocumentoId, trackChanges);
                return tipoDocumento is not null;
            }
            catch
            {
                return false;
            }
        }

        public async Task AssinarDocumentoAsync(AssinaturaDto dto, bool trackChanges)
        {
            var uploadsFolder = Path.Combine(_env.ContentRootPath, "uploads", "documentos");
            var docEntity = await _repository.Documento
                               .GetDocumentoByIdAsync(dto.documentoId, trackChanges: true);
            if (docEntity == null)
            {
                _logger.LogError($"Documento {dto.documentoId} não encontrado no BD.");
                throw new ArgumentException($"Documento {dto.documentoId} não encontrado no BD.");
            }

            var originalPath = Path.Combine(uploadsFolder, Path.GetFileName(docEntity.Caminho));
            if (!File.Exists(originalPath))
            {
                _logger.LogError($"Documento não encontrado - {originalPath}");
                throw new FileNotFoundException($"Documento não encontrado - {originalPath}");
            }

            var baseName = Path.GetFileNameWithoutExtension(docEntity.Caminho);
            var signedName = baseName + "_assinado.pdf";
            var signedPath = Path.Combine(uploadsFolder, signedName);

            using var document = PdfReader.Open(originalPath, PdfDocumentOpenMode.Modify);
            var page = document.Pages[document.PageCount - 1];

            using (var gfx = XGraphics.FromPdfPage(page))
            {
                const double imgW = 200, imgH = 100;
                double margin = 40, spacing = 20;
                double totalW = dto.Assinaturas.Count * imgW
                              + (dto.Assinaturas.Count - 1) * spacing;
                double startX = (page.Width.Point - totalW) / 2;
                double yPos = page.Height.Point - imgH - margin;

                for (int i = 0; i < dto.Assinaturas.Count; i++)
                {
                    var b64 = dto.Assinaturas[i].Split(',').Last();
                    var bytes = Convert.FromBase64String(b64);
                    using var ms = new MemoryStream(bytes);
                    using var img = XImage.FromStream(() => ms);

                    double x = startX + i * (imgW + spacing);
                    gfx.DrawImage(img, x, yPos, imgW, imgH);
                }
            }

            document.Save(signedPath);

            docEntity.Caminho = "/uploads/documentos/" + signedName;
            docEntity.DocumentoAssinado = true;
            await _repository.SaveAsync();
        }

        private void ConvertDocxToPdf(string docxPath, string pdfPath)
        {
            XElement htmlElement;
            var settings = new WmlToHtmlConverterSettings { PageTitle = "Documento" };
            using (var wordDoc = WordprocessingDocument.Open(docxPath, true))
            {
                htmlElement = WmlToHtmlConverter.ConvertToHtml(wordDoc, settings);
            }
            string htmlString = htmlElement.ToString(SaveOptions.DisableFormatting);

            byte[] pdfBytes;
            try
            {
                pdfBytes = Pdf
                    .From(htmlString)
                    .OfSize(PaperSize.A4)
                    .WithGlobalSetting("margin.top", "10mm")
                    .WithGlobalSetting("margin.bottom", "10mm")
                    .WithObjectSetting("web.defaultEncoding", "utf-8")
                    .Content();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao converter HTML para PDF: {ex.Message}");
                throw new InvalidOperationException("Falha na geração de PDF a partir do HTML.", ex);
            }

            File.WriteAllBytes(pdfPath, pdfBytes);
        }

        private void ConvertDocxToPdfFillingPlaceholders(string docxPath, string pdfPath, Dictionary<string, string> valores)
        {
            byte[] fileBytes = File.ReadAllBytes(docxPath);

            using var memStream = new MemoryStream();
            memStream.Write(fileBytes, 0, fileBytes.Length);
            memStream.Position = 0;

            using var wordDoc = WordprocessingDocument.Open(memStream, true);

            var settings = new WmlToHtmlConverterSettings { PageTitle = "Documento" };
            XElement htmlElement = WmlToHtmlConverter.ConvertToHtml(wordDoc, settings);
            string html = htmlElement.ToString(SaveOptions.DisableFormatting);

            var pattern = new Regex(@"\{\{\s*(?:<[^>]+>|\s)*
                         ([^<>{}\s]+)
                         (?:<[^>]+>|\s)*\}\}",
                        RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

            html = pattern.Replace(html, m => $"{{{{{m.Groups[1].Value}}}}}");

            var rx = new Regex(@"\{\{(.+?)\}\}", RegexOptions.Compiled);
            var found = rx.Matches(html)
                          .Select(m => m.Groups[1].Value.Trim())
                          .Distinct(StringComparer.OrdinalIgnoreCase)
                          .ToList();


            var missing = found.Where(key => !valores.ContainsKey(key)).ToList();
            if (missing.Any())
            {
                _logger.LogInfo($"Faltam campos para preencher no documento {docxPath}");
                throw new MissingTemplateValuesException(missing);
            }

            foreach (var kv in valores)
            {
                string placeholder = $"{{{{{kv.Key}}}}}";
                html = html.Replace(placeholder, SecurityElement.Escape(kv.Value));
            }

            byte[] pdfBytes;
            try
            {
                pdfBytes = Pdf
                    .From(html)
                    .OfSize(PaperSize.A4)
                    .WithGlobalSetting("margin.top", "10mm")
                    .WithGlobalSetting("margin.bottom", "10mm")
                    .WithObjectSetting("web.defaultEncoding", "utf-8")
                    .Content();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao converter HTML para PDF: {ex.Message}");
                throw;
            }

            File.WriteAllBytes(pdfPath, pdfBytes);
        }

        private string NumeroPorExtenso(int numero)
        {
            if (numero == 0) return "zero";

            string[] unidades = { "", "um", "dois", "três", "quatro", "cinco", "seis", "sete", "oito", "nove", "dez", "onze", "doze", "treze", "catorze", "quinze", "dezasseis", "dezassete", "dezoito", "dezanove" };
            string[] dezenas = { "", "", "vinte", "trinta", "quarenta", "cinquenta", "sessenta", "setenta", "oitenta", "noventa" };
            string[] centenas = { "", "cento", "duzentos", "trezentos", "quatrocentos", "quinhentos", "seiscentos", "setecentos", "oitocentos", "novecentos" };

            if (numero < 0)
                return "menos " + NumeroPorExtenso(Math.Abs(numero));

            if (numero < 20)
                return unidades[numero];

            if (numero < 100)
                return dezenas[numero / 10] + (numero % 10 != 0 ? " e " + NumeroPorExtenso(numero % 10) : "");

            if (numero == 100)
                return "cem";

            if (numero < 1000)
                return centenas[numero / 100] + (numero % 100 != 0 ? " e " + NumeroPorExtenso(numero % 100) : "");

            if (numero < 1000000)
            {
                int milhares = numero / 1000;
                int resto = numero % 1000;
                string mil = milhares == 1 ? "mil" : NumeroPorExtenso(milhares) + " mil";
                return mil + (resto != 0 ? (resto < 100 ? " e " : " ") + NumeroPorExtenso(resto) : "");
            }

            if (numero < 1000000000)
            {
                int milhoes = numero / 1000000;
                int resto = numero % 1000000;
                string milhao = milhoes == 1 ? "um milhão" : NumeroPorExtenso(milhoes) + " milhões";
                return milhao + (resto != 0 ? (resto < 100 ? " e " : " ") + NumeroPorExtenso(resto) : "");
            }

            return numero.ToString();
        }

    }
}
