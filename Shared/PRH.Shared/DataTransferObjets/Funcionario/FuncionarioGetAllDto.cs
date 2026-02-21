namespace PRH.Shared.DataTransferObjets.Funcionario;

public class FuncionarioGetAllDto
{
    public FuncionarioGetAllDto()
    {
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Empresa { get; set; }

}
