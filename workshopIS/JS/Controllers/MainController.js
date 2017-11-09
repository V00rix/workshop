var MainController = function ($scope) {
    $scope.models = {
        title: null,
        section: null,
        links: []
    }

    $scope.Link = function (name, sectionName, href) {
        this.name = name;
        this.sectionName = sectionName;
        this.href = href;
    }

    // Initialization
    $scope.onInit = function () {
        // set title
        $scope.models.title = "XTK Systems";
        $scope.models.section = angular.copy($scope.models.title);
        // define links
        $scope.models.links = [
            new $scope.Link("Loan", "New Loan", "#!loan"),
            new $scope.Link("Partners", "Edit Partners", "#!partners"),
            new $scope.Link("Reports", "View Reports", "#!reports"),
            new $scope.Link("CallCentre", "Customer\'s status", "#!callcentre")
        ];
        // subscribe to route chagnes
        $scope.$on("$routeChangeSuccess",
            function (event, current) {
                try {
                    var routeName = current.$$route.templateUrl;
                    $scope.models.section = $scope.models.links.find(l => l.name === routeName).sectionName;
                }
                catch (e) {
                    $scope.models.section = $scope.models.title;
                    try {
                        if (current.$$route.templateUrl === "NotFound")
                            $scope.models.section = "NotFound";
                    }
                    catch (e) {
                        $scope.models.section = $scope.models.title;
                    }
                }
                window.console.log($scope.models.section);
            });
        window.console.log("Main controller initialized.");
    }
}

MainController.$inject = ["$scope"];