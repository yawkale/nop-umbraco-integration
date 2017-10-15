angular.module("umbraco.resources")
.factory("categoryResource", function ($http) {
    return {
        get: function () {
            return $http.get("/umbraco/api/NopCategoryApi/GetCategories");
        },
        post: function (data) {
            return $http.post("/umbraco/api/NopCategoryApi/Create", '"' + data + '"');
        },
        update: function (id, name) {
            var params = { 'id': id, 'name': name }
            return $http.post("/umbraco/api/NopCategoryApi/Update/", params);
        }
    };
});