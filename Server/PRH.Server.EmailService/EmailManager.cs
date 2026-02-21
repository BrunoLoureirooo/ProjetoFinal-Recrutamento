using PRH.Server.Contracts;
using PRH.Server.Entities.Models;
using System.Net.Mail;

namespace PRH.Server.EmailService;

public class EmailManager : IEmailManager
{
    private readonly SmtpClient _smtpClient;
    private readonly MailAddress _sender;
    private readonly ILoggerManager _logger;
    public EmailManager(ILoggerManager logger)
    {
        _logger = logger;

        // configuração dummy para smtp4dev
        _smtpClient = new SmtpClient("localhost")
        {
            Port = 25,
            EnableSsl = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false
        };
        _sender = new MailAddress("prh@prh.pt");
    }
    public void SendEntrevistaEmail(Candidatura candidatura, Funcionario entrevistador, DateTime data)
    {
        int hora = DateTime.Now.Hour;
        string opening;
        if (hora >= 7 && hora <= 12)
            opening = "Bom Dia";
        else if (hora >= 13 && hora <= 19)
            opening = "Boa Tarde";
        else
            opening = "Boa Noite";

        var mailMessage = new MailMessage
            {
                From = _sender,
                Subject = $"Seleção para entrevista na empresa {candidatura.Vaga.Recrutamento.Empresa.Nome}",
                Body = $"{opening} Sr(a). {candidatura.NomeCandidato}" + Environment.NewLine +
                $"Foi selecionado(a) para uma entrevista para o cargo de {candidatura.Vaga.Cargo.Nome} na empresa {candidatura.Vaga.Recrutamento.Empresa.Nome}." + Environment.NewLine +
                $"Será entrevistado pelo(a) Sr(a). {entrevistador.ApplicationUser.Nome} no dia {data.ToString("dd/MM/yyyy")} às {data.ToString("HH:mm")}.",
                IsBodyHtml = false,
            };

        mailMessage.To.Add(candidatura.EmailCandidato);
        mailMessage.CC.Add(entrevistador.Email);

        try
        {
            _smtpClient.SendAsync(mailMessage, null);
            _logger.LogInfo("E-mail enviado para " + candidatura.EmailCandidato + " (candidato) com conhecimento de " + entrevistador.Email + " (entrevistador).");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao enviar email: {ex.Message}");
        }
    }
}
