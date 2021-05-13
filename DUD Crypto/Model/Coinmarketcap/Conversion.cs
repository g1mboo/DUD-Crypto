using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Model.Coinmarketcap
{
    public class Conversion
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
