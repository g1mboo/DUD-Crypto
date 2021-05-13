using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Data.Discord
{
    public static class Author
    {
        public static string Nickname(DiscordUser author) => ParseNickname(author);        

        private static string ParseNickname(DiscordUser author)
        {
            string result = string.Empty;
            try
            {
                string authorString = author.ToString().Replace('_', ' ');
                string nickname = string.Empty;
                for (int i = authorString.LastIndexOf('(') + 1; i < authorString.LastIndexOf(')'); i++)
                    nickname += authorString[i];
                result = nickname;
            }
            catch(Exception)
            {
                result = "UNKNOWN";
            }            
            return result;            
        }
    }
}
