Controller controller = new Controller();

await controller.Init("https://www.kawaiies.com");

var n = await controller.GetSitemapLinks();
foreach (var item in n)
{
    Console.WriteLine(item);
}
var t = await controller.GetProductLinks(n[0]);

await controller.Dispose();