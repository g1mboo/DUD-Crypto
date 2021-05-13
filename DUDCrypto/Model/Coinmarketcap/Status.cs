using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Model.Coinmarketcap
{
    public class Status
    {
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
        
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
        
        [JsonProperty("elapsed")]
        public int Elapsed { get; set; }
        
        [JsonProperty("credit_count")]
        public int CreditCount { get; set; }
    }
}
