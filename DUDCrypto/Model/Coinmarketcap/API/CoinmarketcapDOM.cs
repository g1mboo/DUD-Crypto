using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DUDCrypto.Model.Coinmarketcap.API
{
    public class CoinmarketcapDOM
    {
        public List<Earn> ReturnEarn()
        {
            var earnCompanies = new List<Earn>();

            IWebDriver driver;
            var options = new ChromeOptions();
            options.AddArgument("--headless");

            using (driver = new ChromeDriver(Environment.CurrentDirectory, options))
            {
                //Connecting
                driver.Url = "https://coinmarketcap.com/earn/";                
                //Download
                var html = driver.FindElement(By.XPath("/html")).GetAttribute("innerHTML");
                //Parse                
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                string nameXPath =          $"//*[@id='Whatcrypto']/div[2]/div/div/div/div/div/div[1]/div[2]/h2";
                string symbolXPath =        $"//*[@id='Whatcrypto']/div[2]/div/div/div/div/div/div[1]/div[2]/div";
                string urlXPath =           $"//*[@id='Whatcrypto']/div[2]/div/div/div/div/div/div[2]/a";
                string iconUrlXPath =       $"//*[@id='Whatcrypto']/div[2]/div/div/div/div/div/div[1]/div[1]/img";
                string imageUrlXPath =      $"//*[@id='Whatcrypto']/div[2]/div/div/div/div/a";
                string descriptionXPath =   $"//*[@id='Whatcrypto']/div[2]/div/div/div/div/div/div[1]/p";
                string liveXPath =          $"//*[@id='Whatcrypto']/div[2]/div/div/div/div/div/div[2]/div[2]";

                var nameNodes = doc.DocumentNode.SelectNodes(nameXPath);
                var symbolNodes = doc.DocumentNode.SelectNodes(symbolXPath);
                var urlNodes = doc.DocumentNode.SelectNodes(urlXPath);
                var iconNodes = doc.DocumentNode.SelectNodes(iconUrlXPath);
                var imageNodes = doc.DocumentNode.SelectNodes(imageUrlXPath);
                var descriptionNodes = doc.DocumentNode.SelectNodes(descriptionXPath);
                var liveNodes = doc.DocumentNode.SelectNodes(liveXPath);

                List<string> images = new List<string>();

                foreach (var item in imageNodes)
                {
                    string imageUrlFilthi = item.GetAttributeValue("style", "");
                    string imageUrl = string.Empty;

                    for (int i = imageUrlFilthi.IndexOf(";") + 1; i < imageUrlFilthi.LastIndexOf("&quot"); i++)
                        imageUrl += imageUrlFilthi[i];

                    images.Add(imageUrl);
                }

                if (nameNodes.Count == symbolNodes.Count &
                    symbolNodes.Count == iconNodes.Count &
                    iconNodes.Count == imageNodes.Count &
                    imageNodes.Count == descriptionNodes.Count &
                    descriptionNodes.Count == liveNodes.Count &
                    urlNodes.Count == liveNodes.Count)
                {
                    for (int i = 0; i < nameNodes.Count; i++)
                        earnCompanies.Add(new Earn(
                            nameNodes[i].InnerText, 
                            symbolNodes[i].InnerText, 
                            "https://coinmarketcap.com" + urlNodes[i].GetAttributeValue("href", ""),
                            iconNodes[i].GetAttributeValue("src", ""),
                            images[i],
                            descriptionNodes[i].InnerText, 
                            liveNodes[i].GetAttributeValue("class", "").Contains("w-condition-invisible") ? false : true));                       
                }
            }
            driver.Quit();

            return earnCompanies;
        }
    }
}
