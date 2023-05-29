using Newtonsoft.Json;
using ProductLibrary.Models;

namespace Store.API.FakeData
{
    public class Filebase
    {
        private string _root;
        private string _invQuantityRoot;
        private string _invWeightRoot;
        private string _cartQuantityRoot;
        private string _cartWeightRoot;
        private string _cartSaves;
        
        private static Filebase _instance;
        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = $"{Environment.CurrentDirectory}/StoreData";
            if (!Directory.Exists(_root))
                Directory.CreateDirectory(_root);
            _invQuantityRoot = $"{_root}/InventoryByQuantity";
            if(!Directory.Exists(_invQuantityRoot))
                Directory.CreateDirectory(_invQuantityRoot);
            _invWeightRoot = $"{_root}/InventoryByWeight";
            if(!Directory.Exists(_invWeightRoot))
                Directory.CreateDirectory(_invWeightRoot);
            _cartQuantityRoot = $"{_root}/CartByQuantity";
            if(!Directory.Exists(_cartQuantityRoot))
                Directory.CreateDirectory(_cartQuantityRoot);
            _cartWeightRoot = $"{_root}/CartByWeight";
            if(!Directory.Exists(_cartWeightRoot))
                Directory.CreateDirectory(_cartWeightRoot);
            _cartSaves = $"{_root}/CartSaves";
            if (!Directory.Exists(_cartSaves))
                Directory.CreateDirectory(_cartSaves);
            
        }
        
        public List<ProductByWeight> InventoryByWeight
        {
            get
            {
                var root = new DirectoryInfo(_invWeightRoot);
                var _weights = new List<ProductByWeight>();
                if (root.Exists)
                {
                    foreach(var weightFile in root.GetFiles())
                    {
                        var todo = JsonConvert.DeserializeObject<ProductByWeight>(File.ReadAllText(weightFile.FullName));
                        _weights.Add(todo);
                    }
                }
                return _weights;
            }
        }

        public List<ProductByQuantity> InventoryByQuantity
        {
            get
            {
                var root = new DirectoryInfo(_invQuantityRoot);
                var _quantities = new List<ProductByQuantity>();
                if (root.Exists)
                {
                    foreach (var quantityFile in root.GetFiles())
                    {
                        var app = JsonConvert.DeserializeObject<ProductByQuantity>(File.ReadAllText(quantityFile.FullName));
                        _quantities.Add(app);
                    }
                    
                }
                
                return _quantities;
            }
        }
        
        public List<Product> Inventory
        {
            get
            {
                var returnList = new List<Product>();
                InventoryByWeight.ForEach(returnList.Add);
                InventoryByQuantity.ForEach(returnList.Add);
                return returnList;

            }
        }
        
        public int DeleteInventory(int Id)
        {
            var product = Inventory.FirstOrDefault(i => i.Id == Id);
            if (product != null)
            {
                string path;
                if (product is ProductByWeight) 
                    path = $"{_invWeightRoot}/{product.Id}.json";
                else
                    path = $"{_invQuantityRoot}/{product.Id}.json";
                if(File.Exists(path))
                {
                    File.Delete(path);
                    return Id;
                }
                    
            }
            return 0;

        }

        public Product AddOrUpdateInv(Product product)
        {
            //set up a new Id if one doesn't already exist
            
            if (product.BoGo)
            {
                if(!product.Name.EndsWith(" (BoGo!)"))
                    product.Name += " (BoGo!)";
            }
            
            if (product.Id <= 0)
            {
                product.Id = NextId;
            }
            
            //go to the right place]
            string path;
            if (product is ProductByWeight)
            {
                path = $"{_invWeightRoot}/{product.Id}.json";
            } else
            {
                path = $"{_invQuantityRoot}/{product.Id}.json";
            }

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(product));

