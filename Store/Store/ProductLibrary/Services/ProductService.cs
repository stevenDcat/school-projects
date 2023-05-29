using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ProductLibrary.Models;
using ProductLibrary.Utility;

namespace ProductLibrary.Services
{
    public class ProductService
    {
        private string SERVER = "http://10.0.2.2:7016";
        public List<string> CartSaves { get; set; }
        private List<Product> Inventory { get; set; }
        public List<Product> inventoryList
        {
            get
            {
                return Inventory;
            }
        }
        private List<Product> Cart { get; set; }
        public List<Product> cartList
        {
            get => Cart;
        }

        private static ProductService current;
        public int NextId
        {
            get
            {
                if (!Inventory.Any()) return 1;

                return Inventory.Select(t => t.Id).Max() + 1;
            }
        }
        public static ProductService Current
        {
            get
            {
                if (current == null) current = new ProductService();

                return current;
            }
        }
        private ProductService()
        {
            var inventoryJson = new WebRequestHandler().Get($"{SERVER}/Product/Inventory").Result;
            Inventory = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);
            
            var cartJson = new WebRequestHandler().Get($"{SERVER}/Product/Cart").Result;
            Cart = JsonConvert.DeserializeObject<List<Product>>(cartJson);

            var savesJson = new WebRequestHandler().Get($"{SERVER}/Product/SavesList").Result;
            CartSaves = JsonConvert.DeserializeObject<List<string>>(savesJson);
        }
        public void AddOrUpdateInventory(Product product)
        {
            if (product is ProductByQuantity)
            {
                var response =
                    new WebRequestHandler().Post($"{SERVER}/ByQuantity/AddOrUpDate", product as ProductByQuantity).Result;
                var newProduct = JsonConvert.DeserializeObject<ProductByQuantity>(response);

                var oldProduct = Inventory.FirstOrDefault(i => i.Id == newProduct.Id);
                if (oldProduct != null)
                {
                    var indx = Inventory.IndexOf(oldProduct);
                    Inventory.RemoveAt(indx);
                    Inventory.Insert(indx, newProduct);

                }
                else
                    Inventory.Add(newProduct);
            }
            else if (product is ProductByWeight)
            {
                var response =
                    new WebRequestHandler().Post($"{SERVER}/ByWeight/AddOrUpDate", product as ProductByWeight).Result;
                var newProduct = JsonConvert.DeserializeObject<ProductByWeight>(response);
                var oldProduct = Inventory.FirstOrDefault(i => i.Id == newProduct.Id);
                if (oldProduct != null)
                {
                    var indx = Inventory.IndexOf(oldProduct);
                    Inventory.RemoveAt(indx);
                    Inventory.Insert(indx, newProduct);

                }
                else
                    Inventory.Add(newProduct);
                
            }

           
        }
        public void DeleteFromInventory(Product product)
        {
            string response = string.Empty;
            if (product is ProductByQuantity)
                response = new WebRequestHandler().Get( $"{SERVER}/ByQuantity/DeleteInventory/{product.Id}").Result;
            else if (product is ProductByWeight)
                response = new WebRequestHandler().Get($"{SERVER}/ByWeight/DeleteInventory/{product.Id}").Result; 
            int i = int.Parse(response ?? "0");
            if (i > 0)
            {
                var ToDelete = Inventory.FirstOrDefault(t => t.Id == product.Id);
                if (ToDelete != null)
                {
                    Inventory.Remove(ToDelete);
                }
            }
            
            
           
            
        }
        public void AddOrUpdateCart(Product product)
        {

            string response = String.Empty;
            Product newProduct = null;
            if (product is ProductByQuantity)
            {
                response = new WebRequestHandler().Post($"{SERVER}/ByQuantity/AddCart", product as ProductByQuantity).Result;
                newProduct = JsonConvert.DeserializeObject<ProductByQuantity>(response);
            }
            else if (product is ProductByWeight)
            {
                response = new WebRequestHandler().Post($"{SERVER}/ByWeight/AddCart", product as ProductByWeight).Result;
                newProduct = JsonConvert.DeserializeObject<ProductByWeight>(response);
            }
            
            var toUpdateCart = Cart.FirstOrDefault(t => t.Id == newProduct.Id);
            var toUpdateInv = Inventory.FirstOrDefault(t => t.Id == newProduct.Id);
            if (toUpdateCart == null)
            {
                Cart.Add(newProduct);
                if (newProduct is ProductByQuantity)
                {
                    (toUpdateInv as ProductByQuantity).Quantity -= (product as ProductByQuantity).Quantity;
                }
                else if (newProduct is ProductByWeight)
                {
                    (toUpdateInv as ProductByWeight).Weight -= (product as ProductByWeight).Weight;
                }
            }
            else
            {
                if (toUpdateCart is ProductByQuantity)
                {
                    (toUpdateCart as ProductByQuantity).Quantity += (product as ProductByQuantity).Quantity;
                    (toUpdateInv as ProductByQuantity).Quantity -= (product as ProductByQuantity).Quantity;
                }
                   
                else if (toUpdateCart is ProductByWeight)
                {
                    (toUpdateCart as ProductByWeight).Weight += (product as ProductByWeight).Weight;
                    (toUpdateInv as ProductByWeight).Weight -= (product as ProductByWeight).Weight;
                }
                
            }
        }
        public void DeleteFromCart(Product product)
        {
            string response = String.Empty;
            if (product is ProductByQuantity)
                response = new WebRequestHandler().Get($"{SERVER}/ByQuantity/DeleteCart/{product.Id}").Result;
            else if (product is ProductByWeight)
                response = new WebRequestHandler().Get($"{SERVER}/ByWeight/DeleteCart/{product.Id}").Result;
            int i = int.Parse(response ?? "0");
            
            if (i > 0)
            {
                var toUpdateCart = Cart.FirstOrDefault(t => t.Id == i);
                var toUpdateInv = Inventory.FirstOrDefault(t => t.Id == i);
                if (product is ProductByQuantity)
                {
                    (toUpdateInv as ProductByQuantity).Quantity += (product as ProductByQuantity).Quantity;
                    Cart.Remove(toUpdateCart);
                }
                else if (product is ProductByWeight)
                {
                    (toUpdateInv as ProductByWeight).Weight += (product as ProductByWeight).Weight;
                    Cart.Remove(toUpdateCart);
                }
            }
                
        }
        public string CartCheckout()
        {
            if (!Cart.Any())
            {
                return "The Cart is empty!";
            }
            else
            {
                double subtotal = 0;
                foreach (var product in Cart)
                    subtotal += product.TotalPrice;
                var tax = subtotal * 0.07;
                var total = tax + subtotal;
                
                return $"Subtotal:\t${subtotal.ToString("F")}\nTaxes:\t\t${tax.ToString("F")}\n" +
                       $"Total:\t\t${total.ToString("F")}";
            }
        }


