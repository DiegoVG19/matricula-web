using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Linq;

namespace sidiWeb.Code.Api
{
    public class ApiPagosService
    {
        public PagoApiResponse ObtenerPagos(string dni)
        {
            try
            {
                string urlBase = ConfigurationManager.AppSettings["ApiPagosUrl"];
                string apiKey = ConfigurationManager.AppSettings["x-api-key"];

                string url = $"{urlBase}clientes/{dni}/recibos";

                System.Diagnostics.Debug.WriteLine("URL API PAGOS: " + url);

                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";
                request.Timeout = 10000;
                request.Accept = "application/json";

                request.Headers.Add("x-api-key", apiKey);

                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        string contenido = reader.ReadToEnd();

                        System.Diagnostics.Debug.WriteLine("RESPUESTA API PAGOS:");
                        System.Diagnostics.Debug.WriteLine(contenido);

                        PagoApiResponse respuesta =
    JsonConvert.DeserializeObject<PagoApiResponse>(contenido);

                        int codigoOficina = int.Parse(
                            ConfigurationManager.AppSettings["CodigoOficinaIdiomas"]);

                        if (respuesta.data != null)
                        {
                            respuesta.data = respuesta.data
                                .Where(p => p.oficina != null &&
                                            p.oficina.codigo == codigoOficina)
                                .ToList();
                        }

                        return respuesta;


                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        string error = reader.ReadToEnd();
                        System.Diagnostics.Debug.WriteLine("ERROR API PAGOS:");
                        System.Diagnostics.Debug.WriteLine(error);
                    }
                }

                throw;
            }
        }
    }
}