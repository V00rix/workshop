var CallCentreController = function ($scope, DataService) {
    $scope.models = {
        partners: []
    }

    $scope.allCustomers = [];

    $scope.onInit = function () {
        window.console.log("CallCentre controller initialized!");
        DataService.getPartners().then(() => {
            $scope.models.partners = DataService.partners;
            $scope.allCustomers = $scope.models.partners.map(p => p.customers).reduce((acc, val) => val.concat(acc), []);
            $scope.allCustomers.forEach(c => c.total = c.loans.map(l => l.amount).reduce((acc, val) => acc + val, 0));
            $scope.models.partners.forEach(p => {
                if (p.customers)
                    p.customers.forEach(c => c.partnerId = p.id);
            });
        });
    }
}

angular.module("workshopIS", []).controller("CallCentreController", CallCentreController);

CallCentreController.$inject = ["$scope", "DataService"]; 