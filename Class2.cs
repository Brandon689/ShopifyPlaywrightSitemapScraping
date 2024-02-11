using System.Xml.Serialization;

namespace ShopifyPlaywrightSitemapScraping
{
    [XmlRoot(ElementName = "sitemap")]
    public class Sitemap
    {
        [XmlElement(ElementName = "loc")]
        public string Loc { get; set; }
    }

    [XmlRoot(ElementName = "sitemapindex")]
    public class Sitemapindex
    {

        [XmlElement(ElementName = "sitemap")]
        public List<Sitemap> Sitemap { get; set; }

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
