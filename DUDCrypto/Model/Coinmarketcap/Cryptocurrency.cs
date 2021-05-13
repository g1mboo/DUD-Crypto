using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Model.Coinmarketcap
{    
    public class Cryptocurrency
    {
        [JsonProperty("status")]
        public Status Status { get; set; } 
        
        [JsonProperty("data")]
        public Dictionary<string, Data> Data { get; set; }
    }
}
