var DataService = function (HttpService) {
    this.partners = new Array();

    this.init = function () {
        window.console.log("Data service initialized!");
        this.fakePartners();
        this.getPartners();
    }

    // create test partners
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

    // GET request to server
    this.getPartners = function () {
        return HttpService.getPartners().then(
            (res) => {
                window.console.log("Success!", res);
                this.partners = new Array(res.data.length);
                for (var i = 0; i < res.data.length; i++) {
                    var p = angular.copy(res.data)[i];
                    this.partners[i] = new Partner(p.Id, p.Name, p.ICO, new Date(p.ValidFrom), new Date(p.ValidTo), p.FileData, p.Customers);
                }
                window.console.log("Changed partners: ", this.partners);
            },
            (res) => {
                window.console.log("Error!", res);
            });
    }


    // remove loan by Id
    this.deleteLoan = function (partner, cid, lid) {
        partner.customers[cid].loans.splice(lid, 1);
        // to server
    }

    // adds new customer
    this.addCustomer = function (pid, customer) {
        this.partners[pid].customers.push(customer);
    }

    // remove customer by Id
    this.deleteCustomer = function (partner, cid) {
        partner.customers.splice(cid, 1);
        // to server
    }

    // PARTNER: ADD
    this.addPartner = function (partner) {
        // check if unique (ICO?)
        this.partners.push(partner);
        // to server
        return HttpService.putPartner(partner);
    }

    // PARTNER: UPDATE
    this.updatePartner = function (id, partner) {
        window.console.log(partner.Id);
        this.partners[id] = partner;
        // to server
        return HttpService.putPartner(partner);
    }

    // PARTNER: DELETE
    this.deletePartner = function (id) {
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


    this.init();
}

DataService.$inject = ["HttpService"]; 