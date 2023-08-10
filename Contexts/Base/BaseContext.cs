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
    public abstract class BaseContext {

        protected readonly HttpClient _httpClient;
        protected readonly IConfiguration _configuration;
        protected readonly string _treinoSportApiUrl;

        public BaseContext(IConfiguration configuration) {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _treinoSportApiUrl = _configuration.GetConnectionString("treinoSportApi");
        }
    }
}