            //return the item, which now has an id
            return product;
        }

        public List<ProductByWeight> CartByWeight
        {
            get
            {
                var root = new DirectoryInfo(_cartWeightRoot);
                var _weights = new List<ProductByWeight>();
                if (root.Exists)
                {
                    foreach(var weightFile in root.GetFiles())
                    {
                        var todo = JsonConvert.DeserializeObject<ProductByWeight>(File.ReadAllText(weightFile.FullName));
                        _weights.Add(todo);
                    }
                }
                return _weights;
                
            }
        }
        
        public List<ProductByQuantity> CartByQuantity
        {
            get
            {
                var root = new DirectoryInfo(_cartQuantityRoot);
                var _quantities = new List<ProductByQuantity>();
                if (root.Exists)
                {
                    foreach (var quantityFile in root.GetFiles())
                    {
                        var app = JsonConvert.DeserializeObject<ProductByQuantity>(File.ReadAllText(quantityFile.FullName));
                        _quantities.Add(app);
                    }
                    
                }
                
                return _quantities;
            }
        }
        
        public List<Product> Cart
        {
            get
            {
                var returnList = new List<Product>();
                CartByWeight.ForEach(returnList.Add);
                CartByQuantity.ForEach(returnList.Add);
                return returnList;

            }
        }

        public Product AddCart(Product product)
        {
            var toUpdateCart = Cart.FirstOrDefault(t => t.Id == product.Id);
            var toUpdateInv = Inventory.FirstOrDefault(t => t.Id == product.Id);
            string path;
            if (product is ProductByWeight)
            {
                (toUpdateInv as ProductByWeight).Weight -= (product as ProductByWeight).Weight;
                path = $"{_cartWeightRoot}/{product.Id}.json";
            } else
            {
                (toUpdateInv as ProductByQuantity).Quantity -= (product as ProductByQuantity).Quantity;
                path = $"{_cartQuantityRoot}/{product.Id}.json";
            }
            AddOrUpdateInv(toUpdateInv);
            if(File.Exists(path))
            {
                if (toUpdateCart is ProductByWeight)
                    (toUpdateCart as ProductByWeight).Weight += (product as ProductByWeight).Weight;
                else
                    (toUpdateCart as ProductByQuantity).Quantity += (product as ProductByQuantity).Quantity;
                //blow it up
                File.Delete(path);
                File.WriteAllText(path, JsonConvert.SerializeObject(toUpdateCart));
            }
            else
                File.WriteAllText(path, JsonConvert.SerializeObject(product));
            
            return product;

        }

        public int DeleteCart(int id)
        {
            var toDeleteCart = Cart.FirstOrDefault(t => t.Id == id);
            if (toDeleteCart != null)
            { 
                var toUpdateInv = Inventory.FirstOrDefault(t => t.Id == id);
                string path;
                if (toDeleteCart is ProductByWeight)
                {
                    (toUpdateInv as ProductByWeight).Weight += (toDeleteCart as ProductByWeight).Weight;
                    path = $"{_cartWeightRoot}/{toDeleteCart.Id}.json";
                } else
                {
                    (toUpdateInv as ProductByQuantity).Quantity += (toDeleteCart as ProductByQuantity).Quantity;
                    path = $"{_cartQuantityRoot}/{toDeleteCart.Id}.json";
                }
                AddOrUpdateInv(toUpdateInv);
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return id;
                }
            }
            return 0;
        }

        public Dictionary<string, List<Product>> CartSavesList
        {
            get
            {
                var root = new DirectoryInfo(_cartSaves);
                var _saves = new Dictionary<string, List<Product>>();
                if (root.Exists)
                {
                    foreach (var saveFile in root.GetDirectories())
                    {
                        foreach (var list in saveFile.GetFiles())
                        {
                            var cart = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(list.FullName));
                            _saves.Add(saveFile.Name,cart);
                        }
                    }
                }
                return _saves;
            }

        }

        public List<string> SavesList()
        {
            var saveList = new List<string>();
            foreach (var keyValuePair in CartSavesList)
            {
                saveList.Add(keyValuePair.Key);
            }
            return saveList;
        }

        public string SaveCart(KeyValuePair<string, List<Product>> pair)
        {
            if (!string.IsNullOrEmpty(pair.Key))
            {
                var savefile = $"{_cartSaves}/{pair.Key}";
                if (!Directory.Exists(savefile))
                    Directory.CreateDirectory(savefile);
                var path = $"{savefile}/{pair.Key}.json";
                if(File.Exists(path))
                    File.Delete(path);
                File.WriteAllText(path, JsonConvert.SerializeObject(Cart));
            }
            return pair.Key;
        }

        public List<Product> LoadCart(KeyValuePair<string, List<Product>> pair)
        {
            if(!string.IsNullOrEmpty(pair.Key))
            {
                var savedCart = CartSavesList.FirstOrDefault(i => i.Key.Equals(pair.Key));
                if (!string.IsNullOrEmpty(savedCart.Key))
                    return savedCart.Value;
            }
            return null;
        }
        
        public string DeleteSave(KeyValuePair<string, List<Product>> pair)
        {
            if(!string.IsNullOrEmpty(pair.Key))
            {
                var savefile = $"{_cartSaves}/{pair.Key}";
                if (Directory.Exists(savefile))
                {
                    var file = $"{savefile}/{pair.Key}.json";
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                        Directory.Delete(savefile);
                        return pair.Key;
                    }
                }
               
            }
            return null;
        }

        public string CartClear()
        {
            var root = new DirectoryInfo(_cartQuantityRoot);
            foreach (var file in root.GetFiles())
            {
                File.Delete($"{file.FullName}");
            }
            root = new DirectoryInfo(_cartWeightRoot);
            foreach (var file in root.GetFiles())
            {
                File.Delete($"{file.FullName}");
            }
            return "Cleared";
        }

        public int NextId
        {
            get
            {
                if (!Inventory.Any()) 
                    return 1;
                return Inventory.Select(t => t.Id).Max() + 1;
                
            }
            
        }
    }
}
