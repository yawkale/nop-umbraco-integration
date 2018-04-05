using Nop.Api.Adapter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Integration.Umbraco.Order;
using Nop.Integration.Umbraco.ShoppingCart;
using Nop.Integration.Umbraco.Category;
using Nop.Integration.Umbraco.Orders;
using Nop.Integration.Umbraco.Products;

namespace Nop.Integration.Umbraco.Nop
{
    public interface INopApiService
    {
        Products.Product GetProduct(int id);

        decimal GetProductPrice(int id);

        string CreateCustomer(Customer.Customer customer);

        ShoppingCartRootObject GetShoppingCart(string customerId);

        ShoppingCartRootObject UpdateShoppingCart(ShoppingCartItem item);

        ShoppingCartRootObject CreateShoppingCart(CreateShoppingCartItem item);

        void RemoveShoppingCartItem(int shoppingCartItemId);

        string UpdateCustomer(Customer.Customer customer, string id);

        List<Orders.Order> GetAllOrders();
        Orders.Order CreateOrder(Orders.Order order);

        string CreateProduct(PostProductObject prod);

        string CreateCategory(PostCategoryObject product);

        string UpdateCategory(PostCategoryObject category);

        void UpdateProduct(PostProductObject product);
        Orders.Order UpdateOrder(Orders.Order order);
        Orders.Order GetOrder(int id);

    }

    public class NopApiService : INopApiService
    {
        private readonly ApiClient _nopApiClient;
        public NopApiService()
        {
           _nopApiClient = new ApiClient();
        }

        public Products.Product GetProduct(int id)
        {
            if (id == 0)
            {
                throw  new ArgumentNullException(nameof(id),"id must be greater that 0");
            }
            string jsonUrl = $"/api/products/{id}?fields=id,name,price,category_id,images,attributes,order_minimum_quantity,is_gift_card,is_download,customer_enters_price,is_rental,has_tier_prices";

            var productData = _nopApiClient.Get(jsonUrl);

            var product = JsonConvert.DeserializeObject<ProductRootObject>(productData.ToString())?.Products.FirstOrDefault();

            if (product == null)
            {
                throw new Exception($"Product not found {id}");
            }
            else
            {
                product.Attributes = product.Attributes ?? new List<ProductAttributeMapping>();

                return product;
            }
        }

        public string CreateProduct(PostProductObject product)
        {
            string jsonCustomer = JsonConvert.SerializeObject(new
            {
                product = product
            });

            string jsonUrl = $"/api/products";

            object productData = _nopApiClient.Post(jsonUrl, jsonCustomer);

            var jo = JObject.Parse(productData.ToString());

            var data = jo["products"][0]["id"].ToString();

            return data;
        }

        public List<Products.Product> GetAllProducts()
        {
            string jsonUrl = $"/api/products?limit=250&fields=id,name,price,category_id,images,attributes,sku,order_minimum_quantity,is_gift_card,is_download,customer_enters_price,is_rental,has_tier_prices,store_ids";

            object productData = _nopApiClient.Get(jsonUrl);

            var products = JsonConvert.DeserializeObject<ProductRootObject>(productData.ToString())?.Products;
  
            return products;
        }

        public CategoriesRootObject GetCategories()
        {
            string jsonUrl = $"/api/categories?fields=id,name,store_ids";

            object categoriesData = _nopApiClient.Get(jsonUrl);

            var categories = JsonConvert.DeserializeObject<CategoriesRootObject>(categoriesData.ToString());

            return categories;
        }

        public string CreateCategory(PostCategoryObject category)
        {
            string jsonCategory = JsonConvert.SerializeObject(new
            {
                category = category
            });

            string jsonUrl = $"/api/categories";

            object categoryData = _nopApiClient.Post(jsonUrl, jsonCategory);

            var jo = JObject.Parse(categoryData.ToString());

            var data = jo["categories"][0]["id"].ToString();

            return data;
        }

        public string UpdateCategory(PostCategoryObject category)
        {
            string jsonCategory = JsonConvert.SerializeObject(new
            {
                category = category
            });

            string jsonUrl = $"/api/categories/{category.Id}";

            object categoryData = _nopApiClient.Put(jsonUrl, jsonCategory);

            var jo = JObject.Parse(categoryData.ToString());

            var data = jo["categories"][0]["id"].ToString();

            return data;
        }

        public void UpdateProduct(PostProductObject product)
        {
            string jsonProduct = JsonConvert.SerializeObject(new
            {
                product = product
            });

            string jsonUrl = $"/api/products/{product.Id}";

            _nopApiClient.Put(jsonUrl, jsonProduct);
        }



        #region OrderService

        public Orders.Order UpdateOrder(Orders.Order order)
        {
         
            string jsonOrder = JsonConvert.SerializeObject(new
            {
                order = order
            });

            var id = order.Id;

            string jsonUrl = $"/api/orders/{id}";

            var orderData =  _nopApiClient.Put(jsonUrl, jsonOrder);

            var newOrder = JsonConvert.DeserializeObject<OrdersRootObject>(orderData.ToString());
            return newOrder.Orders.FirstOrDefault();
        }

