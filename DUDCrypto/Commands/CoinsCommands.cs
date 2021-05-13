using DUDCrypto.Data;
using DUDCrypto.Model.Coinmarketcap;
using DUDCrypto.Model.Coinmarketcap.API;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter;
using DUDCrypto.Data.Discord;
using DUDCrypto.Model.Discord;

namespace DUDCrypto.Commands
{
    public class CoinsCommands : BaseCommandModule
    {
        [Command("crypto")]
        public async Task Crypto(CommandContext ctx) =>
           await Prompt.SendPromptAsync(ctx.Channel, ctx.User, $"Enter {DiscordText.SingleLineCode("+help crypto")} to see how the command works");


        [Command("crypto")]
        [Description(":coin: Shows detailed information about the cryptocurrency")]
        public async Task Crypto(CommandContext ctx,
            [Description("Abbreviation of a specific cryptocurrency")] string abbreviation)
        {
            var api = new CoinmarketcapAPI();
            try
            {
                string json = api.QuotesLatest(abbreviation);
                
                //Try to get currency
                var cryptocurrency = JsonConvert.DeserializeObject<Cryptocurrency>(json);

                var id = cryptocurrency.Data.First().Value.Id;
                var rank = cryptocurrency.Data.First().Value.Rank;
                var name = cryptocurrency.Data.First().Value.Name;
                var slug = cryptocurrency.Data.First().Value.Slug;
                var symbol = cryptocurrency.Data.First().Value.Symbol;
                var price = cryptocurrency.Data.First().Value.Quote["USD"].Price;
                var change1h = cryptocurrency.Data.First().Value.Quote["USD"].Change1h;
                var change24h = cryptocurrency.Data.First().Value.Quote["USD"].Change24h;
                var change7d = cryptocurrency.Data.First().Value.Quote["USD"].Change7d;
                var volume24h = cryptocurrency.Data.First().Value.Quote["USD"].Volume24h;
                var marketCap = cryptocurrency.Data.First().Value.Quote["USD"].MarketCap;
                var circulatingSupply = cryptocurrency.Data.First().Value.CirculatingSupply;
                var totalSupply = cryptocurrency.Data.First().Value.TotalSupply;
                var maxSupply = cryptocurrency.Data.First().Value.MaxSupply;
                var dateAdded = cryptocurrency.Data.First().Value.DateAdded;

                //Create Embed
                var embed = new DiscordEmbedBuilder()
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = $"https://s2.coinmarketcap.com/static/img/coins/64x64/{id}.png",
                        Name = $"#{rank} - {name} ({symbol})",
                        Url = $"https://coinmarketcap.com/currencies/{slug}/"
                    },

                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                    {
                        Url = $"https://s2.coinmarketcap.com/static/img/coins/64x64/{id}.png",
                        Height = 100,
                        Width = 100
                    },

                    Timestamp = cryptocurrency.Data.First().Value.LastUpdated,

                    Title = DiscordText.Bold($"Price {DiscordText.UnderLine($"${Numbers.EditToReadableNumber(price)}")}"),
                };
                //Price info
                embed.AddField(
                    $"Change{DiscordText.BoldItalic("(1h)")}",
                    change1h > 0 ? DiscordText.GreenLine($"{Numbers.EditPercent(change1h)}% ▲") : DiscordText.RedLine($"{Numbers.EditPercent(change1h)}% ▼"),
                    true);

                embed.AddField(
                    $"Change{DiscordText.BoldItalic("(24h)")}",
                    change24h > 0 ? DiscordText.GreenLine($"{Numbers.EditPercent(change24h)}% ▲") : DiscordText.RedLine($"{Numbers.EditPercent(change24h)}% ▼"),
                    true);

                embed.AddField(
                    $"Change{DiscordText.BoldItalic("(7d)")}",
                    change7d > 0 ? DiscordText.GreenLine($"{Numbers.EditPercent(change7d)}% ▲") : DiscordText.RedLine($"{Numbers.EditPercent(change7d)}% ▼"),
                    true);

                embed.AddField(
                    $"Volume{DiscordText.BoldItalic("(24h)")}",
                    DiscordText.Bold($"${Numbers.EditToReadableNumber(volume24h)}"),
                    true);

                embed.AddField($"⠀", "⠀", true);

                embed.AddField(
                    $"Market cap",
                    DiscordText.Bold($"${Numbers.EditToReadableNumber(marketCap)}"),
                    true);

