var HttpService = function ($http) {
    window.console.log("Http service initialized.");
    this.baseUrl = "http://localhost:53911";

    this.postLoan = function(loanData) {
        window.console.log("Attempting to post loanData...");
        return $http.post(this.baseUrl + "/data/loan/post", loanData);
    }

    this.getPartners = function (partners) {
        window.console.log("Attempting to get partners...");
        return $http.get(this.baseUrl + "/data/registration/");
    }

    this.postPartners = function (partners) {
        window.console.log("Trying to post..", JSON.stringify(partners));
        return $http.post(this.baseUrl + "/data/registration/post", JSON.stringify(partners));
    }

    this.putPartner = function (partner) {
        window.console.log("Trying to put/update..", JSON.stringify(partner));
        return $http.put(this.baseUrl + "/data/registration/put", JSON.stringify(partner));
    }

    this.deletePartner = function (id) {
        window.console.log("Trying to delete..", JSON.stringify(id));
        return $http.put(this.baseUrl + "/data/registration/delete", JSON.stringify(id));
    }
}

HttpService.$inject = ["$http"];