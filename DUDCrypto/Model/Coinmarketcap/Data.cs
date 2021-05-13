using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Model.Coinmarketcap
{
    public class Data
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("is_active")]
        public bool IsAcitve { get; set; }

        [JsonProperty("is_fiat")]
        public bool IsFiat { get; set; }

        [JsonProperty("circulating_supply")]
        public ulong? CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public ulong? TotalSupply { get; set; }

        [JsonProperty("max_supply")]        
        public ulong? MaxSupply { get; set; }

        [JsonProperty("date_added")]
        public DateTime DateAdded { get; set; }

        [JsonProperty("num_market_pairs")]
        public int NumMarketPairs { get; set; }

        [JsonProperty("cmc_rank")]
        public int? Rank { get; set; }

        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("platform")]
        public Platform Platform { get; set; }

        [JsonProperty("quote")]
        public Dictionary<string, Quote> Quote { get; set; }
    }
}
