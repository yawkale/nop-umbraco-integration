angular.module("umbraco")
.controller("NopProductPickerController", ['$scope', '$routeParams', 'productResource', function ($scope, $routeParams, productResource) {
    $scope.productName;

    var domElement = document.getElementsByClassName('umb-panel-header-name-input');
    $scope.pageScope = angular.element(domElement).scope()

    $scope.createProduct = function () {
        productResource.post($scope.pageScope.name).then((response) => {
            $scope.model.value = JSON.parse(response.data);
            $scope.getSource();
        })
    }
    $scope.updateProduct = function () {
        productResource.update($scope.model.value, $scope.pageScope.name).then((response) => {
            $scope.getSource();
        })
    }

    $scope.$on('update-' + $routeParams.id, function (event) {
        $scope.updateProduct()
    });

    $scope.$on('create-' + $routeParams.id, function (event) {
        $scope.createProduct()
    });
}]);