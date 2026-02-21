using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Mobile.Services
{
    class AssinaturaDocumentoService
    {
        private readonly IHttpClientFactory _factory;

        public AssinaturaDocumentoService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<bool> EnviarAssinaturasAsync(List<string> assinaturas, string nomeFicheiro)
        {
            var client = _factory.CreateClient("PRH.Server");

            var payload = new
            {
                NomeFicheiro = nomeFicheiro,
                Assinaturas = assinaturas
            };

            var response = await client.PostAsJsonAsync("/api/documento/assinaturas", payload);

            return response.IsSuccessStatusCode;
        }

    }
}
