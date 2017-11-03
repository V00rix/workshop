var LoanController = function ($scope, DataService) {
    $scope.models = {
        partners: []
    };

    $scope.regexs = [
        { reg: /^(([a-z -'`A-Z]*)|([а-я -'`А-Я]*)|([a-z -'`A-ZěščřžýáíéúůĚŠČŘŽÝÁÍÉÚŮ]*))$/, key: "name" },
        { reg: /^((\+[0-9][0-9][0-9][ -]?)?[0-9][0-9][0-9][ -]?[0-9][0-9][0-9][ -]?[0-9][0-9][0-9])$/, key: "phone" },
        { reg: /^([a-zA-Z0-9.])*@([a-zA-Z])*\.([a-zA-Z])+$/, key: "email" },
        { reg: /^ *$/, key: "empty" },
        { reg: /^.*$/, key: "any" }
    ];

    $scope.editedCustomer = new Customer();
    $scope.currentLoan = new Loan();
    $scope.currentLoan.interest = .1;
    $scope.selectedPartner = null;

    $scope.onPartnerSelected = function (partner) {
        $scope.selectedPartner = $scope.models.partners.find((p) => {
            return p.name === partner;
        });
        $scope.editedCustomer.partner = $scope.selectedPartner;
    }

    $scope.onCreateLoan = function () {
        if ($scope.dataValid()) {
            window.console.log("Data valid. Creating new loan...");
            DataService.addLoan({
                amount: $scope.currentLoan.amount,
                duration: $scope.currentLoan.duration,
                interest: $scope.currentLoan.interest,
                partnerId: $scope.selectedPartner.id,
                phone: $scope.editedCustomer.phone,
                firstName: $scope.editedCustomer.firstName,
                surname: $scope.editedCustomer.surname,
                email: $scope.editedCustomer.email,
                note: $scope.editedCustomer.note
            }).then($scope.models.partners = DataService.partners);
        }
        else {
            window.console.log("Invalid data! Could not create loan.");
        }

    }

    $scope.dataValid = function () {
        var err = [];
        var warn = [];
        var res = true;

        // Errors
        if (!$scope.selectedPartner) {
            err.push("No partner selected!");
            res = false;
        }
        if (!$scope.regexs.find(r => r.key === 'phone').reg.test($scope.editedCustomer.phone)) {
            err.push("Invalid phone number!");
            res = false;
        }
        if (!$scope.currentLoan.amount) {
            err.push("Loan amount wasn't entered!");
            res = false;
        }
        if ($scope.currentLoan.amount > 50000 || $scope.currentLoan.amount < 20000) {
            err.push("Loan amount not in range!");
            res = false;
        }
        if (!$scope.currentLoan.duration) {
            err.push("Loan duration wasn't entered!");
            res = false;
        }
        else if ($scope.currentLoan.duration < 6 || $scope.currentLoan.duration > 96) {
            err.push("Loan duration not in range!");
            res = false;
        }
        if ($scope.currentLoan.interest < 0 || $scope.currentLoan.interest > 0.5) {
            err.push("Loan interest not in range!");
            res = false;
        }

        // Warnings
        if (!$scope.editedCustomer.firstName || $scope.regexs.find(r => r.key === 'empty').reg.test($scope.editedCustomer.firstName)) {
            warn.push("First name is empty!");
        }
        if (!$scope.regexs.find(r => r.key === 'name').reg.test($scope.editedCustomer.firstName)) {
            warn.push("Invalid first name!");
        }
        if (!$scope.editedCustomer.surname || $scope.regexs.find(r => r.key === 'empty').reg.test($scope.editedCustomer.surname)) {
            warn.push("Surname is empty!");
        }
        if (!$scope.regexs.find(r => r.key === 'name').reg.test($scope.editedCustomer.surname)) {
            warn.push("Invalid surname!");
        }
        if ($scope.regexs.find(r => r.key === 'empty').reg.test($scope.editedCustomer.email)) {
            warn.push("Email is empty!");
        }
        if (!$scope.regexs.find(r => r.key === 'email').reg.test($scope.editedCustomer.email)) {
            warn.push("Invalid email!");
        }
        if (!$scope.currentLoan.interest) {
            warn.push("Loan duration wasn't entered - setting to .1");
        }

        if (err.length < 1 && warn.length < 1)
            window.console.log("Everything is fine!");
        else {
            if (err.length > 0) {
                window.console.log("Errors: ");
                for (let er of err) {
                    window.console.log(er);
                }
            }
            if (warn.length > 0) {
                window.console.log("Warnings: ");
                for (let w of warn) {
                    window.console.log(w);
                }
            }
        }
        return res;
    }

    $scope.logPartners = function () {
        window.console.log($scope.partners);
    }

    $scope.onInit = function () {
        DataService.getPartners().then(() => {
            window.console.log("Loans initialized.");
            $scope.models.partners = DataService.partners;
        });
    }
}

angular.module("workshopIS", []).controller("LoanController", LoanController);

LoanController.$inject = ["$scope", "DataService"]; 