var PartnersController = function ($scope, DataService) {
    $scope.models = {
        partners: [],
        selectedPartnerId: null,
        editedPartner: null,
        formData: null
    }

    // Controller states
    $scope.state = {
        // defines whether the editing form is shown
        inEditMode: false
    }

    // Initialization
    $scope.onInit = function () {
        // if data from server has already been fetched
        if (DataService.partners) {
            $scope.models.partners = DataService.partners;
            window.console.log("Partners controller initialized!");
        }
        else
            // fetch data
            DataService.getPartners().then(() => {
            $scope.models.partners = DataService.partners;
            window.console.log("Partners controller initialized!");
        });
    }

    // Set selected partner as edited one when selected from list
    $scope.onPartnerSelected = function (pid) {
        $scope.models.selectedPartnerId = pid;
        $scope.models.editedPartner = angular.copy($scope.models.partners[pid]);
        $scope.state.inEditMode = true;
        window.console.log("Selected: ", $scope.models.editedPartner);
    }

    // Add or change partner when edit is finished and confirmed
    $scope.onEditConfirmed = function () {
        if ($scope.models.selectedPartnerId !== null) {
            if ($scope.models.formData)
                $scope.models.formData.append("id", $scope.models.editedPartner.id);
            DataService.updatePartner($scope.models.selectedPartnerId, $scope.models.editedPartner, $scope.models.formData).then(() => {
                $scope.models.formData = null;
            });
        }
        else {
            DataService.addPartner($scope.models.editedPartner, $scope.models.formData).then(() => {
                $scope.models.formData = null;
            });
        }
        $scope.models.partners = DataService.partners;
        $scope.closeEdit();
    }

    // Add or change partner's file data
    $scope.appendFile = function (files) {
        $scope.models.formData = new FormData();
        $scope.models.formData.append("file", files[0]);
    };

    // Create new partner and set as edited one on button pressed
    $scope.newPartner = function () {
        $scope.models.editedPartner = new Partner();
        $scope.state.inEditMode = true;
    }

    // Close editing form on button pressed
    $scope.onEditCanceled = function () {
        $scope.closeEdit();
    }

    // Delete selected partner
    $scope.onDeletePartner = function () {
        if ($scope.models.selectedPartnerId !== null)
            DataService.deletePartner($scope.models.selectedPartnerId);
        $scope.closeEdit();
    }

    // Delete partner's customer
    $scope.onDeleteCustomer = function (cid) {
        DataService.deleteCustomer($scope.models.editedPartner, cid);
    }
    
    // Delete partner's customer's loan
    $scope.onDeleteLoan = function (cid, lid) {
        DataService.deleteLoan($scope.models.editedPartner, cid, lid);
    }

    // helper: code to close form and reset values
    $scope.closeEdit = function () {
        $scope.models.editedPartner = null;
        $scope.models.selectedPartnerId = null;
        $scope.state.inEditMode = false;
    }
}

PartnersController.$inject = ["$scope", "DataService"]; 