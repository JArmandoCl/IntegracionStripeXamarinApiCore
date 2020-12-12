using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdvientoXamarinDemo.Models
{
    public class ClientHttp
    {
        private const string UrlWebApi = "http://192.168.0.16";
        public async Task<T> Get<T>(string url)
        {
            try
            {
                Uri enlace = new Uri(new Uri(UrlWebApi), url);
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(enlace);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    T objetoRecuperado = JsonConvert.DeserializeObject<T>(jsonResponse);
                    return objetoRecuperado;
                }
                throw new Exception($"Error al realizar la petición: {response.StatusCode} {response.Content}");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}
