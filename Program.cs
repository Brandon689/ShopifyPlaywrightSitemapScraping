﻿using ShopifyPlaywrightSitemapScraping;
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

var r = controller.RetrieveProductData();

controller.DownloadImages(r);