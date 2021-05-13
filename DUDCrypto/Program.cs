using DUDCrypto.Data;
using DUDCrypto.Model;
using DUDCrypto.Model.Discord;
using DSharpPlus.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DUDCrypto
{
    class Program
    {
        private static Bot CryptoBot { get; set; }        
        
        private static TimerCallback NewsUpdateTimer { get; set; }

        static async Task Main(string[] args)
        {
            //Upload earn headlines
            await Headlines.UploadEarnAsync();

            //Activate actual news updating
            NewsUpdateTimer = new TimerCallback(Headlines.UploadAndUpdateNewsAsync);
            Timer newsTimer = new Timer(NewsUpdateTimer, null, 0, 600000);

            //Activate a bot
            CryptoBot = new Bot();
            await CryptoBot.ActivateBotAsync();            
        }
        
    }
}
