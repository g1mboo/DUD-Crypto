using DUDCrypto.Data.Discord;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DUDCrypto.Model.Discord
{
    public class Prompt
    {
        public static async Task SendPromptAsync(DiscordChannel channel, DiscordUser user, string message)
        {
            await channel.SendMessageAsync($"{DiscordText.Tag(user.Id.ToString())} {message}");
        } 
    }
}
