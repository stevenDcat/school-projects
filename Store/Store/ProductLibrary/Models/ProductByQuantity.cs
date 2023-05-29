using Newtonsoft.Json;
using ProductLibrary.Utility;

namespace ProductLibrary.Models
{
    [JsonConverter(typeof(ProductJsonConverter))]
    public class ProductByQuantity : Product
    {
        public int Quantity { set; get; }
        public double Price { set; get; }

        public double TotalPrice
        {
            get
            {
                if (BoGo)
                    return Quantity * Price / 2;
                return Quantity * Price;
            }
        }
        
        
        public override string ToString()
        {
            return $"Id: {Id} - Name: {Name} - Description: {Description} \n" +
                   $"Price per unit: ${Price.ToString("F")} - Units: {Quantity}";
        }
    }
}