using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceClientApi
{
    public class Notifiable
    {
        private readonly Dictionary<string, Object> listNotifiable;

        public Notifiable()
        {
            listNotifiable = new Dictionary<string, Object>();
        }

        public bool IsValid()
        {
            return listNotifiable == null || !listNotifiable.Any();
        }

        protected void AddNotification<T>(string chave, T value)
        {
            listNotifiable.Add(chave, value);
        }

        public string NotificationsMessage()
        {
            var retorno = string.Empty;
            if(listNotifiable != null && listNotifiable.Any())
            {
                retorno = string.Join(";", listNotifiable.Select(s => s.Key).ToArray());
            }
            return retorno;
        }

        public string FullNotificationsMessage()
        {
            var retorno = string.Empty;
            if (listNotifiable != null && listNotifiable.Any())
            {
                retorno = string.Join(";", listNotifiable.Select(s => string.Join("|", s.Key, JsonConvert.SerializeObject(s.Value))).ToArray());
            }
            return retorno;
        }

        public string[] Notifications()
        {
            var retorno = new string[] { };
            if (listNotifiable != null && listNotifiable.Any())
            {
                retorno = listNotifiable.Select(s => s.Key).ToArray();
            }
            return retorno;
        }

        public string[] FullNotifications()
        {
            var retorno = new string[] { };
            if (listNotifiable != null && listNotifiable.Any())
            {
                retorno = listNotifiable.Select(s => string.Join("|", s.Key, JsonConvert.SerializeObject(s.Value))).ToArray();
            }
            return retorno;
        }
    }
}
