using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace AcquireXModel
{
    [XmlRoot(ElementName = "product")]

    public class ProductInfo
    {
        [JsonProperty("itemCode")]
        [XmlElement(ElementName = "itemCode")]

        public string ItemCode { get; set; }
        [JsonProperty("name")]
        [XmlElement(ElementName = "name")]

        public string Name { get; set; }
        [JsonProperty("manufacturer")]
        [XmlElement(ElementName = "manufacturer")]
        public string Manufacturer { get; set; }
        [JsonProperty("brand")]
        [XmlElement(ElementName = "brand")]

        public string Brand { get; set; }
        [XmlElement(ElementName = "price")]
        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("upc")]
        [XmlElement(ElementName = "upc")]
        public double Upc { get; set; }
        [JsonProperty("mpn")]
        public string Mnp { get; set; }


    }

    [XmlRoot(ElementName = "ProductsResponse")]

    public class Products
    {
        [XmlElement(ElementName = "product")]
        [JsonProperty("products")]
        public List<ProductInfo> ProductList { get; set; }
    }
}