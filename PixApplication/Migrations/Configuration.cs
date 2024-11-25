namespace PixApplication.Migrations
{
    using PixApplication.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Windows.Documents;

    internal sealed class Configuration : DbMigrationsConfiguration<PixApplication.Entity.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
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
            );

            // Adicionando dados iniciais à tabela ConfigPix
            context.Cobrancas.AddOrUpdate(
                c => c.IdCobranca, // Chave para evitar duplicatas
                new CobrancaPix { IdCobranca= "idcobranca1",  LinkPagamento = "link1", Identificacao = "identificacao1", NomeDevedor = "nomedevedor1"},
                new CobrancaPix { IdCobranca = "idcobranca2", LinkPagamento = "link2", Identificacao = "identificacao2", NomeDevedor = "nomedevedor2"}
            );

            // Adicionando dados iniciais à tabela ConfigPix
            context.TokenResponses.AddOrUpdate(
                c => c.Id, // Chave para evitar duplicatas
                new Model.TokenResponse { Id = 1, access_token = "access1", token_type = "type1", expires_in = 3600.0 }, // Exemplo de expires_in com valor double
                new Model.TokenResponse { Id = 2, access_token = "access2", token_type = "type2", expires_in = 7200.0 }  // Exemplo de expires_in com valor double
            );

            context.SaveChanges();
        }
    }
}
