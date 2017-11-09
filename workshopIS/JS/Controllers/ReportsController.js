var ReportsController = function ($scope, DataService) {
    $scope.models = {
        partners: [],
        allCustomers: []
    }

    // Controlelr states
    $scope.state = {
        fromHighest: false,
        sortCustomers: "name"
    }

    // Initialization
    $scope.onInit = function () {
        // if data from server has already been fetched
        if (DataService.partners) {
            $scope.models.partners = DataService.partners;
            for (let p of $scope.models.partners) {
                p.totalIncome = $scope.countIncome(p);
            }
            $scope.models.allCustomers = $scope.models.partners.map(p => p.customers).reduce((acc, val) => val.concat(acc), []);
            $scope.models.partners.forEach(p => {
                if (p.customers)
                    p.customers.forEach(c => c.partnerId = p.id);
            });
            window.console.log("Reports controller initialized!");
        }
        else
            // fetch data
            DataService.getPartners().then(() => {
                $scope.models.partners = DataService.partners;
                for (let p of $scope.models.partners) {
                    p.totalIncome = $scope.countIncome(p);
                }
                $scope.models.allCustomers = $scope.models.partners.map(p => p.customers).reduce((acc, val) => val.concat(acc), []);
                $scope.models.partners.forEach(p => {
                    if (p.customers)
                        p.customers.forEach(c => c.partnerId = p.id);
                });
                window.console.log("Reports controller initialized!");
            });
    }

    // Filtering, sorting
    $scope.onSelectChange = function (val) {
        $scope.state.fromHighest = val;
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

ReportsController.$inject = ["$scope", "DataService"]; 