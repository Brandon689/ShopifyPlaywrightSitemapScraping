using Microsoft.Playwright;

public class Controller
{
    //private readonly HttpClient _httpClient;
    private IPlaywright playwright;
    private IBrowser browser;
    private IPage page;
    private string baseUrl;
    private string baseName;

    public Controller()
    {
        //_httpClient = new HttpClient();
    }

    public async Task Init(string _baseUrl)
    {
        baseUrl = _baseUrl;
        baseName = deriveName();
        playwright = await Playwright.CreateAsync();
        browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });
        page = await browser.NewPageAsync();
        await page.RouteAsync("**/*.js", route => route.AbortAsync());
    }

    public async Task Dispose()
    {
        await browser.CloseAsync();
        playwright.Dispose();
        await browser.DisposeAsync();
    }

    public async ValueTask<string[]> GetSitemapLinks()
    {
        await page.GotoAsync($"{baseUrl}/sitemap.xml");
        var m = await page.QuerySelectorAllAsync("sitemapindex sitemap");
        string[] x = new string[m.Count];
        for (int i = 0; i < m.Count; ++i)
        {
            x[i] = (await m[i].TextContentAsync()).Trim().Replace("\n", "");
        }
        return x;
    }

    public async ValueTask<string[]> GetProductLinks(string collection)
    {
        await page.GotoAsync(collection);
        var m = await page.QuerySelectorAllAsync("urlset url loc");
        var textContents = await Task.WhenAll(m.Select(async element => await element.TextContentAsync()));
        return textContents.Where(x => !x.StartsWith("https://cdn.shopify.com")).Skip(1).ToArray();
    }

    public async ValueTask<string> GetProductJSON(string uri)
    {
        if (uri.EndsWith(".json") == false)
            uri += ".json";
        await page.GotoAsync(uri);
        return = await page.EvalOnSelectorAsync<string>("pre", "element => element.textContent");
    }
    
    public async Task DownloadToFS()
    {
        Directory.CreateDirectory($"../../../{baseName}/");
        var sitemap = await this.GetSitemapLinks();
        var productSitemap = await this.GetProductLinks(sitemap[0]);
        for (var i = 0; i < productSitemap.Length; i++)
        {
            string productJson = await this.GetProductJSON(productSitemap[i]);
            File.WriteAllText($"../../../{baseName}/{productSitemap[i].Replace($"{baseUrl}/products/", "")}.json", productJson);
        }
    }

    private string deriveName()
    {
        return baseUrl
            .Replace("https://", "")
            .Replace("www.", "")
            .Replace(".com", "");
    }
}
