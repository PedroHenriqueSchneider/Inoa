using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace InoaB3.Utils
{
    public static class HttpClientHelper
    {
        public static HttpClient GetClient(string baseAddress, string token)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Adiciona o token no header Authorization
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }
    }
}