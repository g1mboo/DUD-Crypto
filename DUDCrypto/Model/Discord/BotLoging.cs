using DUDCrypto.Data.Discord;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DUDCrypto.Model.Discord
{
    public class BotLoging
    {
        
        public static void PrintError(DiscordClient sender, DiscordUser user, Exception exception)
        {
            EventId BotEventId = new EventId(000, "USER");
            sender.Logger.LogError(BotEventId, exception, Author.Nickname(user) + " throwing Exeption:\r\n" + exception.Message);
        }

        public static void PrintInfo(DiscordClient sender, string name, string info)
        {
            EventId BotEventId = new EventId(000, name);
            sender.Logger.LogInformation(BotEventId, info);
        }
    }
}
