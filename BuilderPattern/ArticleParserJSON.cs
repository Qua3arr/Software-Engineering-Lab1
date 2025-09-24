using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Text.Encodings.Web;

namespace BuilderPattern
{
    //Класс статьи
    public class Article
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string Text { get; set; }
        public string Hash { get; set; }
        public bool IsHashValid {get; set; }
    }

    //Класс строителя
    public class ArticleBuilder
    {
        private Article Article { get; set; }

        public ArticleBuilder()
        {
            Article = new Article();
        }

        public ArticleBuilder SetTitle(string title)
        {
            Article.Title = title.Trim();
            return this;
        }

        public ArticleBuilder SetAuthors(string authorsLine)
        {
            var authors = new List<string>();
            foreach (var author in authorsLine.Split(','))
            {
                authors.Add(author.Trim());
            }
            Article.Authors = authors;
            return this;
        }

        public ArticleBuilder SetContent(string text)
        {
            Article.Text = text.Trim();
            return this;
        }

        public ArticleBuilder SetHash(string hash)
        {
            Article.Hash = hash.Trim();
            return this;
        }

        public Article Build()
        {
            return Article;
        }
    }

    //Класс распорядителя (Director)
    public class ArticleWriter
    {
        public Article ParseText(string filePath) 
        {
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            ArticleBuilder builder = new ArticleBuilder();

            int line_counter = 0;

            string text = string.Empty;

            foreach (string line in lines)
            {
                line_counter++;
                if (line_counter == 1)
                {
                    builder.SetTitle(line);
                }
                else if (line_counter == 2)
                {
                    builder.SetAuthors(line);
                }
                else if (line_counter == lines.Length)
                {
                    builder.SetHash(line);
                    builder.SetContent(text);
                }
                else 
                { 
                    text = text + line + "\n";
                }
            }

            return builder.Build();
        }
    }

    //Проверка правильности хэша
    public static class HashValidator
    {
        public static bool Verify(Article article)
        {
            using (var md5 = MD5.Create())
            {
                byte[] contentBytes = Encoding.UTF8.GetBytes(article.Text);
                byte[] hashBytes = md5.ComputeHash(contentBytes);
                string computedHash = BitConverter.ToString(hashBytes);
                return computedHash == article.Hash;
            }
        }
    }

    internal class ArticleParserJSON
    {
        static void Main(string[] args)
        {
            string inputFile = "article.txt";
            string outputFile = "article.json";

            var parser = new ArticleWriter();
            var article = parser.ParseText(inputFile);
            article.IsHashValid = HashValidator.Verify(article);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(article, options);

            File.WriteAllText(outputFile, json, Encoding.UTF8);

            Console.WriteLine("Файл статьи сохранён в формате JSON!");
        }
    }
}
