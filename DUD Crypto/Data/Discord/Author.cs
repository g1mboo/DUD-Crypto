using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Data.Discord
{
    public static class Author
    {
        public static string GetNickname(DiscordUser author)
        {
            string authorString = author.ToString().Replace('_', ' ');
            string nickname = string.Empty;
            for (int i = authorString.LastIndexOf('(') + 1; i < authorString.LastIndexOf(')'); i++)
                nickname += authorString[i];
            return nickname;
        }
    }
}
