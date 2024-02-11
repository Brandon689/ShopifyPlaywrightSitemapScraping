using Microsoft.Playwright;
using ShopifyPlaywrightSitemapScraping;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

public class SitemapScraper
{
    private readonly HttpClient _httpClient;
    private IPlaywright playwright;
    private IBrowser browser;
    private IPage page;
    private string baseUrl;

    public SitemapScraper()
    {
        _httpClient = new HttpClient();
        baseUrl = "https://www.kawaiies.com";
    }

    public async Task V()
    {
        playwright = await Playwright.CreateAsync();
        browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });
        page = await browser.NewPageAsync();
        await page.RouteAsync("**/*.js", route => route.AbortAsync());
    }

    public async Task Delete()
    {
        await browser.CloseAsync();
        playwright.Dispose();
        await browser.DisposeAsync();
    }

    public async ValueTask<string[]> Sitemap()
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

    public async ValueTask<string[]> Products(string collection)
    {
        await page.GotoAsync(collection);
        var m = await page.QuerySelectorAllAsync("urlset url loc");
        var textContents = await Task.WhenAll(m.Select(async element => await element.TextContentAsync()));
        return textContents.Where(x => !x.StartsWith("https://cdn.shopify.com")).Skip(1).ToArray();
    }
}
