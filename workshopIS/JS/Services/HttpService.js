var HttpService = function ($http) {
    window.console.log("Http service initialized!");
    this.baseUrl = "http://localhost:53911";

    this.getPartners = function (partners) {
        window.console.log("Attempting to get partners!");
        $http.get(this.baseUrl + '/data/registration').then(
            (res) => {
                window.console.log("Success!", res);
                partners = new Array(res.data.length);
                //partners = res.data;
                for (i = 0; i < res.data.length; i++) {
                    var p = res.data[i];
                    //partners[i] = new Partner(p.Name, p.ICO, p.ValidFrom);
                }
                window.console.log(partners);
            },
            (res) => {
                window.console.log("Error!", res);
            });

    }

    this.postPartners = function (partners) {
        $http.post(this.baseUrl + '/data/registration', partners).then(
            (res) => {
                window.console.log("Success!", res);
            },
            (res) => {
                window.console.log("Error!", res);
            });
    }
}

HttpService.$inject = ["$http"];