function Loan(id, duration, amount, customerId, monthlyCharge, apr, interest, note) {
    // properties
    this.id = id || null;
    this.duration = duration || 0;
    this.amount = amount || 0;
    this.customerId = customerId || null;
    this.monthlyCharge = monthlyCharge || null;
    this.apr = apr || null;
    this.interest = interest || null;
    this.note = note || null;

    // functions
    this.copy = function(data) {
        this.id = data.id || null;
        this.duration = data.duration || 0;
        this.amount = data.amount || 0;
        this.customerId = data.customerId || null;
        this.monthlyCharge = data.monthlyCharge || null;
        this.apr = data.apr || null;
        this.interest = data.interest || null;
        this.note = data.note || null;
    } 
}

Loan.From = function (data) {
    return new Loan(data.id,
        data.duration,
        data.amount,
        data.customerId,
        data.monthlyCharge,
        data.apr,
        data.interest,
        data.note);
}

function Customer(id, phone, creationDate, partnerId, firstName, surname, email, contactState, loans) {
    // properties
    this.id = id || null;
    this.phone = phone || null;
    this.creationDate = creationDate ? new Date(creationDate) : new Date();
    this.partnerId = partnerId || null;
    this.firstName = firstName || null;
    this.surname = surname || null;
    this.email = email || null;
    this.contactState = contactState || 0;
    this.loans = [];
    if (loans)
        for (var l of loans)
            this.loans.push(new Loan.From(l));

    // functions
    this.copy = function(data) {
        this.id = data.id || null;
        this.phone = data.phone || null;
        this.creationDate = data.creationDate ? new Date(data.creationDate) : new Date();
        this.partnerId = data.partnerId || null;
        this.firstName = data.firstName || null;
        this.surname = data.surname || null;
        this.email = data.email || null;
        this.contactState = data.contactState || 0;
        this.loans = [];
        if (data.loans)
            for (var l of data.loans)
                this.loans.push(new Loan.From(l));
    }
}

Customer.From = function (data) {
    return new Customer(data.id,
        data.phone,
        data.creationDate,
        data.partnerId,
        data.firstName,
        data.surname,
        data.email,
        data.contactState,
        data.loans);
}

function Partner(id, name, ico, validFrom, validTo, fileData, customers) {
    // properties
    this.id = id || null;
    this.name = name;
    this.ico = ico;
    this.validFrom = validFrom ? new Date(validFrom) : new Date();
    this.validTo= validTo ? new Date(validTo) : null;
    this.fileData = fileData || null;
    this.customers = [];
    if (customers)
        for (var c of customers)
            this.customers.push(new Customer.From(c));

    // functions
    this.copy = function(data) {
        this.id = data.id || null;
        this.name = data.name;
        this.ico = data.ico;
        this.validFrom = data.validFrom ? new Date(data.validFrom) : new Date();
        this.validTo = data.validTo ? new Date(data.validTo) : null;
        this.fileData = data.fileData || null;
        this.customers = [];
        if (data.customers)
            for (var c of data.customers)
                this.customers.push(new Customer.From(c));
    }
}

Partner.From = function (data) {
    return new Partner(data.id,
        data.name,
        data.ico,
        data.validFrom,
        data.validTo,
        data.fileData,
        data.customers);
}