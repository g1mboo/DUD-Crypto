using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Model.Coinmarketcap
{
    public class Earn
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Url { get; set; }
        public string IconUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool Live { get; set; }

        public Earn(string name, string symbol, string url, string iconUrl, string imageUrl, string description, bool liveStatus)
        {
            Name = name;
            Symbol = symbol;
            Url = url;
            IconUrl = iconUrl;
            ImageUrl = imageUrl;
            Description = description;
            Live = liveStatus;
        }
    }
}
