using NUnit.Framework;

namespace DUDCrypto.Tests.Data
{
    [TestFixture]
    public class DiscordAuthorTests
    {        
        [Test]
        public void DiscordAuthorReturnNickname()
        {
            string nickname = DUDCrypto.Data.Discord.Author.Nickname(null);
            Assert.AreEqual(nickname, "UNKNOWN");
        }
    }
}