var HttpService = function ($http) {
    window.console.log("Http service initialized.");
    this.baseUrl = "http://localhost:53911";

    // Post new loan request
    this.postLoan = function(loanData) {
        window.console.log("Attempting to post loanData...");
        return $http.post(this.baseUrl + "/data/loan/post", loanData);
    }

    // Fetch partners
    this.getPartners = function () {
        window.console.log("Attempting to get partners...");
        return $http.get(this.baseUrl + "/data/registration/");
    }

    // Post partners [never used]
    this.postPartners = function (partners) {
        window.console.log("Trying to post..", partners);
        return $http.post(this.baseUrl + "/data/registration/post", JSON.stringify(partners));
    }

    // Update partner or add new
    this.putPartner = function (partner) {
        window.console.log("Trying to put/update..", partner);
        return $http.put(this.baseUrl + "/data/registration/put", JSON.stringify(partner, (key, value) => {
            if (key === "fileData")
                return undefined;
            else return value;
        }));
    }

    // Delete partner
    this.deletePartner = function (id) {
        window.console.log("Trying to delete..", id);
        return $http.put(this.baseUrl + "/data/registration/delete", JSON.stringify(id));
    }

    // Post filedata to selected partner
    this.postFile = function (data) {
        return $http.post(this.baseUrl + "/data/registration/file", data, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            });
    }

    // Update customer's contact state
    this.updateState = function (pid, cid, state) {
        var obj = {
            pid: pid,
            cid: cid,
            state: state
        }
        return $http.post(this.baseUrl + "/data/callcentre/state",
            JSON.stringify(obj));
    }
}

HttpService.$inject = ["$http"];