using DUDCrypto.Data.Discord;
using DUDCrypto.Model.Discord;
using DUDCrypto.Model.Discord.Assembly;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DUDCrypto.Commands
{
    public class HelpCommands : BaseCommandModule
    {
        [Command("help")]        
        public async Task Help(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    //IconUrl = "https://i.imgur.com/UtcbcJn.png",
                    Name = $"DUD Crypto"
                },
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                {
                    Url = "https://i.imgur.com/lyIVt5a.png",
                    Height = 100,
                    Width = 100
                }
            };

            embed.AddField($"Commands", $"To see all the commands, enter {DiscordText.SingleLineCode($"{Bot.Prefix}help commands")}");
            embed.AddField($"Creator", $"Alexander Dudkin");

            await ctx.Channel.SendMessageAsync(embed: embed);
        }

        [Command("help")]
        public async Task Help(CommandContext ctx, string name)
        {
            var commands = GetCommandInfos(typeof(InfoCommands), typeof(CoinsCommands));

            switch (name)
            {
                case "commands":
                    var embed = new DiscordEmbedBuilder()
                    {                        
                        Title = $"Commands"
                    };

                    foreach (var item in commands)
                    {
                        string commandName = string.Empty;

                        if (item.Arguments.Count > 0)
                            foreach (var arg in item.Arguments)                            
                                commandName += $" [{arg.Name}]";

                        commandName = DiscordText.SingleLineCode(Bot.Prefix + item.Name + commandName);

                        string description = item.Description + Environment.NewLine + "⠀";

                        embed.AddField(commandName, description);
                    }

                    await ctx.Channel.SendMessageAsync(embed: embed);
                    break;
                default:
                    CommandInfo commandInfo;
                    if ((commandInfo = commands.Where(x => x.Name == name).First()) != null)
                    {
                        string commandName = string.Empty;

                        embed = new DiscordEmbedBuilder();

                        if (commandInfo.Arguments.Count > 0)
                            foreach (var arg in commandInfo.Arguments)
                            {
                                commandName += $" [{arg.Name}]";
                                embed.AddField(DiscordText.SingleLineCode($"[{arg.Name}]"), arg.Description, true);
                            }

                        commandName = commandInfo.Name + commandName;

                        embed.Title = $"About: {DiscordText.SingleLineCode(Bot.Prefix + commandName)}";
                        embed.Description = DiscordText.Bold(commandInfo.Description);

                        await ctx.Channel.SendMessageAsync(embed: embed);
                    }
                    else
                        await Prompt.SendPromptAsync(ctx.Channel, ctx.User, "command not found");                    
                    break;
            }            
        }

        private List<CommandInfo> GetCommandInfos(params Type[] types)
        {
            var commands = new List<CommandInfo>();
            
            foreach(var type in types)
            {                
                foreach(var method in type.GetMethods())
                {
                    CommandAttribute commandNameAttribute = null;
                    DescriptionAttribute commandDescriptionAttribute = null;

                    foreach (var attribute in method.GetCustomAttributes(true))
                    {   
                        if(attribute as CommandAttribute != null)
                            commandNameAttribute = attribute as CommandAttribute;
                        else
                            commandDescriptionAttribute = attribute as DescriptionAttribute;
                    }

                    List<ArgumentInfo> arguments = new List<ArgumentInfo>();

                    foreach(var parameter in method.GetParameters())
                    {                       
                        string paramName = parameter.Name;
                        DescriptionAttribute paramDescriptionAttribute = null;

                        var paramAttributes = parameter.GetCustomAttributes(true);

                        foreach (var paramAttr in paramAttributes)
                            paramDescriptionAttribute = paramAttr as DescriptionAttribute;                        

                        if(paramDescriptionAttribute != null)
                            arguments.Add(new ArgumentInfo(paramName, paramDescriptionAttribute.Description));                                                
                    }

                    if(commandNameAttribute != null & commandDescriptionAttribute != null)
                        commands.Add(new CommandInfo(commandNameAttribute.Name, commandDescriptionAttribute.Description, arguments.ToArray()));
                }
            }

            return commands;
        }
    }
}
