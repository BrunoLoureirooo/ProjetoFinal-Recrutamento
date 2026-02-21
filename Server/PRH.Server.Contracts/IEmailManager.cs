using PRH.Server.Entities.Models;

namespace PRH.Server.Contracts;

public interface IEmailManager
{
    void SendEntrevistaEmail(Candidatura candidatura, Funcionario entrevistador, DateTime data);
}
