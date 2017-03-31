angular.module("umbraco")
.controller("Tagliatelle.PropertyEditors.tagliatelleTagEditor",

    function ($http,$scope,editorState,assetsService)
    {
        assetsService.loadCss("/App_Plugins/Tagliatelle/PropertyEditors/css/tagliatelle.css");

        $scope.hideTags = true;
    	$scope.tags = [];
    	$scope.currentTags = [];
    	$scope.currentTagIds = [];

    	// get current node id using routeParams
    	var currentNodeId = editorState.current.id;

        // Retrieve all tags that currently exist within the current website scope
    	$scope.initialiseTags = function () {
    	    $http.get("/umbraco/backoffice/tagliatelle/TagEditorApi/GetTags/?currentNodeId=" + currentNodeId + "&containerId=" + $scope.model.config.parentContainer + "&documentTypeAlias=" + $scope.model.config.documentTypeAlias)
                .success(function (data, status, headers, config) {
                    $scope.tags = data;
                });
    	}

    	// Do a web api call to get tag names from currentTagIds
    	$scope.initTagNamesFromIds = function () {
    	    $http.get("/umbraco/backoffice/tagliatelle/TagEditorApi/GetTagNames/?nodeIds=" + $scope.model.value + "&documentTypeAlias=" + $scope.model.config.documentTypeAlias)
				.success(function (data, status, headers, config) {
					$scope.currentTags = data;
				});
    	}

        /* Populate the existing tag list */
    	$scope.initialiseTags();

        // Populate the control with the values selected from the database
    	if ($scope.model.value) {
    		$scope.currentTagIds = $scope.model.value.split(",");
    		$scope.initTagNamesFromIds();
    	}


		// Toggle the display of existing website tags
    	$scope.showTags = function (e) {
    	    $scope.hideTags = ($scope.hideTags == true ? false : true);
    	};

		// Add a tag when the user presses enter
    	$scope.addTagOnKeyPress = function (e) {
    		var code = e.KeyCode || e.which;
    		if (code == 13) {
    			e.preventDefault();
    			$scope.addTag();
    		}
    	}

    	// Add a tag to the view
    	$scope.addTag = function (e) {
    		if ($scope.currentTags.indexOf($scope.tagToAdd) < 0) {
    			$scope.currentTags.push($scope.tagToAdd);
    		}
    		$scope.tagToAdd = "";
    	};

        // Remove tag from view
    	$scope.removeTag = function (tag) {
    	    var i = $scope.currentTags.indexOf(tag);
    	    if (i >= 0) {
    	        $scope.currentTags.splice(i, 1);
    	    }
    	}

        // Add an existing tag
    	$scope.addFromTagList = function (e) {
    	    var tag = $(e.currentTarget).attr("data-tag");
    	    $scope.tagToAdd = tag;
    	    $scope.addTag(e);
    	}

    	$scope.$on("formSubmitting", function (ev, args) {
    	    $.ajax({
    	        url: "/umbraco/backoffice/tagliatelle/TagEditorApi/GetAndEnsureNodeIdsForTags/?currentNodeId=" + currentNodeId + "&tags=" + $scope.currentTags + "&containerId=" + $scope.model.config.parentContainer + "&documentTypeAlias=" + $scope.model.config.documentTypeAlias,
    	        async: false
    	    }).done(function (data) {
    	        $scope.model.value = data.join();
    	        $scope.model.onValueChanged($scope.model.value);
    	        $scope.initialiseTags();
    	    });
    	});

    	$scope.model.onValueChanged = function (newVal, oldVal) {
    	    $scope.model.value = newVal;
    	    $scope.currentTagIds = $scope.model.value.split(",");
    	    $scope.initTagNamesFromIds();
    	};

		
    }
);

