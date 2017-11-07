var DataService = function (HttpService) {
    this.partners = new Array();

    this.init = function () {
        window.console.log("Data service initialized.");
        // this.fakePartners();
        this.getPartners();
    }

    this.capitalizeFirstLetter = function (string) {
        return (string[0].toUpperCase() + string.slice(1));
    }

    this.uncapitalizeFirstLetter = function (string) {
        return (string[0].toLowerCase() + string.slice(1));
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

    // LOAN: POST
    this.addLoan = function (loanData) {
        return HttpService.postLoan(loanData).then(
            (res) => {
                this.getPartners();
                window.console.log("Successs!", res);
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

    // PARTNERS: GET
    this.getPartners = function () {
        return HttpService.getPartners().then(
            (res) => {
                window.console.log("Success.", res.data);
                this.partners = res.data;
                window.console.log(this.partners);
                for (var i = 0; i < res.data.length; ++i) {
                    this.partners[i].validFrom = new Date(res.data[i].validFrom);
                    this.partners[i].validTo = new Date(res.data[i].validTo);
                    if (!this.partners[i].customers)
                        this.partners[i].customers = [];
                    for (let c of this.partners[i].customers) {
                        if (!c.loans)
                            c.loans = [];
                    }
                }
                //    //var p = angular.copy(res.data)[i];
                //    window.console.log(res.data[i]);
                //    this.partners[i] = res.data[i];
                //this.partners[i] = new Partner(p.Id, p.Name, p.ICO, new Date(p.ValidFrom), new Date(p.ValidTo), p.FileData, []);
                //for (var j = 0; j < p.Customers.length; ++j) {
                //    this.partners[i].customers.push(
                //        new Customer(p.Customers[j].Id,
                //            p.Customers[j].Phone,
                //            p.Customers[j].CreationDate,
                //            p.Id,
                //            p.Customers[j].FirstName,
                //            p.Customers[j].Surname,
                //            p.Customers[j].Email,
                //            p.Customers[j].ContactState,
                //            []));
                //    for (var k = 0; k < p.Customers[j].Loans.length; ++k) {
                //        this.partners[i].customers[j].loans.push(
                //            new Loan(p.Customers[j].Loans[k].Id,
                //                p.Customers[j].Loans[k].Duration,
                //                p.Customers[j].Loans[k].Amount,
                //                p.Customers[j].Id,
                //                p.Customers[j].Loans[k].MonthlyCharge,
                //                p.Customers[j].Loans[k].APR,
                //                p.Customers[j].Loans[k].Interest,
                //                p.Customers[j].Loans[k].Note));
                //    }
                //}
            },
            (res) => {
                window.console.log("Error!", res);
            });
    }

    // PARTNER: ADD
    this.addPartner = function (partner) {
        this.retype(partner);
        // to server
        return HttpService.putPartner(partner).then(
            (res) => {
                partner.id = res.data;
                this.retypeBack(partner);
                this.partners.push(partner);
                window.console.log("Successs!", res);
            },
            (res) => {
                window.console.log("Error!", res);
            });
    }

    // PARTNER: UPDATE
    this.updatePartner = function (id, partner) {
        // retyping
        this.retype(partner);
        window.console.log(partner);
        // to server
        return HttpService.putPartner(angular.copy(partner)).then(
            (res) => {
                //this.retypeBack(partner);
                this.partners[id] = partner;
                if (!this.partners[id].customers)
                    this.partners[id].customers = [];
                for (let c of this.partners[id].customers) {
                    if (!c.loans)
                        c.loans = [];
                }
                window.console.log("Successs!", res);
                // retyping
            },
            (res) => {
                window.console.log("Error!", res);
            });
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

    this.retype = function (partner) {
        for (let customer of partner.customers) {
            for (let loan of customer.loans) {
                for (let prop in customer.loans) {
                    loan[this.capitalizeFirstLetter(prop)] = loan[prop];
                    delete loan[prop];
                }
            }
            for (let prop in customer) {
                customer[this.capitalizeFirstLetter(prop)] = customer[prop];
                delete customer[prop];
            }
        }
        for (let prop in partner) {
            if (prop === 'apr') {
                partner.APR = partner.apr;
                delete partner.apr;
                continue;
            }
            if (prop === 'ico') {
                partner.ICO = partner.ico;
                delete partner.ico;
                continue;
            }
            partner[this.capitalizeFirstLetter(prop)] = partner[prop];
            delete partner[prop];
        }
    }

    this.retypeBack = function (partner) {
        for (let customer of partner.Customers) {
            for (let loan of customer.Loans) {
                for (let prop in customer.Loans) {
                    loan[this.uncapitalizeFirstLetter(prop)] = loan[prop];
                    delete loan[prop];
                }
            }
            for (let prop in customer) {
                customer[this.uncapitalizeFirstLetter(prop)] = customer[prop];
                delete customer[prop];
            }
        }
        for (let prop in partner) {
            if (prop === 'APR') {
                partner.apr = partner.APR;
                delete partner.APR;
                continue;
            }
            if (prop === 'ICO') {
                partner.ico = partner.ICO;
                delete partner.ICO;
                continue;
            }
            partner[this.uncapitalizeFirstLetter(prop)] = partner[prop];
            delete partner[prop];
        }
    }

    this.init();
}

DataService.$inject = ["HttpService"]; 