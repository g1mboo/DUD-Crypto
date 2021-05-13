using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DUDCrypto.Model.Decrypt.API
{
    public class DecryptDOM
    {
        public List<News> ReturnNews()
        {
            List<News> news = new List<News>();

            IWebDriver driver;
            var options = new ChromeOptions();
            options.AddArgument("--headless");

            //using (driver = new ChromeDriver(Environment.CurrentDirectory, options))
            using (driver = new ChromeDriver(Environment.CurrentDirectory))
            {
                //Connecting
                IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
                driver.Url = "https://decrypt.co/news";
                js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                //Download
                var html = driver.FindElement(By.XPath("/html")).GetAttribute("innerHTML");
                //Parse
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                string titleXPath =     $"//*[@id='__next']/div/div[1]/div[2]/div[2]/div[2]/div[3]/div/div[2]/a/div/div[2]/h2";                
                string categoryXPath =  $"//*[@id='__next']/div/div[1]/div[2]/div[2]/div[2]/div[3]/div/div[2]/a/div/div[2]/div/div[1]/div[1]/div/span[3]";
                string summaryXPath =   $"//*[@id='__next']/div/div[1]/div[2]/div[2]/div[2]/div[3]/div/div[2]/a/div/div[2]/p/span";
                string urlXPath =       $"//*[@id='__next']/div/div[1]/div[2]/div[2]/div[2]/div[3]/div/div[2]/a";
                string imageUrlXPath =  $"//*[@id='__next']/div/div[1]/div[2]/div[2]/div[2]/div[3]/div/div[2]/a/div/div[1]/div/div/div/div[1]";
                string dateXPath =      $"//*[@id='__next']/div/div[1]/div[2]/div[2]/div[2]/div[3]/div/div[2]/a/div/div[2]/div/div[1]/div[2]/span[2]/span/time[1]";
                string authorXPath =    $"//*[@id='__next']/div/div[1]/div[2]/div[2]/div[2]/div[3]/div/div[2]/a/div/div[2]/div/div[1]/div[2]/span[1]";

                var titles = doc.DocumentNode.SelectNodes(titleXPath);
                var categories =  doc.DocumentNode.SelectNodes(categoryXPath);                
                var summaries = doc.DocumentNode.SelectNodes(summaryXPath);
                var urls = doc.DocumentNode.SelectNodes(urlXPath);
                var images = doc.DocumentNode.SelectNodes(imageUrlXPath);
                var dates = doc.DocumentNode.SelectNodes(dateXPath);
                var authors = doc.DocumentNode.SelectNodes(authorXPath);

                if(titles.Count == categories.Count &
                    categories.Count == summaries.Count &
                    summaries.Count == urls.Count &
                    urls.Count == images.Count &
                    images.Count == dates.Count &
                    dates.Count == authors.Count)
                    for (int i = 0; i < titles.Count; i++)
                    {
                        news.Add(new News(
                            titles[i].InnerText,
                            categories[i].InnerText,
                            "https://decrypt.co" + categories[i].GetAttributeValue("href", ""),
                            summaries[i].InnerText,
                            "https://decrypt.co" + urls[i].GetAttributeValue("href", ""),
                            images[i].GetAttributeValue("src", ""),
                            DateTime.Parse(dates[i].GetAttributeValue("datetime", "")), 
                            authors[i].InnerText));
                    }
                
            }
            driver.Quit();

            return news;
        }
    }
}
