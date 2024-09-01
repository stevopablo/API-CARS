namespace Api.IAdmServico;
using Api.Entidades;

public interface IAdmServico{
    List<Administrador> Login(api.DTOs.LoginDTO LoginDTO);

}