using Commerce.Api.Adapter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nop.Integration.Umbraco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Integration.Umbraco.Nop
{
    public interface INopApiService
    {
        Product GetProduct(int id);

        decimal GetProductPrice(int id);

        string CreateCustomer(Customer customer);

        ShoppingCartRootObject GetShoppingCart(string customerId);

        ShoppingCartRootObject UpdateShoppingCart(ShoppingCartItem item);

        ShoppingCartRootObject CreateShoppingCart(CreateShoppingCartItem item);

        void RemoveShoppingCartItem(int shoppingCartItemId);

        string UpdateCustomer(Customer customer, string id);
    }

    public class NopApiService : INopApiService
    {
        private readonly ApiClient _nopApiClient;

        public NopApiService()
        {
           _nopApiClient = new ApiClient(AccessProvider._token, AccessProvider._serverUrl);
        }

        public Product GetProduct(int id)
        {
            string jsonUrl = $"/api/products/{id}?fields=id,name,price,category_id,images,attributes,order_minimum_quantity";

            object productData = _nopApiClient.Get(jsonUrl);

            return JsonConvert.DeserializeObject<ProductRootObject>(productData.ToString())?.Products.FirstOrDefault();
        }

        public decimal GetProductPrice(int id)
        {
            string jsonUrl = $"/api/products/{id}?fields=price";

            var productData = _nopApiClient.Get(jsonUrl);

            var jo = JObject.Parse(productData.ToString());

            var data = jo["products"][0]["price"].ToString();

            return Convert.ToDecimal(data);
        }

        public string CreateCustomer(Customer customer)
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

        public string UpdateCustomer(Customer customer, string id)
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
            var nopApiClient = new ApiClient(AccessProvider._token, AccessProvider._serverUrl);

            string jsonUrl = $"/api/shopping_cart_items/{shoppingCartItemId}";

            nopApiClient.Delete(jsonUrl);
        }
    }
}