        public void CartLoad(string filename = null)
        {
            var test = new KeyValuePair<string, List<Product>>(filename, Cart);
            var response = new WebRequestHandler().Post($"{SERVER}/Product/LoadCart", test).Result;
            if (!string.IsNullOrEmpty(response))
            {
                var newCart = JsonConvert.DeserializeObject<List<Product>>(response);
                while (Cart.Any())
                {
                    DeleteFromCart(Cart.First());
                }
                
                for(int i = 0; i < newCart.Count; i++)
                {
                    AddOrUpdateCart(newCart[i]);
                }
            }
        }
        
        public void CartSave(string filename = null)
        {
            var test = new KeyValuePair<string, List<Product>>(filename, Cart);
            var response = new WebRequestHandler().Post($"{SERVER}/Product/SaveCart", test).Result;
            CartSaves.Add(response);
           
        }
        

        public void RemoveCartSave(string filename)
        {
            var test = new KeyValuePair<string, List<Product>>(filename, Cart);
            var response = new WebRequestHandler().Post($"{SERVER}/Product/DeleteSave", test).Result;
            if (!string.IsNullOrEmpty(response))
            {
                CartSaves.Remove(response);
            }
        }

        public void Pay()
        {
            var response = new WebRequestHandler().Get($"{SERVER}/Product/Pay").Result;
            if(response.Equals("Cleared"))
                Cart.Clear();
        }
    }
}