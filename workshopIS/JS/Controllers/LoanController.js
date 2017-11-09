var LoanController = function ($scope, DataService) {
    $scope.models = {
        partners: [],
        editedCustomer: null,
        currentLoan: null,
        selectedPartner: null
    };

    // Initialization
    $scope.onInit = function () {
        // set defaults
        $scope.models.editedCustomer = new Customer();
        $scope.models.currentLoan = new Loan();
        $scope.models.currentLoan.interest = .1;
        $scope.models.selectedPartner = null;
        // if data from server has already been fetched
        if (DataService.partners) {
            $scope.models.partners = DataService.partners;
            window.console.log("Loans controller initialized.");
        }
        else
            // fetch data
            DataService.getPartners().then(() => {
            $scope.models.partners = DataService.partners;
            window.console.log("Loans controller initialized.");
        });
    }

    // Link partner to customer, when partner is selected from dropdown
    $scope.onPartnerSelected = function (partner) {
        window.console.log(partner);
        $scope.models.selectedPartner = $scope.models.partners.find((p) => {
            return p.name === partner;
        });
        $scope.models.editedCustomer.partner = $scope.models.selectedPartner;
    }

    // Send form to server and create loan on success
    $scope.onCreateLoan = function (valid) {
        window.console.log($scope.models);
        window.console.log(valid);
        if (valid) {
            window.console.log("Data valid. Creating new loan...");
            DataService.addLoan({
                amount: $scope.models.currentLoan.amount,
                duration: $scope.models.currentLoan.duration,
                interest: $scope.models.currentLoan.interest,
                partnerId: $scope.models.selectedPartner.id,
                phone: $scope.models.editedCustomer.phone,
                firstName: $scope.models.editedCustomer.firstName,
                surname: $scope.models.editedCustomer.surname,
                email: $scope.models.editedCustomer.email,
                note: $scope.models.editedCustomer.note
            });
        }
        else {
            window.console.log("Invalid data! Could not create loan.");
        }
    }

    // Reset form
    $scope.clearForm = function (form) {
        // reset values
        $scope.models.editedCustomer = new Customer();
        $scope.models.currentLoan = new Loan();
        $scope.models.currentLoan.interest = .1;
        $scope.models.selectedPartner = null;
        window.console.log("Clearing form...");
        // reset form
        var controlNames = Object.keys(form).filter(key => key.indexOf('$') !== 0);
        for (var name of controlNames) {
            if (form[name].control)
                form[name].control.$modelValue = null;
        }
        form.$setPristine();
        form.$setUntouched();
    }
}

LoanController.$inject = ["$scope", "DataService"]; 