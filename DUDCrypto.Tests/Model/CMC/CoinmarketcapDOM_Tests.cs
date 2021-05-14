using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Tests.Model.CMC
{
    [TestFixture]
    public class CoinmarketcapDOM_Tests
    {
        public List<DUDCrypto.Model.Coinmarketcap.Earn> _list;
        [SetUp]
        public void SetUp()
        {
            DUDCrypto.Model.Coinmarketcap.API.CoinmarketcapDOM dom = new DUDCrypto.Model.Coinmarketcap.API.CoinmarketcapDOM();
            _list = dom.ReturnEarn();
        }


        [Test]
        public void ReturnEarnIsNotNull()
        {         
            Assert.IsNotNull(_list);
            Assert.IsNotNull(_list.FirstOrDefault());
            Assert.IsTrue(_list.Count > 1);   
        }

        [Test]
        public void ReturnEarnIsNotEmpty()
        {            
            foreach (var item in _list)
            {
                Assert.IsNotEmpty(item.Name);
                Assert.IsNotEmpty(item.Symbol);
                Assert.IsNotEmpty(item.Description);
                Assert.IsNotEmpty(item.IconUrl);
                Assert.IsNotEmpty(item.ImageUrl);
                Assert.IsNotEmpty(item.Url);
            }

        }
    }
}
