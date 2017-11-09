var MainController = function ($scope) {
    $scope.Link = function(name, sectionName, href) {
        this.name = name;
        this.sectionName = sectionName;
        this.href = href;
    }

    $scope.models = {
        title: null,
        section: null,
        links: []
    }

    $scope.init = function () {
        $scope.models.title = "XTK Systems";
        $scope.models.section = angular.copy($scope.models.title);
        $scope.models.links = [
            new $scope.Link("Loan", "New Loan", "#!loan"),
            new $scope.Link("Partners", "Edit Partners", "#!partners"),
            new $scope.Link("Reports", "View Reports", "#!reports"),
            new $scope.Link("Call Centre", "Customer\'s status", "#!callcentre")
        ];
        window.console.log("Main controller initialized.");
    }


    $scope.init();
}

MainController.$inject = ['$scope'];