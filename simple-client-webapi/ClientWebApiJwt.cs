using Newtonsoft.Json;
using SimpleClientWebApi.Enums;
using SimpleClientWebApi.Extends;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SimpleClientWebApi
{
    public class ClientWebApiJwt : Notifiable
    {
        private readonly string _keyJwt;
        private readonly string _uriWebApi;
        private readonly KeyTypeJwt _keyType;
        private readonly string _keyScheme;

        public ClientWebApiJwt(string keyJwt, KeyTypeJwt keyType = KeyTypeJwt.Bearer)
        {
            _keyJwt = keyJwt;
            _keyType = keyType;
        }

        public ClientWebApiJwt(string keyJwt, KeyTypeJwt keyType, string keyScheme = "")
        {
            _keyJwt = keyJwt;
            _keyType = keyType;
            _keyScheme = keyScheme;
        }

        public ClientWebApiJwt(string keyJwt, string uriWebApi, KeyTypeJwt tipoChave = KeyTypeJwt.Bearer)
        {
            _keyJwt = keyJwt;
            _uriWebApi = uriWebApi;
            _keyType = tipoChave;
        }

        public T1 Get<T1, T2>(T2 sendObject = default(T2), string route = "") where T1 : new() where T2 : new()
        {
            T1 retorno = new T1();
            var url = string.Empty;
            try
            {
                retorno = _returnResult<T1, T2>(sendObject, HttpMethod.Get, route);
            }
            catch (Exception ex)
            {
                AddNotification(string.Format("Erro send request get with object. Url: {0}. Error message: {1}", url, ex.Message), ex);
            }

            return retorno;
        }

        public T1 Post<T1, T2>(T2 sendObject = default(T2), string route = "") where T1 : new() where T2 : new()
        {
            T1 retorno = new T1();
            var url = string.Empty;
            try
            {
                retorno = _returnResult<T1, T2>(sendObject, HttpMethod.Post, route);
            }
            catch (Exception ex)
            {
                AddNotification(string.Format("Erro send request get with object. Url: {0}. Error message: {1}", url, ex.Message), ex);
            }

            return retorno;
        }

        public T1 Put<T1, T2>(T2 sendObject = default(T2), string route = "") where T1 : new() where T2 : new()
        {
            T1 retorno = new T1();
            var url = string.Empty;
            try
            {
                retorno = _returnResult<T1, T2>(sendObject, HttpMethod.Put, route);
            }
            catch (Exception ex)
            {
                AddNotification(string.Format("Erro send request get with object. Url: {0}. Error message: {1}", url, ex.Message), ex);
            }

            return retorno;
        }

        public T1 Delete<T1, T2>(T2 sendObject = default(T2), string route = "") where T1 : new() where T2 : new()
        {
            T1 retorno = new T1();
            var url = string.Empty;
            try
            {
                retorno = _returnResult<T1, T2>(sendObject, HttpMethod.Delete, route);
            }
            catch (Exception ex)
            {
                AddNotification(string.Format("Erro send request get with object. Url: {0}. Error message: {1}", url, ex.Message), ex);
            }

            return retorno;
        }

        private string _returnUrl(string route = "")
        {
            var retorno = string.IsNullOrEmpty(_uriWebApi) ? "" : _uriWebApi;

            if (!string.IsNullOrEmpty(route))
            {
                if (string.IsNullOrEmpty(_uriWebApi))
                {
                    retorno = string.Format("{0}", route);
                }
                else
                {
                    retorno = string.Format("{0}/{1}", _uriWebApi, route);
                }
            }

            return retorno;
        }

        private HttpClient _returnClient()
        {
            var retorno = new HttpClient();
            if (_keyType == KeyTypeJwt.Bearer)
            {
                retorno.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", _keyJwt);
            }
            else
            {
                retorno.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue(_keyScheme, _keyJwt);
            }

            return retorno;
        }

        private T1 _returnResult<T1, T2>(T2 sendObject, HttpMethod method, string route = "") where T1 : new() where T2 : new()
        {
            T1 retorno = new T1();
            string serializedObject = sendObject != null ? JsonConvert.SerializeObject(sendObject) : string.Empty;

            var client = _returnClient();
            var url = _returnUrl(route);

            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(url),
                Content = string.IsNullOrEmpty(serializedObject) ? null : new StringContent(serializedObject, Encoding.UTF8, "application/json"),
            };

            var reponse = client.SendAsync(request).Result;
            if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (reponse.Content != null)
                {
                    var resultadoTexto = reponse.Content.ReadAsStringAsync().Result;
                    retorno = JsonConvert.DeserializeObject<T1>(resultadoTexto);
                }
            }
            else
            {
                AddNotification(string.Format("Return status incorrect. Status return: {0}", reponse.StatusCode.ToString()), reponse.StatusCode);
            }

            return retorno;
        }
    }
}
