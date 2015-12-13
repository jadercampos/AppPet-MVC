using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AppPet.MVC.Models
{
    public class AppPetContext : DbContext
    {
        public DbSet<Especie> Especies { get; set; }
        public DbSet<Raca> Racas { get; set; }
        public DbSet<TipoServico> TipoServico { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        //public DbSet<Unidade> Unidades { get; set; }
        public AppPetContext()
            : base("DefaultConnection")
        {
        }

    }
}