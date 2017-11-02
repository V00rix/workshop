var DataService = function () {
    this.partners;
   
    this.init = function () {
        this.fakePartners();
        // getPartners();
        window.console.log("Data service initialized!");
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
            window.console.log("adding customers to " + partner);
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
        window.console.log(this.partners);
    }

    // GET request to server
    this.getPartners = function () {
        // some code, then
        // myHttpService....
    }

    // remove partner by Id
    this.deletePartner = function (id) {
        this.partners.splice(id, 1);
        // to server
    }

    this.updatePartner = function (id, partner) {
        this.partners[id] = partner;
        // to server
    }

    this.addPartner = function (partner) {
        // check if unique
        this.partners.push(partner);
        // to server
    }

    // adds new customer
    this.addCustomer = function (pid, customer) {
        this.partners[pid].customers.push(customer);
    }

    this.init();
}