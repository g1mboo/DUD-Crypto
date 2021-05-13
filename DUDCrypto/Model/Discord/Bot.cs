using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DUDCrypto.Model.Discord
{
    public class Bot
    {
        public readonly EventId BotEventId = new EventId(666, "DUD Crypto");
        public static string _prefix { get; private set; }
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        private Timer BotStatusTimer { get; set; }

        public Bot()
        {
           
        }

        public async Task ActivateBotAsync()
        {
            //Инициализируем конфигурацию
            var json = string.Empty;

            using (var fo = File.OpenRead("bot-config.json"))
            using (var fr = new StreamReader(fo, new UTF8Encoding(false)))
                json = await fr.ReadToEndAsync().ConfigureAwait(false);

            var configuration = JsonConvert.DeserializeObject<BotConfig>(json);

            //Giving a _prefix
            _prefix = configuration.Prefix;

            //Initialize bot
            Client = new DiscordClient(new DiscordConfiguration()
            {
                Token = configuration.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug
            });

            Client.UseInteractivity(new InteractivityConfiguration());

            var prefixes = new List<string>
            {
                configuration.Prefix
            };

            //Initialize bot commands
            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = prefixes,
                EnableDms = false,
                EnableMentionPrefix = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);            
            Commands.RegisterCommands<Commands.CoinsCommands>();
            Commands.RegisterCommands<Commands.HelpCommands>();
            //Commands.RegisterCommands<Commands.TestCommands>(); //Only for tests           
            Commands.RegisterCommands<Commands.InfoCommands>();

            Client.Ready += ClientReady;
            Client.GuildAvailable += ClientGuildAvailable;
            Client.ClientErrored += ClientError;

            //Run
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }


        private int _switcher = 1;

        private async void UpdateBotStatusAsync(object sender)
        {
            var url = "https://coinmarketcap.com/";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            if (_switcher == 4)
            {
                await Client.UpdateStatusAsync(
                    new DiscordActivity($"{Bot._prefix}help", ActivityType.Watching),
                    null,
                    null);

                _switcher = 1;
            }
            else
            {
                string symbolXPath =    $"//*[@id='__next']/div/div[1]/div[2]/div/div/div[2]/table/tbody/tr[{_switcher}]/td[3]/div/a/div/div/div/p";
                string priceXPath =     $"//*[@id='__next']/div/div[1]/div[2]/div/div/div[2]/table/tbody/tr[{_switcher}]/td[4]/div/a";
                var symbol = doc.DocumentNode.SelectSingleNode(symbolXPath).InnerText;
                var price = doc.DocumentNode.SelectSingleNode(priceXPath).InnerText;

                await Client.UpdateStatusAsync(
                    new DiscordActivity($"{symbol} -> {price}", ActivityType.Watching),
                    null,
                    null);

                _switcher++;
            }
            
        }
    

        private Task ClientReady(DiscordClient sender, ReadyEventArgs e)
        {            
            sender.Logger.LogInformation(BotEventId, "Client is ready.");

            //Activate status updating
            BotStatusTimer = new Timer(UpdateBotStatusAsync, null, 0, 20000);
            sender.Logger.LogInformation(BotEventId, "Status is updating every 20s");

            return Task.CompletedTask;
        }

        private Task ClientGuildAvailable(DiscordClient sender, GuildCreateEventArgs e)
        {
            sender.Logger.LogInformation(BotEventId, $"Guild available: {e.Guild.Name}");
                        
            return Task.CompletedTask;
        }

        private Task ClientError(DiscordClient sender, ClientErrorEventArgs e)
        {            
            sender.Logger.LogError(BotEventId, e.Exception, "Exception occured");
                       
            return Task.CompletedTask;
        }

    }
}
