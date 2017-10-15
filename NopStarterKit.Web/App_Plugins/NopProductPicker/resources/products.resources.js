angular.module("umbraco.resources")
.factory("productResource", function ($http) {
    return {
        get: function () {
            return $http.get("/umbraco/api/NopProductsApi/GetProducts");
        },
        post: function (data) {
            return $http.post("/umbraco/api/NopProductsApi/Create", '"' + data + '"');
        },
        update: function (id, name) {
            var params = { 'id': id, 'name': name }
            return $http.post("/umbraco/api/NopProductsApi/Update/", params);
        }
    };
});