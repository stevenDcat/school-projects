using Newtonsoft.Json;
using ProductLibrary.Utility;

namespace ProductLibrary.Models
{
    [JsonConverter(typeof(ProductJsonConverter))]
    public class ProductByWeight : Product
    {
        public double Weight { set; get; }
        public double Price { set; get; }

        public new double TotalPrice
        {
            get
            {
                if (BoGo)
                    return Weight * Price / 2;
                return Weight * Price;
            }
        }

        public override string ToString()
        {
            return $"Id: {Id} - Name: {Name} - Description: {Description} \n" +
                   $"Price per pound: ${Price.ToString("F")} - Pounds: {Weight.ToString("F")}\n";
        }
    }
}