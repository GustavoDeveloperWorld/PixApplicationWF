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
using System.Windows.Forms;
using static QRCoder.PayloadGenerator.WiFi;

namespace PixApplication.Business
{
    public class AuthenticationBO
    {
        public async Task<Model.Authentication> ObterAutenticacaoAsync()
        {
            using (var context = new AppDbContext())
            {
                // Obtém a primeira chave de autenticação disponível
                var authentication = await context.Authentications.FirstOrDefaultAsync();
                return authentication;
            }
        }
        public async Task<bool> TentarAutenticacaoAsync(Model.Authentication auth)
        {
            var url = "https://oauth.sandbox.bb.com.br/oauth/token";
            var client = new HttpClient();

            // Cabeçalhos padrão
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Cabeçalho Authorization com autenticação básica
            var authValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{auth.ClientID}:{auth.ClientSecret}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);

            // Parâmetros da requisição
            var parametros = new Dictionary<string, string>
    {
        { "grant_type", "client_credentials" },
        { "scope", "cob.write cob.read cobv.write cobv.read lotecobv.write lotecobv.read pix.write pix.read webhook.read webhook.write paylodlocation.write payloadlocation.read" } // Defina o escopo necessário aqui
    };

            try
            {
                var content = new FormUrlEncodedContent(parametros);
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseData);
                    Console.WriteLine($"Token de acesso: {tokenResponse.access_token}");

                    var tokenToStore = new TokenResponse
                    {
                        access_token = tokenResponse.access_token,
                        token_type = tokenResponse.token_type,
                        expires_in = tokenResponse.expires_in 
                    };

                    // Salvar o token no banco de dados
                    using (var context = new AppDbContext())
                    {
                        var tokenExistente = context.TokenResponses.FirstOrDefault();

                        if (tokenExistente != null)
                        {
                            // Se o token existir, atualiza o valor do token
                            tokenExistente.access_token = tokenToStore.access_token;
                            tokenExistente.expires_in = tokenToStore.expires_in;
                            context.SaveChanges();
                        }
                        else
                        {
                            // Se não houver nenhum token, adiciona um novo
                            context.TokenResponses.Add(tokenToStore);
                            context.SaveChanges();
                        }
                    }

                    return true;
                }
                else
                {
                    var errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erro na autenticação: {response.StatusCode} - {errorData}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return false;
            }
        }

    }
}
