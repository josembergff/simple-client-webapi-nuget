

using Newtonsoft.Json;
using prmToolkit.NotificationPattern;
using simple_client_webapi.Enums;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace simple_client_webapi
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

        public T Get<T>(string route = "") where T : new()
        {
            T retorno = new T();
            var url = string.Empty;
            try
            {
                var client = _returnClient();
                url = _returnUrl(route);

                

                var result = client.GetAsync(url).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (result.Content != null)
                    {
                        var resultadoTexto = result.Content.ReadAsStringAsync().Result;
                        retorno = JsonConvert.DeserializeObject<T>(resultadoTexto);
                    }
                }
                else
                {
                    AddNotification("ReturnStatus", string.Format("Return status incorrect. Status return: {0}", result.StatusCode.ToString()), result.StatusCode);
                }
            }
            catch (Exception ex)
            {
                AddNotification("GetError", string.Format("Erro send request get. Url: {0}. Error message: {1}", url, ex.Message), ex);
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


    }
}
