using Newtonsoft.Json;
using ProductLibrary.Utility;
namespace ProductLibrary.Models
{
    [JsonConverter(typeof(ProductJsonConverter))]
    public class Product
    {
        public bool BoGo;
        public string Name { set; get; }
        public string Description { set; get; }
        public int Id { get; set; }

        public double Price
        {
            get
            {
                if (this is ProductByQuantity)
                    return (this as ProductByQuantity).Price;
                else if (this is ProductByWeight)
                    return (this as ProductByWeight).Price;
                else
                    return 0;
            }
        }
        public double TotalPrice
        {
            get
            {
                if (this is ProductByQuantity)
                    return (this as ProductByQuantity).TotalPrice;
                else if (this is ProductByWeight)
                    return (this as ProductByWeight).TotalPrice;
                else
                    return 0;
            }
        }
        


    }
}