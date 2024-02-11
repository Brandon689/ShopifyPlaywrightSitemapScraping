using ShopifyPlaywrightSitemapScraping;
using System.Diagnostics;

Controller controller = new Controller();

await controller.Init("https://www.kawaiies.com");

//var sitemap = await controller.GetSitemapLinks();
//foreach (var item in sitemap)
//{
//    Console.WriteLine(item);
//}
//var sitemapProduct = await controller.GetProductLinks(sitemap[0]);

//await controller.DownloadToFS();


ParallelDownloader download = new();

var r = controller.RetrieveProductData();


//test
List<string> list = new List<string>();
foreach (var item in r)
{
    foreach (var it in item.Images)
    {
        list.Add(it.Src);
    }
}
Stopwatch sw = Stopwatch.StartNew();
await download.Run(list);
Console.WriteLine(sw.ElapsedMilliseconds);
await controller.Dispose();