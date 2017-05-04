angular.module("umbraco.resources")
.factory("productResource", function ($http) {
    return {
        get: function () {
            return $http.get("/umbraco/api/NopProductsApi/GetProducts");
        }
    };
});