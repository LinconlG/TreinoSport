using Java.Util.Zip;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TreinoSport.Extensions;
using TreinoSport.Models;

namespace TreinoSport.Contexts.Base {
    public static class HttpUtilities {

        internal static HttpRequestMessage Get(string path, Dictionary<string, object> queryParams = null) {
            return new HttpRequestMessage() {
                RequestUri = new Uri(path + ParamsToString(queryParams)),
                Method = HttpMethod.Get,
            };
        }

        internal static HttpRequestMessage Get(string path, string token, Dictionary<string, object> queryParams = null) {
            return new HttpRequestMessage() {
                RequestUri = new Uri(path + ParamsToString(queryParams)),
                Headers = {
                    { HttpRequestHeader.Authorization.ToString(), "Bearer " + token }
                },
                Method = HttpMethod.Get
            };
        }

        internal static HttpRequestMessage GetWithBody(string path, string content, Dictionary<string, object> queryParams = null) {
            return new HttpRequestMessage() {
                RequestUri = new Uri(path + ParamsToString(queryParams)),
                Method = HttpMethod.Get,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }

        internal static HttpRequestMessage Post(string path, string content, Dictionary<string, object> queryParams = null) {
            return new HttpRequestMessage() {
                RequestUri = new Uri(path + ParamsToString(queryParams)),
                Method = HttpMethod.Post,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }

        internal static HttpRequestMessage Post(string path, string content, string token, Dictionary<string, object> queryParams = null) {
            return new HttpRequestMessage() {
                RequestUri = new Uri(path + ParamsToString(queryParams)),
                Headers = {
                    { HttpRequestHeader.Authorization.ToString(), "Bearer " + token }
                },
                Method = HttpMethod.Post,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }

        internal static HttpRequestMessage Put(string path, string content, Dictionary<string, object> queryParams = null) {
            return new HttpRequestMessage() {
                RequestUri = new Uri(path + ParamsToString(queryParams)),
                Method = HttpMethod.Put,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }

        internal static HttpRequestMessage Put(string path, string content, string token, Dictionary<string, object> queryParams = null) {
            return new HttpRequestMessage() {
                RequestUri = new Uri(path + ParamsToString(queryParams)),
                Headers = {
                    { HttpRequestHeader.Authorization.ToString(), "Bearer " + token }
                },
                Method = HttpMethod.Put,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }

        internal static HttpRequestMessage Delete(string path, string content, Dictionary<string, object> queryParams = null) {
            return new HttpRequestMessage() {
                RequestUri = new Uri(path + ParamsToString(queryParams)),
                Method = HttpMethod.Delete,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }
        internal static HttpRequestMessage Delete(string path, string content, string token, Dictionary<string, object> queryParams = null) {
            return new HttpRequestMessage() {
                RequestUri = new Uri(path + ParamsToString(queryParams)),
                Headers = {
                    { HttpRequestHeader.Authorization.ToString(), "Bearer " + token }
                },
                Method = HttpMethod.Delete,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }

        internal static HttpRequestMessage Patch(string path, string content, Dictionary<string, object> queryParams = null) {
            return new HttpRequestMessage() {
                RequestUri = new Uri(path + ParamsToString(queryParams)),
                Method = HttpMethod.Patch,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }
        internal static HttpRequestMessage Patch(string path, string content, string token, Dictionary<string, object> queryParams = null) {
            return new HttpRequestMessage() {
                RequestUri = new Uri(path + ParamsToString(queryParams)),
                Headers = {
                    { HttpRequestHeader.Authorization.ToString(), "Bearer " + token }
                },
                Method = HttpMethod.Patch,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }

        internal static HttpRequestMessage PatchWithFile(string path, IFormFile file, string formDataField, Dictionary<string, object> queryParams = null) {
            var fileContent = new StreamContent(file.OpenReadStream());
            return new HttpRequestMessage() {
                RequestUri = new Uri(path + ParamsToString(queryParams)),
                Method = HttpMethod.Patch,
                Content = new MultipartFormDataContent { { fileContent, formDataField, file.FileName } }
            };
        }

        internal async static Task<Dictionary<string, object>> GetBody(this HttpResponseMessage response) {
            var rawBody = await response.Content.ReadAsStringAsync();
            if (rawBody == "") {
                return null;
            }
            return JsonSerializer.Deserialize<Dictionary<string, object>>(rawBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        internal async static Task<T> GetBody<T>(this HttpResponseMessage response) {
            var rawBody = await response.Content.ReadAsStringAsync();
            if (rawBody == "") {
                return default;
            }
            return JsonSerializer.Deserialize<T>(rawBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        internal async static Task<byte[]> GetByteArrayBody(this HttpResponseMessage response) {
            var rawBody = await response.Content.ReadAsByteArrayAsync();
            return rawBody;
        }

        private static string ParamsToString(Dictionary<string, object> queryParams) {
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

        internal static async Task HandleResponse(this HttpResponseMessage response) {

            if (response.StatusCode == HttpStatusCode.InternalServerError) {
                var apiError = await response.GetBody<ApiError>();
                throw new APIException(apiError.Message, apiError.IsPublicMessage);
            }

            if (response.StatusCode == HttpStatusCode.BadRequest) {
                var body = await response.GetBody();
                throw new ValidationException("Erros de validação encontrados", null, body);
            }

            response.EnsureSuccessStatusCode();

            switch (response.StatusCode) {
                case HttpStatusCode.BadRequest:
                    break;
                case HttpStatusCode.Created:
                    break;
                case HttpStatusCode.Forbidden:
                    break;
                case HttpStatusCode.InternalServerError:
                    break;
                case HttpStatusCode.MethodNotAllowed:
                    break;
                case HttpStatusCode.Moved:
                    break;
                case HttpStatusCode.NoContent:
                    break;
                case HttpStatusCode.NotFound:
                    break;
                case HttpStatusCode.NotImplemented:
                    break;
                case HttpStatusCode.OK:
                    break;
                case HttpStatusCode.RequestTimeout:
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    break;
                case HttpStatusCode.Unauthorized:
                    break;
                case HttpStatusCode.UnsupportedMediaType:
                    break;
                default:
                    break;
            }
        }
    }
}
