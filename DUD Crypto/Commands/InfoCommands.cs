using DUDCrypto.Data.Discord;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DUDCrypto.Model.Coinmarketcap.API;
using DUDCrypto.Data;
using DSharpPlus.Interactivity.Extensions;
using DUDCrypto.Model.Discord;

namespace DUDCrypto.Commands
{
    public class InfoCommands : BaseCommandModule
    {
        [Command("news")]
        [Description(":newspaper2: View the latest news")]
        public async Task News(CommandContext ctx)
        {
            var duration = TimeSpan.FromSeconds(600);
            try
            {

                var news = Headlines.News;
                var embedsList = new List<DiscordEmbed>();

                if (news == null)
                    throw new Exception("News has not been uploaded yet");

                for(int j = 0; j < news.Count; j++)
                {
                    embedsList.Add( 
                        new DiscordEmbedBuilder{
                            Author = new DiscordEmbedBuilder.EmbedAuthor
                            {
                                Name = "News ▶ " + news[j].Category,
                                Url = news[j].CategoryUrl
                            },

                            Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                            {
                                Url = "https://i.imgur.com/Rq1pQsJ.png",
                                Width = 100
                            },

                            Footer = new DiscordEmbedBuilder.EmbedFooter
                            {
                                Text = $"#{j + 1}" + " • " + news[j].Author
                            },

                            Title = news[j].Title,
                            Url = news[j].Url,
                            Description = news[j].Summary,
                            ImageUrl = news[j].ImageUrl,
                            Timestamp = news[j].DatePublished,
                            Color = DiscordColor.Azure                            
                        }                        
                        .Build());
                }

                int messageIterator = 0;

                var message = await ctx.Channel.SendMessageAsync(embed: embedsList[messageIterator]).ConfigureAwait(false);

                var toFirstEmoji = DiscordEmoji.FromName(ctx.Client, ":one:");
                var arrowLeftEmoji = DiscordEmoji.FromName(ctx.Client, ":arrow_backward:");
                var arrowRightEmoji = DiscordEmoji.FromName(ctx.Client, ":arrow_forward:");                

                await message.CreateReactionAsync(toFirstEmoji).ConfigureAwait(false);
                await message.CreateReactionAsync(arrowLeftEmoji).ConfigureAwait(false);
                await message.CreateReactionAsync(arrowRightEmoji).ConfigureAwait(false);

                await Task.Delay(400);

                var interactivity = ctx.Client.GetInteractivity();
                                
                while (true)
                {
                    var reactionResult = await interactivity.WaitForReactionAsync(
                    x => x.Message == message && (
                    x.Emoji == toFirstEmoji |
                    x.Emoji == arrowLeftEmoji |
                    x.Emoji == arrowRightEmoji), duration)
                    .ConfigureAwait(false);

                    if (reactionResult.Result == null)
                        break;

                    if (reactionResult.Result.Emoji == toFirstEmoji)
                    {
                        messageIterator = 0;

                        await message.DeleteReactionAsync(toFirstEmoji, reactionResult.Result.User).ConfigureAwait(false);
                    }
                    else if (reactionResult.Result.Emoji == arrowLeftEmoji)
                    {
                        _ = (messageIterator > 0) ? messageIterator-- : messageIterator = embedsList.Count - 1;

                        await message.DeleteReactionAsync(arrowLeftEmoji, reactionResult.Result.User).ConfigureAwait(false);
                    }
                    else if (reactionResult.Result.Emoji == arrowRightEmoji)
                    {
                        _ = (messageIterator < embedsList.Count - 1) ? messageIterator++ : messageIterator = 0;

                        await message.DeleteReactionAsync(arrowRightEmoji, reactionResult.Result.User).ConfigureAwait(false);
                    }

                    await message.ModifyAsync(embedsList[messageIterator]).ConfigureAwait(false); 
                }
            }           
            catch (Exception ex)
            {
                DiscordLoging.PrintError(ctx.Client, ctx.User, ex);
            }
        }

        [Command("earn")]
        [Description(":microscope: Study crypto projects and get rewards")]
        public async Task Earn(CommandContext ctx)
        {
            var duration = TimeSpan.FromSeconds(600);
            try
            {
                var earns = Headlines.Earn;
                var embedsList = new List<DiscordEmbed>();

                if (earns == null)
                    throw new Exception("Earn companies has not been uploaded yet");

                for (int j = 0; j < earns.Count; j++)
                {
                    embedsList.Add(
                        new DiscordEmbedBuilder
                        {
                            Author = new DiscordEmbedBuilder.EmbedAuthor
                            {
                                Name = earns[j].Name + " " + earns[j].Symbol,
                                IconUrl = earns[j].IconUrl,
                                Url = earns[j].Url
                            },

                            Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                            {                                
                                Url = earns[j].IconUrl,                                
                            },

                            Footer = new DiscordEmbedBuilder.EmbedFooter
                            {
                                Text = $"Page {j + 1} / {earns.Count}"
                            },
                                                        
                            Description = earns[j].Description,
                            ImageUrl = earns[j].ImageUrl,
                            Color = DiscordColor.Purple
                        }
                        .AddField(DiscordText.BoldUnderLine("Campaign status:"), (earns[j].Live) ? DiscordText.Bold(DiscordText.MultyLineCode("LIVE")) : DiscordText.Bold(DiscordText.MultyLineCode("DONE")))
                        .Build());
                }

                int messageIterator = 0;

                var message = await ctx.Channel.SendMessageAsync(embed: embedsList[messageIterator]).ConfigureAwait(false);

                var toFirstEmoji = DiscordEmoji.FromName(ctx.Client, ":one:");
                var arrowLeftEmoji = DiscordEmoji.FromName(ctx.Client, ":arrow_backward:");
                var arrowRightEmoji = DiscordEmoji.FromName(ctx.Client, ":arrow_forward:");

                await message.CreateReactionAsync(toFirstEmoji).ConfigureAwait(false);
                await message.CreateReactionAsync(arrowLeftEmoji).ConfigureAwait(false);
                await message.CreateReactionAsync(arrowRightEmoji).ConfigureAwait(false);

                await Task.Delay(400);

                var interactivity = ctx.Client.GetInteractivity();

                while (true)
                {
                    var reactionResult = await interactivity.WaitForReactionAsync(
                    x => x.Message == message && (
                    x.Emoji == toFirstEmoji |
                    x.Emoji == arrowLeftEmoji |
                    x.Emoji == arrowRightEmoji), duration)
                    .ConfigureAwait(false);

                    if (reactionResult.Result == null)
                        break;

                    if (reactionResult.Result.Emoji == toFirstEmoji)
                    {
                        messageIterator = 0;

                        await message.DeleteReactionAsync(toFirstEmoji, reactionResult.Result.User).ConfigureAwait(false);
                    }
                    else if (reactionResult.Result.Emoji == arrowLeftEmoji)
                    {
                        _ = (messageIterator > 0) ? messageIterator-- : messageIterator = 9;

                        await message.DeleteReactionAsync(arrowLeftEmoji, reactionResult.Result.User).ConfigureAwait(false);
                    }
                    else if (reactionResult.Result.Emoji == arrowRightEmoji)
                    {
                        _ = (messageIterator < 9) ? messageIterator++ : messageIterator = 0;

                        await message.DeleteReactionAsync(arrowRightEmoji, reactionResult.Result.User).ConfigureAwait(false);
                    }

                    await message.ModifyAsync(embedsList[messageIterator]).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                DiscordLoging.PrintError(ctx.Client, ctx.User, ex);
            }
        }
    }
}
