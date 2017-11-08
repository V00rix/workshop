var ReportsController = function ($scope, DataService) {
    $scope.models = {
        partners: []
    }

    $scope.fromHighest = false;
    $scope.sortCustomers = 'name';
    $scope.allCustomers = [];

    $scope.onSelectChange = function (val) {
        $scope.fromHighest = val;
    }

    $scope.onInit = function () {
        window.console.log("Reports controller initialized!");
        DataService.getPartners().then(() => {
            $scope.models.partners = DataService.partners;
            for (let p of $scope.models.partners) {
                p.totalIncome = $scope.countIncome(p);
            }
            $scope.allCustomers = $scope.models.partners.map(p => p.customers).reduce((acc, val) => val.concat(acc), []);
        });
    }

    $scope.countIncome = function (partner) {
        return partner.customers
            .map(c => c.loans
                .map(l => l.amount)
                .reduce((acc, val) => acc + val, 0))
            .reduce((acc, val) => acc + val, 0);
    }
}

angular.module("workshopIS", []).controller("ReportsController", ReportsController);

ReportsController.$inject = ["$scope", "DataService"]; 