var DataService = function (HttpService) {
    // partners model
    this.partners = null;

    // MOCKING function form basic functionality
    this.fakePartners = function () {
        this.partners = [
            new Partner("my boi", 312, new Date(2013, 10, 12, 0, 0, 0, 0)),
            new Partner("mai boi", 312, new Date(2013, 10, 12, 0, 0, 0, 0)),
            new Partner("my boi", 312, new Date(2013, 10, 12, 0, 0, 0, 0), new Date(2023, 10, 12, 0, 0, 0, 0)),
            new Partner("eee boi", 312, new Date(2013, 10, 12, 0, 0, 0, 0)),
            new Partner("my boi", 312, new Date(2013, 10, 12, 0, 0, 0, 0)),
        ];
        for (let partner of this.partners) {
            window.console.log("adding customers to " + partner.name);
            partner.customers = [
                new Customer("333222111", new Date(2013, 10, 12, 0, 0, 0, 0), partner, "John", "Smith"),
                new Customer("333222111", new Date(2013, 10, 12, 0, 0, 0, 0), partner, "ddsadn", "Smith"),
                new Customer("333222111", new Date(2013, 10, 12, 0, 0, 0, 0), partner, "John", "Smith"),
                new Customer("333222111", new Date(2013, 10, 12, 0, 0, 0, 0), partner, "John", "Smith"),
            ];
            for (let customer of partner.customers) {
                customer.loans = [
                    new Loan(50, 50000, customer, 100),
                    new Loan(50, 50000, customer, 100),
                    new Loan(54, 500000, customer, 300),
                    new Loan(39, 50000, customer, 100),
                    new Loan(20, 50000, customer, 100)
                ];
                for (let loan of customer.loans) {
                    loan.note = "dsadsadasdas";
                }
            }
        };
        window.console.log("Fake partners created.", this.partners);
    }

    // remove loan by Id
    this.deleteLoan = function (partner, cid, lid) {
        partner.customers[cid].loans.splice(lid, 1);
    }

    // adds new customer
    this.addCustomer = function (pid, customer) {
        this.partners[pid].customers.push(customer);
    }

    // remove customer by Id
    this.deleteCustomer = function (partner, cid) {
        partner.customers.splice(cid, 1);
    }

    // LOAN: POST
    this.addLoan = function (loanData) {
        window.console.log("Adding new loan...", loanData);
        // to server
        return HttpService.postLoan(loanData).then(
            (res) => {
                window.console.log("Success!", res);
                var p, c;
                // on successfull response find patner of a customer
                if ((p = this.partners.find(par => par.id === loanData.partnerId)) != null) {
                    // if partner contains this customer already, then...
                    if ((c = p.customers.find(cus => cus.id === res.data.id)) != null) {
                        // update it
                        c.copy(res.data);
                    } else {
                        // else push new to lsit
                        p.customers.push(new Customer.From(res.data));
                    }
                }
            },
            (res) => {
                window.console.log("Error!", res);
            });
    }

    // PARTNERS: GET
    this.getPartners = function () {
        window.console.log("Getting partners...");
        // to server
        return HttpService.getPartners().then(
            (res) => {
                window.console.log("Success.", res.data);
                this.partners = [];
                for (var i = 0; i < res.data.length; ++i) 
                    this.partners.push(new Partner.From(res.data[i]));
                window.console.log(this.partners);
            },
            (res) => {
                window.console.log("Error!", res);
            });
    }

    // PARTNER: ADD
    this.addPartner = function (partner, formData) {
        window.console.log("Adding new partner...");
        // to server
        return HttpService.putPartner(partner).then(
            (res) => {
                partner.id = res.data;
                this.partners.push(partner);
                window.console.log("Successs!", res);
                if (formData)
                    this.postFile(formData);
            },
            (res) => {
                window.console.log("Error!", res);
            });
    }

    // PARTNER: UPDATE
    this.updatePartner = function (id, partner, formData) {
        window.console.log("Updating partner " + id + "...");
        // to server
        return HttpService.putPartner(angular.copy(partner)).then(
            (res) => {
                this.partners[id] = partner;
                if (!this.partners[id].customers)
                    this.partners[id].customers = [];
                for (let c of this.partners[id].customers) {
                    if (!c.loans)
                        c.loans = [];
                }
                window.console.log("Successs!", res);
                if (formData)
                    this.postFile(formData);
            },
            (res) => {
                window.console.log("Error!", res);
            });
    }

    // PARTNER: DELETE
    this.deletePartner = function (id) {
        window.console.log("Deleting partner " + id + "...");
        // to server
        HttpService.deletePartner(this.partners[id].id).then(
            (res) => {
                window.console.log("Successs!", res);
                this.partners.splice(id, 1);
            },
            (res) => {
                window.console.log("Error!", res);
            });
    }

    // POST FILE of a partner
    this.postFile = function (data) {
        window.console.log("Posting file data...");
        // to server
        HttpService.postFile(data).then(
            (res) => {
                window.console.log("Successs!", res);
            },
            (res) => {
                window.console.log("Error!", res);
            });
    }

    // UPDATE STATE of a customer 
    this.updateState = function (pid, cid, contactState) {
        HttpService.updateState(pid, cid, contactState);
    }
}

DataService.$inject = ["HttpService"]; 