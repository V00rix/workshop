var CallCentreController = function ($scope, DataService) {
    $scope.models = {
        partners: [],
        allCustomers: []
    }

    // Initialization
    $scope.onInit = function () {
        // if data from server has already been fetched
        if (DataService.partners) {
            $scope.models.partners = DataService.partners;
            // Concat all customers into one array
            $scope.models.allCustomers = $scope.models.partners.map(p => p.customers).reduce((acc, val) => val.concat(acc), []);
            // Assign partner's id ot each
            $scope.models.allCustomers.forEach(c => c.total = c.loans.map(l => l.amount).reduce((acc, val) => acc + val, 0));
            $scope.models.partners.forEach(p => {
                if (p.customers)
                    p.customers.forEach(c => c.partnerId = p.id);
            });
            window.console.log("CallCentre controller initialized!");
        }
        else
            // fetch data
            DataService.getPartners().then(() => {
                $scope.models.partners = DataService.partners;
                // Concat all customers into one array
                $scope.models.allCustomers = $scope.models.partners.map(p => p.customers).reduce((acc, val) => val.concat(acc), []);
                // Assign partner's id ot each
                $scope.models.allCustomers.forEach(c => c.total = c.loans.map(l => l.amount).reduce((acc, val) => acc + val, 0));
                $scope.models.partners.forEach(p => {
                    if (p.customers)
                        p.customers.forEach(c => c.partnerId = p.id);
                });
                window.console.log("CallCentre controller initialized!");
            });
    }

    // Change state of a customer
    $scope.updateState = function (customer) {
        window.console.log(customer);
        DataService.updateState(customer.partnerId, customer.id, customer.contactState);
    }
}

CallCentreController.$inject = ["$scope", "DataService"]; 