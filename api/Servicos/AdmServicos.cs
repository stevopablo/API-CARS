using Api.Entidades;
namespace Api.AdmServicos
{
    using api.DTOs;
    using Api.Db.context;
    using Api.IAdmServico;
    using System.Collections.Generic;
    using System.Linq;

    public class AdmServicos : IAdmServico
    {
        private readonly MyDbContext _contexto;

        public AdmServicos(MyDbContext contexto)
        {
            _contexto = contexto;
        }

        public List<Administrador> Login(LoginDTO loginDTO)
        {
            var admins = _contexto.Administradores
                .Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha)
                .ToList();

            return admins;
        }
    }
}
