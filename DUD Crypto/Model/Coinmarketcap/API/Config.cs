using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Model.Coinmarketcap.API
{
    public struct Config
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
    }
}
