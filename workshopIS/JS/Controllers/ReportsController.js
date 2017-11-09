var ReportsController = function ($scope, DataService) {
    $scope.models = {
        partners: []
    }

    $scope.fromHighest = false;
    $scope.sortCustomers = 'name';
    $scope.allCustomers = [];

    // Filtering, sorting
    $scope.onSelectChange = function (val) {
        $scope.fromHighest = val;
    }

    // Initialization
    $scope.onInit = function () {
        if (DataService.partners) {
            $scope.models.partners = DataService.partners;
            for (let p of $scope.models.partners) {
                p.totalIncome = $scope.countIncome(p);
            }
            $scope.allCustomers = $scope.models.partners.map(p => p.customers).reduce((acc, val) => val.concat(acc), []);
            $scope.models.partners.forEach(p => {
                if (p.customers)
                    p.customers.forEach(c => c.partnerId = p.id);
            });
            window.console.log("Reports controller initialized!");
        }
        else
            DataService.getPartners().then(() => {
                $scope.models.partners = DataService.partners;
                for (let p of $scope.models.partners) {
                    p.totalIncome = $scope.countIncome(p);
                }
                $scope.allCustomers = $scope.models.partners.map(p => p.customers).reduce((acc, val) => val.concat(acc), []);
                $scope.models.partners.forEach(p => {
                    if (p.customers)
                        p.customers.forEach(c => c.partnerId = p.id);
                });
                window.console.log("Reports controller initialized!");
            });
    }

    // Summ ammounts throughout all loans throughout all customers for each partner
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