using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Model.Coinmarketcap
{
    public class Quote
    {
        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("volume_24h")]
        public ulong Volume24h { get; set; }
        
        [JsonProperty("percent_change_1h")]
        public double Change1h { get; set; }
        
        [JsonProperty("percent_change_24h")]
        public double Change24h { get; set; }
        
        [JsonProperty("percent_change_7d")]
        public double Change7d { get; set; }

        [JsonProperty("percent_change_30d")]
        public double Change30d { get; set; }

        [JsonProperty("percent_change_60d")]
        public double? Change60d { get; set; }

        [JsonProperty("percent_change_90d")]
        public double? Change90d { get; set; }

        [JsonProperty("market_cap")]
        public ulong MarketCap { get; set; }

        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}
