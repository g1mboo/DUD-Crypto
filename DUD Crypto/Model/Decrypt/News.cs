using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Model.Decrypt
{
    public class News
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string CategoryUrl { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DatePublished { get; set; }
        public string Author { get; set; }

        public News(string title, string category, string categoryUrl, string summary, string url, string imageUrl, DateTime date, string author)
        {
            Title = title;
            Category = category;
            CategoryUrl = categoryUrl;
            Summary = summary;
            Url = url;
            ImageUrl = imageUrl;
            DatePublished = date;
            Author = author;
        }
    }
}
