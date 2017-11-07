var CallCentreController = function ($scope, DataService) {
    $scope.models = {
        partners: []
    }
   
    $scope.onInit = function () {
        window.console.log("CallCentre controller initialized!");
        DataService.getPartners().then(() => {
            $scope.models.partners = DataService.partners;
            window.console.log($scope.models.partners);
        });
    }
}

angular.module("workshopIS", []).controller("CallCentreController", CallCentreController);

CallCentreController.$inject = ["$scope", "DataService"]; 