                embed.AddField(
                    $"Circulating Supply",
                    DiscordText.Bold(DiscordText.SingleLineCode($"{Numbers.EditToReadableNumber(circulatingSupply)} {symbol}")),
                    true);

                embed.AddField(
                    $"Total Supply",
                    DiscordText.Bold(DiscordText.SingleLineCode($"{Numbers.EditToReadableNumber(totalSupply)} {symbol}")),
                    true);

                embed.AddField(
                    $"Max Supply",
                    DiscordText.Bold(DiscordText.SingleLineCode($"{Numbers.EditToReadableNumber(maxSupply)} {symbol}")),
                    true);

                //Coin info
                string info = $"{DiscordText.Bold("Date added:")} {dateAdded.ToShortDateString()}";

                if (cryptocurrency.Data.First().Value.Platform != null)
                    info += Environment.NewLine + DiscordText.Bold("Platform: ") + 
                        DiscordText.SingleLineCode(cryptocurrency.Data.First().Value.Platform.Name);

                embed.AddField(DiscordText.BoldUnderLine($"Coin data:"), info);


                //Change color
                if (change7d < 0)
                    embed.Color = DiscordColor.Red;
                else if (change7d > 0)
                    embed.Color = DiscordColor.Green;
                else
                    embed.Color = DiscordColor.Gray;

