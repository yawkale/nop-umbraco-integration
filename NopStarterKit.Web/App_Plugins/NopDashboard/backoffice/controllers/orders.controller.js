angular.module("umbraco").controller("Nop.OrdersController", function ($scope, $routeParams, orderResource, notificationsService) {
    $scope.options = {
        pageSize: 10,
        pageNumber: ($routeParams.page && Number($routeParams.page) != NaN && Number($routeParams.page) > 0) ? $routeParams.page : 1,
        filter: {},
        orderBy: 'Name',
        orderDirection: "asc"//,
    };

    $scope.reloadView = function () {
        orderResource.get().then(function (response) {
            $scope.totalPages = Math.ceil(response.data.length / $scope.options.pageSize);
            $scope.viewData = response.data.slice((($scope.options.pageNumber - 1) * 10), 10 * $scope.options.pageNumber);

            if ($scope.options.pageNumber > $scope.totalPages) {
                $scope.options.pageNumber = $scope.totalPages;
            }

            $scope.pagination = [];

            if ($scope.totalPages <= 10) {
                for (var i = 0; i < $scope.totalPages; i++) {
                    $scope.pagination.push({
                        val: (i + 1),
                        isActive: $scope.options.pageNumber == (i + 1)
                    });
                }
            }
            else {
                var maxIndex = $scope.totalPages - 10;

                var start = Math.max($scope.options.pageNumber - 5, 0);

                start = Math.min(maxIndex, start);

                for (var i = start; i < (10 + start) ; i++) {
                    $scope.pagination.push({
                        val: (i + 1),
                        isActive: $scope.options.pageNumber == (i + 1)
                    });
                }

                if (start > 0) {
                    $scope.pagination.unshift({ name: "First", val: 1, isActive: false }, { val: "...", isActive: false });
                }

                if (start < maxIndex) {
                    $scope.pagination.push({ val: "...", isActive: false }, { name: "Last", val: $scope.totalPages, isActive: false });
                }
            }
        });
    };

    $scope.reloadView();

    $scope.next = function () {
        if ($scope.options.pageNumber < $scope.totalPages) {
            $scope.options.pageNumber++;
            $scope.reloadView();
        }
    };

    $scope.goToPage = function (pageNumber) {
        $scope.options.pageNumber = pageNumber + 1;
        $scope.reloadView();
    };

    $scope.prev = function () {
        if ($scope.options.pageNumber > 1) {
            $scope.options.pageNumber--;
            $scope.reloadView();
        }
    };
});

