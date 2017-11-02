var workshopIS = angular.module("workshopIS", ["ngRoute"]);

workshopIS.service('DataService', DataService);

workshopIS.controller("MainController", MainController);
workshopIS.controller("LoanController", LoanController);
workshopIS.controller("PartnersController", PartnersController);

var configFunction = function ($routeProvider) {
    $routeProvider.when("/loan",
            {
                templateUrl: "Loan",
                controller: "LoanController"
            })
        .when("/partners",
            {
                templateUrl: "Partners",
                controller  : "PartnersController"
            });
}
configFunction.$inject = ["$routeProvider"];

workshopIS.config(configFunction);