        public List<Orders.Order> GetAllOrders()
        {
            string jsonUrl = $"/api/orders?fields=id,order_total,paid_date_utc,payment_status,customer";

            object productData = _nopApiClient.Get(jsonUrl);

            var orders = JsonConvert.DeserializeObject<OrdersRootObject>(productData.ToString())?.Orders;

            return orders;
        }

        public Orders.Order GetOrder(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id), "id must be greater that 0");
            }
            string jsonUrl = $"/api/orders/{id}";

            var orderData = _nopApiClient.Get(jsonUrl);

            var order = JsonConvert.DeserializeObject<OrdersRootObject>(orderData.ToString())?.Orders.FirstOrDefault();

            if (order == null)
            {
                throw new Exception($"Order not found {id}");
            }
            return order;
        }


        public Orders.Order CreateOrder(Orders.Order order)
        {
            string jsonUrl = "api/orders";

            string jsonOrder = JsonConvert.SerializeObject(new
            {
                order = order
            });
            object orderData = _nopApiClient.Post(jsonUrl, jsonOrder);
            var newOrder = JsonConvert.DeserializeObject<OrdersRootObject>(orderData.ToString());
            return newOrder.Orders.FirstOrDefault();
        }

        #endregion


        public CustomersRootObject GetCustomers()
        {
            string jsonUrl = $"/api/customers?fields=first_name,last_name,id,email";

            object response = _nopApiClient.Get(jsonUrl);

            var customers = JsonConvert.DeserializeObject<CustomersRootObject>(response.ToString());

            return customers;
        }

        public decimal GetProductPrice(int id)
        {
            string jsonUrl = $"/api/products/{id}?fields=price";

            var productData = _nopApiClient.Get(jsonUrl);

            var jo = JObject.Parse(productData.ToString());

            var data = jo["products"][0]["price"].ToString();

            return Convert.ToDecimal(data);
        }

        public string CreateCustomer(Customer.Customer customer)
        {
            string jsonCustomer = JsonConvert.SerializeObject(new
            {
                customer = customer
            });

            string jsonUrl = $"/api/customers";

            object productData = _nopApiClient.Post(jsonUrl, jsonCustomer);

            var jo = JObject.Parse(productData.ToString());

            var data = jo["customers"][0]["id"].ToString();

            return data;
        }

        public string UpdateCustomer(Customer.Customer customer, string id)
        {
            string jsonCustomer = JsonConvert.SerializeObject(new
            {
                customer = customer
            });

            string jsonUrl = $"/api/customers/{id}";

            object productData = _nopApiClient.Put(jsonUrl, jsonCustomer);

            var jo = JObject.Parse(productData.ToString());

            var data = jo["customers"][0]["id"].ToString();

            return data;
        }

        public ShoppingCartRootObject GetShoppingCart(string customerId)
        {
            string jsonUrl = $"/api/shopping_cart_items/{customerId}";

            object productData = _nopApiClient.Get(jsonUrl);

            return JsonConvert.DeserializeObject<ShoppingCartRootObject>(productData.ToString());
        }

        public ShoppingCartRootObject UpdateShoppingCart(ShoppingCartItem item)
        {
            string jsonShoppingCart = JsonConvert.SerializeObject(new
            {
                shopping_cart_item = item
            });

            var json = JObject.Parse(jsonShoppingCart);

            json.Descendants()
                .OfType<JProperty>()
                .Where(attr => attr.Name.StartsWith("images"))
                .ToList()
                .ForEach(attr => attr.Remove());
            jsonShoppingCart = json.ToString();

            string jsonUrl = $"/api/shopping_cart_items/{item.Id}";

            object productData = _nopApiClient.Put(jsonUrl, jsonShoppingCart);

            return JsonConvert.DeserializeObject<ShoppingCartRootObject>(productData.ToString());
        }

        public ShoppingCartRootObject CreateShoppingCart(CreateShoppingCartItem shoppingCartItem)
        {
            string shoppingCartItemJson = JsonConvert.SerializeObject(new
            {
                shopping_cart_item = shoppingCartItem
            });

            string jsonUrl = $"/api/shopping_cart_items";

            object productData = _nopApiClient.Post(jsonUrl, shoppingCartItemJson);

            var shoppingCart = JsonConvert.DeserializeObject<ShoppingCartRootObject>(productData.ToString());

            return shoppingCart;
        }

        public void RemoveShoppingCartItem(int shoppingCartItemId)
        {
            string jsonUrl = $"/api/shopping_cart_items/{shoppingCartItemId}";

            _nopApiClient.Delete(jsonUrl);
        }

        //private string SearchCategoryByName(string name)
        //{
        //    string jsonUrl = $"/api/categories/search";

        //    object categoriesData =_nopApiClient.Get(jsonUrl);

        //    var categories = JsonConvert.DeserializeObject<CategoriesRootObject>(categoriesData.ToString());
        //    var jo = JObject.Parse(categoriesData.ToString());

        //    return "sas";
        //}
    }
}
