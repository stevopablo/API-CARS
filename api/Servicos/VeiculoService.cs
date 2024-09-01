namespace Api.IVeiculoServico;  

using System.Collections.Generic;
using System.Net.Quic;
using Api.Db.context;
using Api.Entidades;
using Api.IAdmServico;
using Microsoft.EntityFrameworkCore;

public class VeiculoServico : IVeiculoServico
    {
        private readonly MyDbContext _contexto;

        public VeiculoServico(MyDbContext contexto)
        {
            _contexto = contexto;
        }

    public void Apagar(Veiculo veiculo)
    {
        _contexto.Veiculo.Remove(veiculo);
        _contexto.SaveChanges();      
    }

    public void Atualizar(Veiculo veiculo)
    {
        _contexto.Veiculo.Update(veiculo);
        _contexto.SaveChanges();
    }

    public Veiculo? BuscaPorId(int id)
    {
        return _contexto.Veiculo.Where(v => v.Id == id).FirstOrDefault();
    }

    public void Incluir(Veiculo veiculo)
    {
        _contexto.Veiculo.Add(veiculo);
        _contexto.SaveChanges();
    }


     public List<Veiculo> Todos(int pagina = 1, string? nome = null, string? marca = null)
        {
            var query = _contexto.Veiculo.AsQueryable();
            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(v => v.Nome.ToLower().Contains(nome.ToLower()));
            }
            if (!string.IsNullOrEmpty(marca))
            {
                query = query.Where(v => v.Marca.ToLower().Contains(marca.ToLower()));
            }
           
            return query.Skip((pagina - 1) * 10).Take(10).ToList();

        }
}

