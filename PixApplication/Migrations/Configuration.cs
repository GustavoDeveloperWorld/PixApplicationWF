namespace PixApplication.Migrations
{
    using PixApplication.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PixApplication.Entity.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PixApplication.Entity.AppDbContext context)
        {
            // Adicionando dados iniciais à tabela Authentication
            context.Authentications.AddOrUpdate(
                a => a.ClientID, // Chave para evitar duplicatas
                new Authentication { ClientID = "client1", ClientSecret = "secret1", ApplicationKey = "key1" },
                new Authentication { ClientID = "client2", ClientSecret = "secret2", ApplicationKey = "key2" }
            );

            // Adicionando dados iniciais à tabela ConfigPix
            context.ConfigPixes.AddOrUpdate(
                c => c.ChavePix, // Chave para evitar duplicatas
                new ConfigPix { ChavePix = "pixkey1", Identificacao = "id1" },
                new ConfigPix { ChavePix = "pixkey2", Identificacao = "id2" }
            );;

            // Adicionando dados iniciais à tabela ConfigPix
            context.Cobrancas.AddOrUpdate(
                c => c.IdCobranca, // Chave para evitar duplicatas
                new CobrancaPix { IdCobranca= "idcobranca1",  LinkPagamento = "link1", Identificacao = "identificacao1", NomeDevedor = "nomedevedor1"},
                new CobrancaPix { IdCobranca = "idcobranca2", LinkPagamento = "link2", Identificacao = "identificacao2", NomeDevedor = "nomedevedor2"}
            );

            context.SaveChanges(); // Salva as alterações
        }
    }
}
