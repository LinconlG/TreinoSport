﻿using Microsoft.AspNetCore.Http;
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

        protected readonly HttpClient _httpClient;
        protected readonly string _treinoSportApiUrl;

        public BaseContext() {
            _httpClient = new HttpClient();
            _treinoSportApiUrl = "http://10.0.2.2:5050/api";
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
