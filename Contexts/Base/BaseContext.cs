using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TreinoSport.Extensions;

namespace TreinoSport.Contexts.Base {
    public abstract class BaseContext {

        private static HttpClient httpClient;
        public static string urlAndroidAPI;
        private static IConfiguration _configuration;

        public static void StartContext(IConfiguration configuration) {
            _configuration = configuration;
            urlAndroidAPI = _configuration.GetConnectionString("treinoSportApi");
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");
        }

        public static async Task<HttpResponseMessage> HttpResquest(HttpMethod verbo, string urlApi, string endpoint, Dictionary<string, object> queryParams = null, object body = null) {
            var api = await CheckAPI();
            if (api.StatusCode != HttpStatusCode.OK) {
                throw new Exception("Falha na conexão com a API");
            }

            HttpRequestMessage message = new(verbo, urlApi + endpoint + ParamsToString(queryParams));

            message.Content = body is null ? null : JsonContent.Create(body);

            HttpResponseMessage response = await httpClient.SendAsync(message);

            return response;
        }

        public static async Task<HttpResponseMessage> CheckAPI() {
            try {
                HttpRequestMessage message = new(HttpMethod.Get, urlAndroidAPI + "/check");


                var task = httpClient.SendAsync(message);

                var response = await task.TimeoutAfter(4000);

                return response;
            }
            catch (Exception e) {

                throw new Exception(e.Message);
            }

        }

        public static HttpClientHandler GetInsecureHandler() {
            HttpClientHandler handler = new();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public static string ParamsToString(Dictionary<string, object> queryParams) {
            if (queryParams == null || queryParams.Count == 0) {
                return "";
            }
            string stringParams = "?";
            foreach (var param in queryParams) {
                if (param.Value != null && param.Value != "") {

                    if (param.Value is IList) {
                        foreach (object item in param.Value as IList) {
                            stringParams += $"{param.Key}={item}&";
                        }
                    }
                    else {
                        stringParams += param.Key + "=" + param.Value + "&";
                    }
                }
            }
            return stringParams;
        }
    }
}
