using ServiceClientApi;
using ServiceClientApi.Enums;

namespace simple_client_webapi
{
    public class ClientWebApiJwt : WebApiJwt
    {
        public ClientWebApiJwt(string keyJwt, KeyTypeJwt keyType = KeyTypeJwt.Bearer):base(keyJwt, keyType)
        {
        }

        public ClientWebApiJwt(string keyJwt, KeyTypeJwt keyType, string keyScheme = ""): base(keyJwt, keyType, keyScheme)
        {
        }

        public ClientWebApiJwt(string keyJwt, string uriWebApi, KeyTypeJwt tipoChave = KeyTypeJwt.Bearer):base (keyJwt, uriWebApi, tipoChave)
        {
        }
    }
}
