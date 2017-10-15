angular.module("umbraco").directive('auto', ["$timeout", "productResource", function ($timeout, productResource) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, iElement, iAttrs, ngModelCtrl) {
            scope.getSource = function () {
                productResource.get().then(function (response) {
                    iElement.autocomplete({
                        source: response.data.map(function (data, i) {
                            if (data.id == scope.model.value)
                                scope.productName = data.name;

                            return {
                                value: data.id,
                                label: data.name
                            }
                        }),
                        select: function (event, ui) {

                            $timeout(function () {
                                scope.productName = ui.item.label
                                scope.model.value = ui.item.value;
                            }, 0);

                            event.preventDefault();
                        },
                        search: function (event, ui) {
                            var b = 5;
                        }
                    });
                })
            };

            scope.autocompleteDataChanged = function () {
                if (!scope.productName) {
                    scope.model.value = null;
                }

            }

            scope.getSource();
        }
    };
}]);