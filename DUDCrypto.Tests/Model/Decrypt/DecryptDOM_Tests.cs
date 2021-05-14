using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Tests.Model.Decrypt
{
    [TestFixture]
    public class DecryptDOM_Tests
    {
        public List<DUDCrypto.Model.Decrypt.News> _list;
        [SetUp]
        public void SetUp()
        {
            DUDCrypto.Model.Decrypt.API.DecryptDOM dom = new DUDCrypto.Model.Decrypt.API.DecryptDOM();
            _list = dom.ReturnNews();
        }


        [Test]
        public void ReturnNewsIsNotNull()
        {         
            Assert.IsNotNull(_list);
            Assert.IsNotNull(_list.FirstOrDefault());               
        }

        [Test]
        public void ReturnNewsCountIs12()
        {
            Assert.IsTrue(_list.Count > 1);
            Assert.IsTrue(_list.Count == 12);            
        }

        [Test]
        public void ReturnNewsIsNotEmpty()
        {            
            foreach (var item in _list)
            {
                Assert.IsNotEmpty(item.Author);
                Assert.IsNotEmpty(item.Category);
                Assert.IsNotEmpty(item.CategoryUrl);
                Assert.IsNotEmpty(item.DatePublished.ToString());
                Assert.IsNotEmpty(item.ImageUrl);
                Assert.IsNotEmpty(item.Summary);
                Assert.IsNotEmpty(item.Title);
                Assert.IsNotEmpty(item.Url);
            }

        }
    }
}
