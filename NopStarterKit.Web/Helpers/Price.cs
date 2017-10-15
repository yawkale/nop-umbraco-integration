using Nop.Integration.Umbraco.Nop;
using System;

namespace NopStarterKit.Web.Helpers
{
    public static class Price
    {
        private static readonly NopApiService _nopService;

        static Price()
        {
            _nopService = new NopApiService();
        }

        public static string GetPrice(int id)
        {
            var price = _nopService.GetProductPrice(id);

            return String.Format("{0:n}", price); 
        }
    }
}