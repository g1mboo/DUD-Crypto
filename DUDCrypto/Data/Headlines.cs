using DUDCrypto.Model.Coinmarketcap;
using DUDCrypto.Model.Coinmarketcap.API;
using DUDCrypto.Model.Decrypt;
using DUDCrypto.Model.Decrypt.API;
using DUDCrypto.Model.Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DUDCrypto.Data
{
    public static class Headlines
    {
        public static List<News> News { get; private set; }
        public static List<Earn> Earn { get; private set; }
        

        public static async void UploadAndUpdateNewsAsync(object sender)
        {
            var site = new DecryptDOM();
            News = await Task.Run(() => site.ReturnNews()).ConfigureAwait(false);
        }

        public static async Task UploadEarnAsync()
        {
            var site = new CoinmarketcapDOM();
            Earn = await Task.Run(() => site.ReturnEarn()).ConfigureAwait(false);
        }
    }
}
