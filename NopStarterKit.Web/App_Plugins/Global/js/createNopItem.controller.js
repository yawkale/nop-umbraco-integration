angular.module('umbraco').controller("Umbraco.Editors.Content.CreateNopItem", contentMyActionController);
function contentMyActionController($scope, $rootScope, $routeParams, $http, contentResource) {

    var nodeId = $scope.$parent.currentNode.id;

    $rootScope.$broadcast('create-' + nodeId);
}