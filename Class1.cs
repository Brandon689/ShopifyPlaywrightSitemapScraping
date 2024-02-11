

using System.Xml.Serialization;

[XmlRoot(ElementName = "url")]
public class Url
{

    [XmlElement(ElementName = "loc")]
    public string Loc { get; set; }

    [XmlElement(ElementName = "changefreq")]
    public string Changefreq { get; set; }

    [XmlElement(ElementName = "lastmod")]
    public DateTime Lastmod { get; set; }

    [XmlElement(ElementName = "image")]
    public Image Image { get; set; }
}

[XmlRoot(ElementName = "image")]
public class Image
{

    [XmlElement(ElementName = "loc")]
    public string Loc { get; set; }

    [XmlElement(ElementName = "title")]
    public string Title { get; set; }

    [XmlElement(ElementName = "caption")]
    public string Caption { get; set; }
}

[XmlRoot(ElementName = "urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
public class Urlset
{

    [XmlElement(ElementName = "url")]
    public List<Url> Url { get; set; }

    [XmlAttribute(AttributeName = "xmlns")]
    public string Xmlns { get; set; }

    [XmlAttribute(AttributeName = "image")]
    public string Image { get; set; }

    [XmlText]
    public string Text { get; set; }
}

