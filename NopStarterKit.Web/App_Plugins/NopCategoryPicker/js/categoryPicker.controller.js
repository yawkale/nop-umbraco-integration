angular.module("umbraco")
.controller("NopCategoryPickerController", function ($scope, categoryResource, $routeParams) {
    $scope.categoryName;

    var domElement = document.getElementsByClassName('umb-panel-header-name-input');
    $scope.pageScope = angular.element(domElement).scope()

    $scope.createCategory = function () {
        categoryResource.post($scope.pageScope.name).then((response) => {
            $scope.model.value = JSON.parse(response.data);
            $scope.getSource();
        })
    }

    $scope.updateCategory = function () {
        categoryResource.update($scope.model.value, $scope.pageScope.name).then((response) => {
            $scope.model.value = JSON.parse(response.data);
            $scope.getSource();
        })
    }

    $scope.$on('update-' + $routeParams.id, function (event) {
        $scope.updateCategory()
    });

    $scope.$on('create-' + $routeParams.id, function (event) {
        $scope.createCategory()
    });
});