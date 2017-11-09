var PartnersController = function ($scope, DataService) {
    $scope.models = {
        partners: []
    }
    $scope.inEditMode = false;
    $scope.selectedPartnerId = null;
    $scope.editedPartner = null;
    $scope.formData = null;

    $scope.onPartnerSelected = function (pid) {
        $scope.selectedPartnerId = pid;
        $scope.editedPartner = angular.copy($scope.models.partners[pid]);
        $scope.inEditMode = true;
        window.console.log("Selected: ", $scope.editedPartner);
    }

    $scope.onEditConfirmed = function () {
        window.console.log("Edited partner: ", $scope.editedPartner, $scope.editedPartner.id);
        // not null -> editing existing
        if ($scope.selectedPartnerId !== null) {
            if ($scope.formData)
                $scope.formData.append("id", $scope.editedPartner.id);
            DataService.updatePartner($scope.selectedPartnerId, $scope.editedPartner, $scope.formData).then(() => {
                $scope.formData = null;
            });
        }
        else {
            DataService.addPartner($scope.editedPartner, $scope.formData).then(() => {
                $scope.formData = null;
            });
        }
        $scope.models.partners = DataService.partners;
        $scope.closeEdit();
    }

    $scope.appendFile = function (files) {
        $scope.formData = new FormData();
        $scope.formData.append("file", files[0]);
    };

    $scope.newPartner = function () {
        $scope.editedPartner = new Partner();
        $scope.inEditMode = true;
    }

    $scope.onEditCanceled = function () {
        $scope.closeEdit();
    }

    $scope.onDeletePartner = function () {
        if ($scope.selectedPartnerId !== null)
            DataService.deletePartner($scope.selectedPartnerId);
        $scope.closeEdit();
    }

    $scope.onDeleteCustomer = function (cid) {
        DataService.deleteCustomer($scope.editedPartner, cid);
    }

    $scope.onDeleteLoan = function (cid, lid) {
        DataService.deleteLoan($scope.editedPartner, cid, lid);
    }

    $scope.closeEdit = function () {
        $scope.editedPartner = null;
        $scope.selectedPartnerId = null;
        $scope.inEditMode = false;
    }

    // Initialization
    $scope.onInit = function () {
        if (DataService.partners) {
            $scope.models.partners = DataService.partners;
            window.console.log("Partners controller initialized!");
        }
        else DataService.getPartners().then(() => {
            $scope.models.partners = DataService.partners;
            window.console.log("Partners controller initialized!");
        });
    }
}

angular.module("workshopIS", []).controller("PartnersController", PartnersController);

PartnersController.$inject = ["$scope", "DataService"]; 