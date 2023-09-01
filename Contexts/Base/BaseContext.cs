using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TreinoSport.Contexts.Base {
    public class BaseContext {

        protected readonly HttpClient httpClient;
        protected readonly string _treinoSportApiUrl;
        protected readonly IConfiguration _configuration;

        public BaseContext(IConfiguration configuration) {
            _configuration = configuration;
            _treinoSportApiUrl = _configuration.GetConnectionString("treinoSportApi");
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");
        }

        public HttpClientHandler GetInsecureHandler() {
            HttpClientHandler handler = new();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public string ParamsToString(Dictionary<string, object> queryParams) {
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
