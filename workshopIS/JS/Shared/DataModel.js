function Loan(duration, amount, customer, monthlyCharge, apr, interest, note) {
    this.duration = duration;
    this.amount = amount;
    this.customer = customer;
    this.monthlyCharge = monthlyCharge || null;
    this.apr = apr || null;
    this.interest = interest || null;
    this.note = note || null;
}

function Customer(phone, creationDate, partner, firstName, surname, email, contactState, loans) {
    this.phone = phone;
    this.creationDate = creationDate;
    this.partner = partner;
    this.firstName = firstName || null;
    this.surname = surname || null;
    this.email = email || null;
    this.contactState = contactState || 0;
    this.loans = loans || [];
}

function Partner(name, ico, validFrom, validTo, fileData, customers) {
    this.name = name;
    this.ico = ico;
    this.validFrom = validFrom;
    this.validTo = validTo || null;
    this.fileData = fileData || null;
    this.customers = customers || [];
}