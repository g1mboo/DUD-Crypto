using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Tests.Model.CMC
{
    [TestFixture]
    public class CoinmarketcapAPI_Tests
    {
        public DUDCrypto.Model.Coinmarketcap.API.CoinmarketcapAPI _api;
        [SetUp]
        public void SetUp()
        {
            _api = new DUDCrypto.Model.Coinmarketcap.API.CoinmarketcapAPI();
        }


        [Test]
        public void QuotesLatestReturnJsonString()
        {
            string json = _api.QuotesLatest("btc");

            Assert.IsNotNull(json);
            Assert.IsNotEmpty(json);
            Assert.IsTrue(json.StartsWith("{\"status\":{"));
            Assert.IsTrue(json.Contains("\"id\":1,"));
            Assert.IsTrue(json.Contains("\"name\":\"Bitcoin\","));
            Assert.IsTrue(json.Contains("\"symbol\":\"BTC\","));
            Assert.IsTrue(json.Contains("\"slug\":\"bitcoin\","));
            Assert.IsTrue(json.Contains("quote"));
            Assert.IsTrue(json.Contains("USD"));
            Assert.IsTrue(json.Contains("\"price\":"));
            Assert.IsTrue(json.EndsWith("}"));
        }

        [Test]
        public void ListingsLatestReturnJsonString()
        {
            string json = _api.ListingsLatest(1,10);

            Assert.IsNotNull(json);
            Assert.IsNotEmpty(json);            
            Assert.IsTrue(json.StartsWith("{\"status\":{"));
            Assert.IsTrue(json.Contains("\"name\":\"Bitcoin\","));
            Assert.IsTrue(json.Contains("\"name\":\"Ethereum\","));            
            Assert.IsTrue(json.Contains("\"symbol\":\"BTC\","));
            Assert.IsTrue(json.Contains("\"symbol\":\"ETH\","));
            Assert.IsTrue(json.Contains("\"symbol\":\"BNB\","));
            Assert.IsTrue(json.Contains("\"slug\":\"bitcoin\","));
            Assert.IsTrue(json.Contains("\"slug\":\"ethereum\","));
            Assert.IsTrue(json.Contains("quote"));
            Assert.IsTrue(json.Contains("USD"));
            Assert.IsTrue(json.Contains("\"price\":"));
            Assert.IsTrue(json.EndsWith("}"));
        }

        [Test]
        public void PriceConversionReturnJsonString()
        {
            string json = _api.PriceConversion("BTC", 1, "BTC");

            Assert.IsNotNull(json);
            Assert.IsNotEmpty(json);
            Assert.IsTrue(json.StartsWith("{\"status\":{"));
            Assert.IsTrue(json.Contains("\"symbol\":\"BTC\","));
            Assert.IsTrue(json.Contains("\"id\":1,"));
            Assert.IsTrue(json.Contains("\"name\":\"Bitcoin\","));
            Assert.IsTrue(json.Contains("\"amount\":1"));
            Assert.IsTrue(json.Contains("\"quote\":{\"BTC\":{\"price\":1,"));
            Assert.IsTrue(json.EndsWith("}"));
        }
    }
}
