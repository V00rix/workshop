var PartnersController = function ($scope, DataService) {
    $scope.models = {
        partners: []
    }
    $scope.inEditMode = false;
    $scope.selectedPartnerId = null;
    $scope.editedPartner = null;

    $scope.onPartnerSelected = function (pid) {
        $scope.selectedPartnerId = pid;
        $scope.editedPartner = angular.copy($scope.models.partners[pid]);
        $scope.inEditMode = true;
    }

    $scope.onEditConfirmed = function () {
        window.console.log("Edited partner: ", $scope.editedPartner, $scope.selectedPartnerId);
        // not null -> editing existing
        if ($scope.selectedPartnerId != null)
            DataService.updatePartner($scope.selectedPartnerId, $scope.editedPartner);
        else
            DataService.addPartner($scope.editedPartner);
        $scope.models.partners = DataService.partners;
        $scope.closeEdit();
    }

    $scope.newPartner = function () {
        $scope.editedPartner = new Partner();
        $scope.inEditMode = true;
    }

    $scope.onEditCanceled = function () {
        $scope.closeEdit();
    }

    $scope.onDeletePartner = function () {
        if ($scope.selectedPartnerId != null)
            DataService.deletePartner($scope.selectedPartnerId);
        $scope.closeEdit();
    }

    $scope.onDeleteCustomer = function (cid) {
        DataService.deleteCustomer($scope.editedPartner, cid);
        DataService.updatePartner($scope.selectedPartnerId, $scope.editedPartner);
        $scope.models.partners = DataService.partners;
    }

    $scope.onDeleteLoan = function (cid, lid) {
        DataService.deleteLoan($scope.editedPartner, cid, lid);
        DataService.updatePartner($scope.selectedPartnerId, $scope.editedPartner);
        $scope.models.partners = DataService.partners;
    }

    $scope.closeEdit = function () {
        $scope.editedPartner = null;
        $scope.selectedPartnerId = null;
        $scope.inEditMode = false;
    }

    $scope.onInit = function () {
        $scope.models.partners = DataService.partners;
    }
}

angular.module("workshopIS", []).controller("PartnersController", PartnersController);

PartnersController.$inject = ["$scope", "DataService"]; 