namespace PRH.Shared.DataTransferObjets.Funcionario;

public class FuncionarioGetOneDto
{
    public FuncionarioGetOneDto()
    {
    }
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public EmpresaDto Empresa { get; set; }

}