                await ctx.Channel.SendMessageAsync(embed: embed);
            }
            catch (WebException)
            {
                await Prompt.SendPromptAsync(ctx.Channel, ctx.User, "To view the cryptocurrency rate, enter the correct abbreviated name");
            }
            catch (Exception ex) 
            {
                DiscordLoging.PrintError(ctx.Client, ctx.User, ex);
            }
        }


        [Command("top")]        
        [Description(":trophy: Provides the top 15 most expensive cryptocurrencies by market capitalization")]
        public async Task Top(CommandContext ctx)
        {
            var api = new CoinmarketcapAPI();
            try
            {
                string json = api.ListingsLatest(1, 15);

                //Try to get currencies
                var cryptocurrencies = JsonConvert.DeserializeObject<Listing>(json);                

                var embed = new DiscordEmbedBuilder()
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = "https://i.ibb.co/wQ3JJ7c/podium.png",
                        Url = "https://coinmarketcap.com/",
                        Name = "CoinMarketCap"
                    },
                    Color = DiscordColor.Orange,
                    Timestamp = DateTime.Now,
                    Title = $"Top 15 cryptocurrecies by Market Cap"
                };
                                    
                embed.AddField($"{DiscordText.BoldUnderLine("# - Name")}", 
                    $":first_place: **- {cryptocurrencies.DataList[0].Name} {cryptocurrencies.DataList[0].Symbol}**", 
                    true);
                embed.AddField($"{DiscordText.BoldUnderLine($"Price {DiscordText.Italic("(USD)")}")}", 
                    $"**${Numbers.EditToReadableNumber(cryptocurrencies.DataList[0].Quote["USD"].Price)}**", 
                    true);
                embed.AddField($"{DiscordText.BoldUnderLine($"Market Cap {DiscordText.Italic("(USD)")}")}", 
                    $"**${Numbers.EditToReadableNumber(cryptocurrencies.DataList[0].Quote["USD"].MarketCap)}**", 
                    true);

                for (int z = 0, i = 1, k = 2; z < 7; z++, i = i + 2, k = k + 2)
                {
                    string line1 = string.Empty; 
                    string line2 = string.Empty;
                    
                    if(cryptocurrencies.DataList[i].Rank == 2 && cryptocurrencies.DataList[k].Rank == 3)
                    {
                        line1 = $":second_place: {DiscordText.Bold($"- {cryptocurrencies.DataList[i].Name} {cryptocurrencies.DataList[i].Symbol}")}";
                        line2 = $":third_place: {DiscordText.Bold($"- {cryptocurrencies.DataList[k].Name} {cryptocurrencies.DataList[k].Symbol}")}";
                    }
                    else
                    {
                        line1 = $"{DiscordText.Bold($"{cryptocurrencies.DataList[i].Rank} - {cryptocurrencies.DataList[i].Name} {cryptocurrencies.DataList[i].Symbol}")}";
                        line2 = $"{DiscordText.Bold($"{cryptocurrencies.DataList[k].Rank} - {cryptocurrencies.DataList[k].Name} {cryptocurrencies.DataList[k].Symbol}")}";
                    }

                    embed.AddField(line1, line2, true);

                    embed.AddField(
                        $"{DiscordText.Bold($"${Numbers.EditToReadableNumber(cryptocurrencies.DataList[i].Quote["USD"].Price)}")}",
                        $"{DiscordText.Bold($"${Numbers.EditToReadableNumber(cryptocurrencies.DataList[k].Quote["USD"].Price)}")}",
                        true);

                    embed.AddField(
                        $"{DiscordText.Bold($"${Numbers.EditToReadableNumber(cryptocurrencies.DataList[i].Quote["USD"].MarketCap)}")}",
                        $"{DiscordText.Bold($"${Numbers.EditToReadableNumber(cryptocurrencies.DataList[k].Quote["USD"].MarketCap)}")}",
                        true);
                }

                await ctx.Channel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                DiscordLoging.PrintError(ctx.Client, ctx.User, ex);
            }
        }


        [Command("convert")]
        public async Task Conversion(CommandContext ctx) =>
           await Prompt.SendPromptAsync(ctx.Channel, ctx.User, $"Enter {DiscordText.SingleLineCode("+help convert")} to see how the command works");


        [Command("convert")]
        [Description(":dollar: Converts crypto or fiat currency to specified currency")]
        public async Task Conversion(CommandContext ctx,
            [Description("Abbreviation of the currency to be converted")] string abbreviation,
            [Description("Amount of cryptocurrency")] double amount,
            [Description("Abbreviation of the currency to convert to")] string convert)
        {
            var api = new CoinmarketcapAPI();
            try
            {
                string json = api.PriceConversion(abbreviation, amount, convert);

                //Try to get currency
                var conversion = JsonConvert.DeserializeObject<Conversion>(json);

                var embed = new DiscordEmbedBuilder()
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = $"https://s2.coinmarketcap.com/static/img/coins/64x64/{conversion.Data.Id}.png",                        
                        Name = conversion.Data.Name
                    },

                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                    {
                        Url = $"https://s2.coinmarketcap.com/static/img/coins/64x64/{conversion.Data.Id}.png",
                        Height = 100,
                        Width = 100
                    },

                    Title = $"Price for {DiscordText.BoldUnderLine(amount.ToString())} {DiscordText.Bold(conversion.Data.Symbol)}",
                    Color = DiscordColor.Black,
                    Timestamp = DateTime.Now,
                };
                                
                embed.AddField(
                    DiscordText.Bold($"({conversion.Data.Symbol}/{convert.ToUpper()})"),
                    DiscordText.SingleLineCode($"{Numbers.EditToReadableNumber(conversion.Data.Quote[convert.ToString().ToUpper()].Price)} {convert.ToUpper()}"),
                    true);

                await ctx.Channel.SendMessageAsync(embed: embed);
            }
            catch (WebException)
            {
                await Prompt.SendPromptAsync(ctx.Channel, ctx.User, "Type the correct currency abbreviation and non negative amount");
            }
            catch (Exception ex)
            {
                DiscordLoging.PrintError(ctx.Client, ctx.User, ex);
            }
        }


        [Command("compare")]        
        public async Task Comparison(CommandContext ctx) =>
            await Prompt.SendPromptAsync(ctx.Channel, ctx.User, $"Enter {DiscordText.SingleLineCode("+help compare")} to see how the command works");


        [Command("compare")]
        [Description(":scales: Compares cryptocurrencies by some criteria, such as change1h, change24h, change7d, volume24h, marketcap")]
        public async Task Comparison(CommandContext ctx,
            [Description("Criterion such as change1h, change24h, change7d, volume24h, marketcap")] string criterion,
            [Description("Cryptocurrency abbreviations separated by a space")] params string[] symbols)
        {
            try
            {
                //Cleaning 'trash' criterions
                if (criterion != "change1h" &
                    criterion != "change24h" &
                    criterion != "change7d" &
                    criterion != "volume24h" &
                    criterion != "marketcap")
                    throw new InvalidOperationException();

                
                //Try to get currencies
                var api = new CoinmarketcapAPI();
                string json = api.QuotesLatest(Strings.Merger(symbols).ToUpper());

                //Try to deserialize json
                var deserializeJson = JsonConvert.DeserializeObject<Cryptocurrency>(json);
                var cryptocurrencies = deserializeJson.Data.Values.ToList();


                switch (criterion)
                {
                    case "change1h":
                        cryptocurrencies = cryptocurrencies.OrderByDescending(x => x.Quote.First().Value.Change1h).ToList();
                        break;
                    case "change24h":
                        cryptocurrencies = cryptocurrencies.OrderByDescending(x => x.Quote.First().Value.Change24h).ToList();
                        break;
                    case "change7d":
                        cryptocurrencies = cryptocurrencies.OrderByDescending(x => x.Quote.First().Value.Change7d).ToList();
                        break;
                    case "volume24h":
                        cryptocurrencies = cryptocurrencies.OrderByDescending(x => x.Quote.First().Value.Volume24h).ToList();
                        break;
                    case "marketcap":
                        cryptocurrencies = cryptocurrencies.OrderByDescending(x => x.Quote.First().Value.MarketCap).ToList();
                        break;
                }

                var embed = new DiscordEmbedBuilder()
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = "https://img.icons8.com/color/2x/price-comparison.png",
                        Name = $"Comparison by {criterion}"
                    },
                    
                    Color = DiscordColor.Sienna,
                    Timestamp = DateTime.Now,
                };

                string names = GetEditedNames(cryptocurrencies);
                string criterions = string.Empty;

                if (criterion == "change1h" |
                    criterion == "change24h" |
                    criterion == "change7d")
                    criterions = GetEditedPercentes(cryptocurrencies, criterion);
                else
                    criterions = GetEditedAmount(cryptocurrencies, criterion);

                string prices = GetEditedPrices(cryptocurrencies);

                embed.AddField($"#CMCR - Name (Symbol)", names, true);
                embed.AddField($"{criterion.ToUpper()}", criterions, true);
                embed.AddField($"Price", prices, true);

                await ctx.Channel.SendMessageAsync(embed: embed);
            }
            catch (WebException)
            {
                await Prompt.SendPromptAsync(ctx.Channel, ctx.User, "Type the correct cryptocurrency abbreviations");
            }
            catch (InvalidOperationException)
            {
                await Prompt.SendPromptAsync(ctx.Channel, ctx.User, "Type the correct criterion");
            }
            catch (Exception ex)
            {
                DiscordLoging.PrintError(ctx.Client, ctx.User, ex);
            }
        }


        private string GetEditedNames(List<Model.Coinmarketcap.Data> datas)
        {
            string result = string.Empty;

            foreach (var item in datas)
                result += DiscordText.MultyLineCode($"#{item.Rank} - {item.Name} ({item.Symbol})");

            return DiscordText.Bold(result);
        }

        private string GetEditedPercentes(List<Model.Coinmarketcap.Data> datas, string criteria)
        {
            string result = string.Empty;

            List<double> percentes = new List<double>();

            switch (criteria)
            {
                case "change1h":
                    datas.ForEach(x => percentes.Add(x.Quote.First().Value.Change1h));                
                    break;

                case "change24h":
                    datas.ForEach(x => percentes.Add(x.Quote.First().Value.Change24h));                
                    break;

                case "change7d":
                    datas.ForEach(x => percentes.Add(x.Quote.First().Value.Change7d));                
                    break;
            }

            foreach (var item in percentes)
                result += item > 0 ? DiscordText.GreenLine($"{Numbers.EditPercent(item)}% ▲") : DiscordText.RedLine($"{Numbers.EditPercent(item)}% ▼");

            return result;
        }
        
        private string GetEditedAmount(List<Model.Coinmarketcap.Data> datas, string criteria)
        {
            string result = string.Empty;

            List<ulong> amount = new List<ulong>();

            switch (criteria)
            {
                case "volume24h":
                    datas.ForEach(x => amount.Add(x.Quote.First().Value.Volume24h));
                    break;

                case "marketcap":
                    datas.ForEach(x => amount.Add(x.Quote.First().Value.MarketCap));
                    break;
            }

            foreach (var item in amount)
                result += DiscordText.MultyLineCode($"${Numbers.EditToReadableNumber(item)}");

            return result;
        }

        private string GetEditedPrices(List<Model.Coinmarketcap.Data> datas)
        {
            string result = string.Empty;

            foreach (var item in datas)
                result += DiscordText.MultyLineCode($"${Numbers.EditToReadableNumber(item.Quote.First().Value.Price)}");

            return result;
        }

    }
}


