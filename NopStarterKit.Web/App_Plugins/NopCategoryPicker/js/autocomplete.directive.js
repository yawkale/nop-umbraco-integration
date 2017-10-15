angular.module("umbraco").directive('categoriesAutocomplete', ["$timeout", "categoryResource", function ($timeout, categoryResource) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, iElement, iAttrs, ngModelCtrl) {
            scope.getSource = function () {
                categoryResource.get().then(function (response) {
                    iElement.autocomplete({
                        source: response.data.map(function (data, i) {
                            if (data.value == scope.model.value)
                                scope.categoryName = data.label;

                            return data;
                        }),
                        select: function (event, ui) {

                            $timeout(function () {
                                scope.categoryName = ui.item.label
                                scope.model.value = ui.item.value;
                            }, 0);

                            event.preventDefault();
                        },
                    });
                })
            };

            scope.autocompleteDataChanged = function () {
                if (!scope.categoryName) {
                    scope.model.value = null;
                }
                    
            }

            scope.getSource();
        }
    };
}]);