using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Linq;
using System.Globalization;

class Program
{
    static async Task Main()
    {
        using var client = new HttpClient();

        client.DefaultRequestHeaders.UserAgent.ParseAdd(
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64)"
        );

        await Task1_CSharpPage(client);
        await Task3_Wikipedia(client);
        await Task4_FoxtrotPrices(client);
    }

    static async Task Task1_CSharpPage(HttpClient client)
    {
        string url = "https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/overview";
        string html = await client.GetStringAsync(url);

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var text = doc.DocumentNode.InnerText;

        // 1.1 Sentence with "async and await"
        var sentence = text
            .Split('.', '!', '?')
            .FirstOrDefault(s => s.Contains("async") && s.Contains("await"));

        Console.WriteLine("Sentence with async and await:");
        Console.WriteLine(sentence?.Trim());

        // 1.2 Paragraph with "equivalent format"
        var paragraph = doc.DocumentNode
            .SelectNodes("//p")
            ?.FirstOrDefault(p => p.InnerText.Contains("equivalent format"))
            ?.InnerText;

        Console.WriteLine("\nParagraph with 'equivalent format':");
        Console.WriteLine(paragraph);

        // 1.3 Count of word "program"
        int count = text
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Count(w => w.Equals("program", StringComparison.OrdinalIgnoreCase) ||
                        w.Equals("programs", StringComparison.OrdinalIgnoreCase));

        Console.WriteLine($"\nCount of word 'program': {count}");
    }

    static async Task Task3_Wikipedia(HttpClient client)
    {
        string url = "https://en.wikipedia.org/wiki/.NET";
        string html = await client.GetStringAsync(url);

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

       
        var semverLink = doc.DocumentNode
            .SelectNodes("//a[@href]")
            ?.FirstOrDefault(a => a.InnerText.Contains("semantic", StringComparison.OrdinalIgnoreCase));

        Console.WriteLine("\nSemantic Versioning link:");
        Console.WriteLine("https://en.wikipedia.org" + semverLink?.GetAttributeValue("href", ""));

        string semverUrl = "https://en.wikipedia.org/wiki/Semantic_versioning";
        string semverHtml = await client.GetStringAsync(semverUrl);

        var semverDoc = new HtmlDocument();
        semverDoc.LoadHtml(semverHtml);

     
        var history = semverDoc.DocumentNode
            .SelectSingleNode("//span[@id='History']/parent::h2/following-sibling::p");

        Console.WriteLine("\nSemantic Versioning History:");
        Console.WriteLine(history?.InnerText);
    }

    static async Task Task4_FoxtrotPrices(HttpClient client)
    {
        string url =
            "https://www.foxtrot.com.ua/uk/shop/mobilnye_telefony_samsung_smartfon.html?filter=44959%3D137%3B";

        using var request = new HttpRequestMessage(HttpMethod.Get, url);

        request.Headers.UserAgent.ParseAdd(
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64)"
        );
        request.Headers.Add("Accept", "text/html");
        request.Headers.Add("Accept-Language", "uk-UA,uk;q=0.9,en-US;q=0.8");

        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Foxtrot blocked request: {response.StatusCode}");
            return; 
        }

        string html = await response.Content.ReadAsStringAsync();

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

      
        var prices = doc.DocumentNode
            .SelectNodes("//span[contains(@class,'price')]")
            ?.Select(p =>
            {
                var text = new string(p.InnerText.Where(char.IsDigit).ToArray());
                return decimal.TryParse(text, out var v) ? v : (decimal?)null;
            })
            .Where(v => v.HasValue)
            .Select(v => v.Value)
            .ToList();

        if (prices == null || prices.Count == 0)
        {
            Console.WriteLine("Prices are loaded via JavaScript. HTML parsing not possible.");
            return;
        }

        Console.WriteLine($"Average price: {prices.Average()} грн");
    }
}
