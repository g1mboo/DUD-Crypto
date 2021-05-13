using Converter;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DUDCrypto.Model.Coinmarketcap.API
{
    public class CoinmarketcapAPI
    {
        private Config _config;

        public CoinmarketcapAPI()
        {            
            var json = string.Empty;

            using (var fo = File.OpenRead("cmc-config.json"))
            using (var fr = new StreamReader(fo, new UTF8Encoding(false)))
                json = fr.ReadToEnd();

            _config = JsonConvert.DeserializeObject<Config>(json);
        }

        public string QuotesLatest(string symbol)
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);            
            queryString["symbol"] = symbol;
                        
            return DownloadJson(URL, queryString);
        }
        public string ListingsLatest(int start, int limit)
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = start.ToString();
            queryString["limit"] = limit.ToString();

            return DownloadJson(URL, queryString);
        }
        public string PriceConversion(string symbol, double amount, string convert)
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/tools/price-conversion");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = symbol.ToString();
            queryString["amount"] = amount.ToString();
            queryString["convert"] = convert;

            return DownloadJson(URL, queryString);
        }
        private string DownloadJson(UriBuilder uri, NameValueCollection queryString)
        {
            uri.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", _config.Token);
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(uri.ToString());
        }
    }
}
