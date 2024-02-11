
SitemapScraper s = new SitemapScraper();

await s.V();

var n = await s.Sitemap();
foreach (var item in n)
{
    Console.WriteLine(item);
}
;
var t = await s.Products(n[0]);

;

//using Microsoft.Playwright;
//using System.Xml.Serialization;
//using System.IO;
//using System.Threading.Tasks;
//using System;
//using System.Diagnostics;
//using ShopifyPlaywrightSitemapScraping;

//HttpClient httpClient = new HttpClient();
//var s = await httpClient.GetStringAsync("https://www.kawaiies.com/sitemap.xml");

//XmlSerializer serializer2 = new XmlSerializer(typeof(Sitemapindex));
//string sitemap = "";
//using (StringReader reader2 = new StringReader(sitemap))
//{
//    var test = (Sitemapindex)serializer2.Deserialize(reader2);
//}




//XmlSerializer serializer = new XmlSerializer(typeof(Urlset));
//StringReader reader = new StringReader(File.ReadAllText(@"C:\2024\5\sitemap_products_1.xml"));

//Urlset? n = serializer.Deserialize(reader) as Urlset;

//using var playwright = await Playwright.CreateAsync();
//await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });
//var page = await browser.NewPageAsync();

//await page.RouteAsync("**/*.js", route => route.AbortAsync());
//int limit = n.Url.Count;

////Stopwatch sw = Stopwatch.StartNew();

//for (int i = 1; i < limit; ++i)
//{
//    string url = n.Url[i].Loc + ".json";
//    Console.WriteLine(url);

//    await page.GotoAsync(url);

//    string jsonContent = await page.EvalOnSelectorAsync<string>("pre", "element => element.textContent");

//    string fileName = "../../../json/" + url.Replace("https://www.kawaiies.com/products/", "");
//    File.WriteAllText(fileName, jsonContent);

//    //await Task.Delay(6000);
//}

////sw.Stop();
////Console.WriteLine(sw.ElapsedMilliseconds);