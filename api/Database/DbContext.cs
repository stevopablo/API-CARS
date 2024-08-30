// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;
// using Microsoft.Extensions.Configuration;
// using Api.Entidades;

// namespace Api.Db.context
// {
//     // HERDA DO ENTITY
//     public class MyDbContext : DbContext
//     {
//         private readonly IConfiguration _configAppSettings;

//         public MyDbContext(IConfiguration configAppSettings)
//         {
//             _configAppSettings = configAppSettings;
//         }

//         public DbSet<Administrador> Administradores { get; set; } = default!;

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
//                 var stringConexao = _configAppSettings.GetConnectionString("mysql");

//                 if (!string.IsNullOrEmpty(stringConexao))
//                 {
//                     optionsBuilder.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao));
//                 }
//             }
//         }
//     }
// }
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Api.Entidades;

namespace Api.Db.context
{
    public class MyDbContext : DbContext
    {
        private readonly IConfiguration _configAppSettings;

        public MyDbContext(IConfiguration configAppSettings)
        {
            _configAppSettings = configAppSettings;
        }

        public DbSet<Administrador> Administradores { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var stringConexao = _configAppSettings.GetConnectionString("mysql");

                if (!string.IsNullOrEmpty(stringConexao))
                {
                    optionsBuilder.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao));
                }
            }
        }
    }
}
