angular.module("umbraco.resources")
.factory("orderResource", function ($http) {
    return {
        get: function () {
            return $http.get("/umbraco/api/NopOrdersApi/GetOrders");
        },
    };
});