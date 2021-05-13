using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Model.Discord
{
    public struct BotConfig
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
    }
}
