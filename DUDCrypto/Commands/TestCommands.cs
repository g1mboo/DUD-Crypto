using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DUDCrypto.Commands
{
    public class TestCommands : BaseCommandModule
    {
        [Command("test")]
        [Description("Test")]
        public async Task Test(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    IconUrl = "https://cdn.arstechnica.net/wp-content/uploads/2016/02/5718897981_10faa45ac3_b-640x624.jpg",
                    Name = $"#1 - Test (TEST)"
                },

                Timestamp = DateTime.Now,

                Title = $"**Price __$000,000.00__**"
            };

            embed.Color = DiscordColor.Gray;

            embed.AddField($"Change***(1h)***", "```diff\r\n+000.00%\r\n```", true);
            embed.AddField($"Change***(24h)***", "```diff\r\n000.00%\r\n```", true);
            embed.AddField($"Change***(7d)***", "```diff\r\n-000.00%\r\n```", true);
            embed.AddField($"Market cap", "**$0,000,000,000**", true);
            embed.AddField($"⠀", "⠀", true);
            embed.AddField($"Volume***(24h)***", "**$0,000,000,000**", true);
            embed.AddField($"Circulating Supply", "`000,000,000 XXX`", true);
            embed.AddField($"Total Supply", "`000,000,000 XXX`", true);
            embed.AddField($"Max Supply", "`000,000,000 XXX`", true);

            await ctx.Channel.SendMessageAsync(embed: embed);

        }


        [Command("test2")]
        [Description("Test")]
        public async Task Test2(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    IconUrl = "https://i.ibb.co/wQ3JJ7c/podium.png",
                    Url = "https://coinmarketcap.com/",
                    Name = "CoinMarketCap"
                },


                Color = DiscordColor.Gray,

                Timestamp = DateTime.Now,

                Title = $"Top 10 cryptocurrecies by Market Cap"
            };

            string z = "⠀\r\n";
            string k = "⠀\r\n";
            string j = "⠀\r\n";

            for (int i = 0; i < 10; i++)
            {
                z += $"{i + 1} - Bitcoin BTC\r\n\r\n";
                k += $"$61,000.00\r\n\r\n";
                j += $"$1,000,000,000,000\r\n\r\n";
            }

            embed.AddField($"**# - Name**", z, true);
            embed.AddField($"**Price (USD)**", k, true);
            embed.AddField($"**Market Cap (USD)**", j, true);

            //for (int i = 0; i < 8; i++)
            //{
            //    if (i == 0)
            //    {
            //        embed.AddField($"**__# - Name__**", $"**{i + 1} - Bitcoin BTC**", true);
            //        embed.AddField($"**__Price (USD/BTC)__**", "**$61,000.00**", true);
            //        embed.AddField($"**__Market Cap (USD)__**", "**$1,000,000,000,000**", true);
            //    }
            //    else
            //    {
            //        embed.AddField($"**{i + 1} Bitcoin BTC**", $"**{i + 1} Bitcoin BTC**", true);
            //        embed.AddField($"**$61,000.00**", "**$61,000.00**", true);
            //        embed.AddField($"**$1,000,000,000,000**", "**$1,000,000,000,000**", true);
            //    }
            //}
            //embed.AddField($"Change(24h) ", "100.00%", true);
            //embed.AddField($"Change(7d) ", "100.00%", true);
            //embed.AddField($"Volume(24h) ", "100,000,000,000", true);
            //embed.AddField($"Circulating supply ", "18,000,000,000", true);


            await ctx.Channel.SendMessageAsync(embed: embed);

        }


        [Command("test3")]
        [Description("Test")]
        public async Task Test3(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    IconUrl = "https://cdn.arstechnica.net/wp-content/uploads/2016/02/5718897981_10faa45ac3_b-640x624.jpg",
                    Name = "Testtesttest"
                },

                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                {
                    Url = "https://cdn.arstechnica.net/wp-content/uploads/2016/02/5718897981_10faa45ac3_b-640x624.jpg",
                    Height = 100,
                    Width = 100
                },

                Title = "Prices for N XXX",

                Color = DiscordColor.MidnightBlue,
                Timestamp = DateTime.Now,
            };

            embed.AddField($"**TEST**", "**`0000000 XXX`**", true);
            embed.AddField($"**TEST**", "**`0000000 XXX`**", true);
            embed.AddField($"**TEST**", "**`0000000 XXX`**", true);

            await ctx.Channel.SendMessageAsync(embed: embed);
        }


        [Command("test4")]
        [Description("Test")]
        public async Task Test4(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    IconUrl = "https://cdn.arstechnica.net/wp-content/uploads/2016/02/5718897981_10faa45ac3_b-640x624.jpg",
                    Name = "Comparison"
                },

                //Title = "Prices for N XXX",

                Color = DiscordColor.MidnightBlue,
                Timestamp = DateTime.Now,
            };


            string test = "**```diff\r\n#1 - Stelar (XML)\r\n```" +
                "```diff\r\n#2 - Stelar (XML)\r\n```" +
                "```diff\r\n#3 - Stelar (XML)\r\n```" +
                "```diff\r\n#4 - Stelar (XML)\r\n```**";

            string test2 = "```diff\r\n+100%\r\n```" +
                "```diff\r\n+100%\r\n```" +
                "```diff\r\n-100%\r\n```" +
                "```diff\r\n-100%\r\n```";

            string test3 = "```diff\r\n$100,000\r\n```" +
                "```diff\r\n$100,000\r\n```" +
                "```diff\r\n$100,000\r\n```" +
                "```diff\r\n$100,000\r\n```";

            embed.AddField($"**# - Name (Symbol)**", test, true);
            embed.AddField($"**Change24h**", test2, true);
            embed.AddField($"**Price**", test3, true);

            //embed.AddField($"**Name**", "\r\n```diff\r\nStelar (XML)\r\n```\r\n⠀", true);
            //embed.AddField($"**Change24h**", "\r\n```diff\r\n+100%\r\n```\r\n⠀", true);
            //embed.AddField($"**Price**", "\r\n```diff\r\n$100,000,000\r\n```\r\n⠀", true);



            await ctx.Channel.SendMessageAsync(embed: embed);
        }
    }
}
