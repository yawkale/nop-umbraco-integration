angular.module("umbraco").directive('customersAutocomplete', ["$timeout", "customerResource", function ($timeout, customerResource) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, iElement, iAttrs, ngModelCtrl) {
            customerResource.get().then(function (response) {
                iElement.autocomplete({
                    source: response.data.map(function (data, i) {
                        if (data.value == scope.model.value)
                            scope.customerName = data.label;

                        return data
                    }),
                    select: function (event, ui) {

                        $timeout(function () {
                            scope.customerName = ui.item.label
                            scope.model.value = ui.item.value;
                        }, 0);

                        event.preventDefault();
                    },
                    search: function (event, ui) {
                        var b = 5;
                    }
                });
            })
        }
    };
}]);