using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Data.Discord
{
    public static class DiscordText
    {
        public static string RedLine(string oldString) =>        
             new string($"```diff\r\n{oldString}\r\n```");

        public static string GreenLine(string oldString) =>
             new string($"```diff\r\n+{oldString}\r\n```");

        public static string Italic(string oldString) =>
             new string($"*{oldString}*");

        public static string Bold(string oldString) =>
             new string($"**{oldString}**");

        public static string BoldItalic(string oldString) =>
             new string($"***{oldString}***");

        public static string UnderLine(string oldString) =>
             new string($"__{oldString}__");

        public static string BoldUnderLine(string oldString) =>
             new string($"**__{oldString}__**");

        public static string SingleLineCode(string oldString) =>
             new string($"`{oldString}`");

        public static string MultyLineCode(string oldString) =>
             new string($"```\r\n{oldString}\r\n```");

        public static string Tag(string oldString) =>
             new string($"<@{oldString}>");
    }
}
