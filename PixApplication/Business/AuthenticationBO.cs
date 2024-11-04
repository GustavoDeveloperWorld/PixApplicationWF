using Newtonsoft.Json;
using PixApplication.Entity;
using PixApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PixApplication.Business
{
    public class AuthenticationBO
    {
        public async Task<Authentication> ObterAutenticacaoAsync()
        {
            using (var context = new AppDbContext())
            {
                // Obtém a primeira chave de autenticação disponível
                var authentication = await context.Authentications.FirstOrDefaultAsync();
                return authentication;
            }
        }

        public async Task<bool> TentarAutenticacaoAsync(Authentication auth)
        {
            var url = "https://oauth.sandbox.bb.com.br/oauth/token"; // URL do endpoint de autenticação
            var client = new HttpClient();

            // Cria o objeto de autenticação
            // Adicionando cabeçalhos
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var parametros = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", auth.ClientID },
                { "client_secret", auth.ClientSecret }
            };

            try
            {
                var content = new FormUrlEncodedContent(parametros);

                // Enviar a solicitação POST
                var response = await client.PostAsync(url, content);

                // Verifica a resposta
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    // Processar o token aqui se necessário
                    return true; // Autenticação bem-sucedida
                }
                else
                {
                    var errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erro na autenticação: {response.StatusCode} - {errorData}");
                    return false; // Autenticação falhou
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return false; // Em caso de erro, retornar false
            }
        }
    }
}
