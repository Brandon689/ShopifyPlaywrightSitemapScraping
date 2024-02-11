
Controller s = new Controller();

await s.Init("https://www.kawaiies.com");

var n = await s.GetSitemapLinks();
foreach (var item in n)
{
    Console.WriteLine(item);
}
;
var t = await s.GetProductLinks(n[0]);

;
