using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Model.Coinmarketcap
{
    public class Listing
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("data")]
        public List<Data> DataList { get; set; }
    }
}
