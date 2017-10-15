angular.module('umbraco').controller("Umbraco.Editors.Content.UpdateNopItem", contentMyActionController);
function contentMyActionController($scope, $rootScope, $routeParams, $http, contentResource, productResource, categoryResource) {

    var nodeId = $scope.$parent.currentNode.id;
    
    $rootScope.$broadcast('update-' + nodeId);
}