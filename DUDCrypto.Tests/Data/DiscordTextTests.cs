using NUnit.Framework;

namespace DUDCrypto.Tests.Data
{
    [TestFixture]
    public class DiscordTextTests
    {
        private string _testLine = "Hello, World!";

        [Test]
        public void RedLineTest()
        {
            string redlineString = DUDCrypto.Data.Discord.DiscordText.RedLine(_testLine);

            Assert.IsTrue(redlineString.Contains(_testLine));
            Assert.AreNotEqual(redlineString, _testLine);
            Assert.AreEqual(redlineString, $"```diff\r\n{_testLine}\r\n```");
        }

        [Test]
        public void GreenLineTest()
        {
            string greenLineString = DUDCrypto.Data.Discord.DiscordText.GreenLine(_testLine);

            Assert.IsTrue(greenLineString.Contains(_testLine));
            Assert.AreNotEqual(greenLineString, _testLine);
            Assert.AreEqual(greenLineString, $"```diff\r\n+{_testLine}\r\n```");
        }

        [Test]
        public void ItalicTest()
        {
            string italicString = DUDCrypto.Data.Discord.DiscordText.Italic(_testLine);

            Assert.IsTrue(italicString.Contains(_testLine));
            Assert.AreNotEqual(italicString, _testLine);
            Assert.AreEqual(italicString, $"*{_testLine}*");
        }

        [Test]
        public void BoldTest()
        {
            string boldString = DUDCrypto.Data.Discord.DiscordText.Bold(_testLine);

            Assert.IsTrue(boldString.Contains(_testLine));
            Assert.AreNotEqual(boldString, _testLine);
            Assert.AreEqual(boldString, $"**{_testLine}**");
        }

        [Test]
        public void BoldItalicTest()
        {
            string boldItalicString = DUDCrypto.Data.Discord.DiscordText.BoldItalic(_testLine);

            Assert.IsTrue(boldItalicString.Contains(_testLine));
            Assert.AreNotEqual(boldItalicString, _testLine);
            Assert.AreEqual(boldItalicString, $"***{_testLine}***");
        }

        [Test]
        public void UnderLineTest()
        {
            string underLineString = DUDCrypto.Data.Discord.DiscordText.UnderLine(_testLine);

            Assert.IsTrue(underLineString.Contains(_testLine));
            Assert.AreNotEqual(underLineString, _testLine);
            Assert.AreEqual(underLineString, $"__{_testLine}__");
        }

        [Test]
        public void BoldUnderLineTest()
        {
            string boldUnderLineString = DUDCrypto.Data.Discord.DiscordText.BoldUnderLine(_testLine);

            Assert.IsTrue(boldUnderLineString.Contains(_testLine));
            Assert.AreNotEqual(boldUnderLineString, _testLine);
            Assert.AreEqual(boldUnderLineString, $"**__{_testLine}__**");
        }

        [Test]
        public void SingleLineCodeTest()
        {
            string singleLineCodeString = DUDCrypto.Data.Discord.DiscordText.SingleLineCode(_testLine);

            Assert.IsTrue(singleLineCodeString.Contains(_testLine));
            Assert.AreNotEqual(singleLineCodeString, _testLine);
            Assert.AreEqual(singleLineCodeString, $"`{_testLine}`");
        }

        [Test]
        public void MultyLineCodeTest()
        {
            string MultyLineCodeString = DUDCrypto.Data.Discord.DiscordText.MultyLineCode(_testLine);

            Assert.IsTrue(MultyLineCodeString.Contains(_testLine));
            Assert.AreNotEqual(MultyLineCodeString, _testLine);
            Assert.AreEqual(MultyLineCodeString, $"```\r\n{_testLine}\r\n```");
        }

        [Test]
        public void TagTest()
        {
            string tagString = DUDCrypto.Data.Discord.DiscordText.Tag(_testLine);

            Assert.IsTrue(tagString.Contains(_testLine));
            Assert.AreNotEqual(tagString, _testLine);
            Assert.AreEqual(tagString, $"<@{_testLine}>");
        }
    }
}