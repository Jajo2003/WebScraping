using System;

using Newtonsoft.Json;
using HtmlAgilityPack;

class WebScraping
{
    static string Content = @"
    <div class=""item"" rating = ""3"" data-pdid=""5426""><figure><a
  href=""https://www.100percent.co.nz/Product/WCM7000WD/Electrolux-700L-
  Chest-Freezer""><img alt=""Electrolux 700L Chest Freezer &amp; Filter""
  src=""/productimages/thumb/1/5426_5731_4009.jpg"" data-alternate-
  image=""/productimages/thumb/2/5426_5731_4010.jpg"" class=""mouseover-
  set""><span class=""overlay top-horizontal""><span class=""sold-out""><img
  alt=""Sold Out""
  Src=""/Images/Overlay/overlay_1_2_1.png""></span></span></a></figure><div
  class=""item-detail""><h4><a
  href=""https://www.100percent.co.nz/Product/WCM7000WD/Electrolux-700L-
  Chest-Freezer"">Electrolux 700L Chest Freezer</a></h4><div class=""pricing""
  itemprop=""offers"" itemscope=""itemscope""
  itemtype=""http://schema.org/Offer""><meta itemprop=""priceCurrency""
  content=""NZD""><p class=""price""><span class=""price-display formatted""
  itemprop=""price""><span style=""display: none"">$2,099.00</span>$<span
  class=""dollars over500"">2,099</span><span class=""cents
  zero"">.00</span></span></p></div><p class=""style-number"">WCM7000WD</p><p
  class=""offer""><a
  href=""https://www.100percent.co.nz/Product/WCM7000WD/Electrolux-700L-
  Chest-Freezer""><span style=""color:#CC0000;"">WCM7000WD</span></a></p><div
  class=""item-asset""><!--.--></div></div></div>
  <div class=""item"" rating = ""3.6"" data-pdid=""5862""><figure><a
  href=""https://www.100percent.co.nz/Product/E203S/Electrolux-Anti-Odour-
  Vacuum-Bags""><img alt=""Electrolux Anti-Odour Vacuum Bags""
  src=""/productimages/thumb/1/5862_6182_4541.jpg""></a></figure><div
  class=""item-detail""><h4><a
  href=""https://www.100percent.co.nz/Product/E203S/Electrolux-Anti-Odour-
  Vacuum-Bags"">Electrolux Anti-Odour Vacuum Bags</a></h4><div
  class=""pricing"" itemprop=""offers"" itemscope=""itemscope""
  itemtype=""http://schema.org/Offer""><meta itemprop=""priceCurrency""
  content=""NZD""><p class=""price""><span class=""price-display formatted""
  itemprop=""price""><span style=""display: none"">$22.99</span>$<span
  class=""dollars"">22</span><span class=""cents"">.99</span></span></p></div><p
  class=""style-number"">E203S</p><p class=""offer""><a
  href=""https://www.100percent.co.nz/Product/E203S/Electrolux-Anti-Odour-
  Vacuum-Bags""><span style=""color:#CC0000;"">E203S</span></a></p><div
  class=""item-asset""><!--.--></div></div></div>
  <div class=""item"" rating = ""8.4"" data-pdid=""4599""><figure><a
  href=""https://www.100percent.co.nz/Product/USK11ANZ/Electrolux-UltraFlex-
  Starter-Kit""><img alt=""Electrolux UltraFlex Starter &#91; Kit &#93; ""
  src=""/productimages/thumb/1/4599_4843_2928.jpg""></a></figure><div
  class=""item-detail""><h4><a
  href=""https://www.100percent.co.nz/Product/USK11ANZ/Electrolux-UltraFlex-
  Starter-Kit"">Electrolux UltraFlex &#64; Starter Kit</a></h4><div
  class=""pricing"" itemprop=""offers"" itemscope=""itemscope""
  itemtype=""http://schema.org/Offer""><meta itemprop=""priceCurrency""
  content=""NZD""><p class=""price""><span class=""price-display formatted""
  itemprop=""price""><span style=""display: none"">$44.99</span>$<span
  class=""dollars"">44</span><span class=""cents"">.99</span></span></p></div><p
  class=""style-number"">USK11ANZ</p><p class=""offer""><a
  href=""https://www.100percent.co.nz/Product/USK11ANZ/Electrolux-UltraFlex-
  Starter-Kit""><span style=""color:#CC0000;"">USK11ANZ</span></a></p><div
  class=""item-asset""><!--.--></div></div></div>";
    public static void Main(string[] args)
    {
        List<item> Items = new List<item>();

        HtmlDocument testDoc = new HtmlDocument();

        testDoc.LoadHtml(Content);

        var itemNodes = testDoc.DocumentNode.SelectNodes("//div[@class='item']");




        if (itemNodes.Count != 0)
        {
            foreach (var item in itemNodes)
            {
                var productname = item.SelectSingleNode(".//h4/a");
                
                var productnameTrimmed = productname?.InnerText.Trim();

                var productprice = item.SelectSingleNode(".//span[@class='price-display formatted']//span");
                string productpriceTrimmed = productprice?.InnerText.Trim();

                productpriceTrimmed =productpriceTrimmed.Replace(",","").Replace("$",""); 
               
                var productrating = float.Parse(item.GetAttributeValue("rating", ""));

                if (productrating > 5)
                {
                    Random randomnum = new Random();
                    productrating = (float)Math.Round((randomnum.NextDouble() * 4 + 1),1);
                    
                }


                float itemtoFloat;
                if (!float.TryParse(productpriceTrimmed, out itemtoFloat))
                {
                    Console.WriteLine("Wrong price detected on this item:" +productname);
                    continue;
                }
                float.TryParse(productpriceTrimmed, out itemtoFloat);

                Items.Add(new item
                {
                    productName = productnameTrimmed,
                    price = itemtoFloat,
                    rating = productrating
                });


            }

           
            foreach (var i in Items)
            {
                Console.WriteLine(i.productName);
                Console.WriteLine(i.price);
                Console.WriteLine(i.rating);
            }

            Console.WriteLine();
            var jsonArray = JsonConvert.SerializeObject(Items);
            Console.WriteLine(jsonArray);
        }
    }






}

class item {
    public string? productName;
    public float? price;
    public float? rating;


}