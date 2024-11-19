using PixApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixApplication.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=MeuBancoConnectionString")
        {
        }

        public DbSet<ConfigPix> ConfigPixes { get; set; }
        public DbSet<Authentication> Authentications { get; set; }
        public DbSet<CobrancaPix> Cobrancas { get; set; }
        public DbSet<TokenResponse> TokenResponses { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Authentication
            modelBuilder.Entity<Authentication>()
                .ToTable("Authentications");

            modelBuilder.Entity<Authentication>()
                .Property(a => a.ClientID)
                .HasMaxLength(500);

            modelBuilder.Entity<Authentication>()
               .Property(a => a.ClientSecret)
               .HasMaxLength(500);

            modelBuilder.Entity<Authentication>()
              .Property(a => a.ApplicationKey)
              .HasMaxLength(500);


            //ConfigPix
            modelBuilder.Entity<ConfigPix>()
                 .ToTable("ConfigPix")
                 .Property(a => a.Id);

            modelBuilder.Entity<ConfigPix>()
                 .Property(a => a.ChavePix);

            modelBuilder.Entity<ConfigPix>()
                 .Property(a => a.Identificacao);

            modelBuilder.Entity<ConfigPix>()
                .Property(a => a.Name);

            modelBuilder.Entity<ConfigPix>()
                .Property(a => a.ExpirePix);

            //CobrancaPix
            modelBuilder.Entity<CobrancaPix>()
                 .ToTable("CobrancaPix")
                 .Property(a => a.Id);

            modelBuilder.Entity<CobrancaPix>()
                 .Property(a => a.IdCobranca);

            modelBuilder.Entity<CobrancaPix>()
                 .Property(a => a.LinkPagamento);

            modelBuilder.Entity<CobrancaPix>()
                 .Property(a => a.Valor);

            modelBuilder.Entity<CobrancaPix>()
                 .Property(a => a.Identificacao)
                 .HasMaxLength(14);

            modelBuilder.Entity<CobrancaPix>()
                 .Property(a => a.NomeDevedor)
                 .HasMaxLength(50);

            modelBuilder.Entity<CobrancaPix>()
                 .Property(a => a.DataCriacao);

            //Token
            modelBuilder.Entity<TokenResponse>()
                 .ToTable("Token")
                 .Property(a => a.Id);

            modelBuilder.Entity<TokenResponse>()
                 .Property(a => a.access_token);

            modelBuilder.Entity<TokenResponse>()
                 .Property(a => a.expires_in);

            modelBuilder.Entity<TokenResponse>()
                 .Property(a => a.token_type);
        }
    }
}
