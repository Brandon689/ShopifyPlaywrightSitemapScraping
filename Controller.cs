using Microsoft.Playwright;

public class Controller
{
    //private readonly HttpClient _httpClient;
    private IPlaywright playwright;
    private IBrowser browser;
    private IPage page;
    private string baseUrl;

    public Controller()
    {
        //_httpClient = new HttpClient();
    }

    public async Task Init(string baseUrl)
    {
        baseUrl = baseUrl;
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
}
