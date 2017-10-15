angular.module("umbraco.resources")
.factory("customerResource", function ($http) {
    return {
        get: function () {
            return $http.get("/umbraco/api/NopCustomerApi/GetCustomers");
        }
    };
});