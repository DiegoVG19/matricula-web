using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace sidiWeb.Code.Api
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;


        public ApiService()
        {
            _httpClient = new HttpClient();

            _httpClient.Timeout = TimeSpan.FromSeconds(10);
        }


        public AlumnoApiResponse ObtenerEstadoAlumno(string dni)
        {
            string urlBase = ConfigurationManager.AppSettings["ApiIntranetUrl"];
            string dniAdministrador = ConfigurationManager.AppSettings["X-Client-Dni"];

            string url = urlBase + dni;


            System.Diagnostics.Debug.WriteLine("URL API: " + url);
            System.Diagnostics.Debug.WriteLine("Header: " + dniAdministrador);


            var request = (System.Net.HttpWebRequest)
                System.Net.WebRequest.Create(url);


            request.Method = "GET";
            request.Timeout = 10000;

            request.Headers.Add(
                "X-Client-Dni",
                dniAdministrador
            );

            request.Accept = "application/json";


            System.Diagnostics.Debug.WriteLine("Antes GetResponse");


            using (var respuesta = request.GetResponse())
            {
                System.Diagnostics.Debug.WriteLine("API RESPONDIÓ");


                using (var reader = new System.IO.StreamReader(
                    respuesta.GetResponseStream()))
                {
                    string contenido = reader.ReadToEnd();


                    System.Diagnostics.Debug.WriteLine(
                        "RESPUESTA API:"
                    );

                    System.Diagnostics.Debug.WriteLine(contenido);


                    return JsonConvert.DeserializeObject<AlumnoApiResponse>(
                        contenido
                    );
                }
            }
        }
    }
}