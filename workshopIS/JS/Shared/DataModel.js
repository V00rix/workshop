function Loan(id, duration, amount, customerId, monthlyCharge, apr, interest, note) {
    this.id = id || null;
    this.duration = duration || 0;
    this.amount = amount || 0;
    this.customerId = customerId;
    this.monthlyCharge = monthlyCharge || null;
    this.apr = apr || null;
    this.interest = interest || null;
    this.note = note || null;
}

function Customer(id, phone, creationDate, partnerId, firstName, surname, email, contactState, loans) {
    this.id = id || null;
    this.phone = phone;
    this.creationDate = creationDate;
    this.partnerId = partnerId || null;
    this.firstName = firstName || null;
    this.surname = surname || null;
    this.email = email || null;
    this.contactState = contactState || 0;
    this.loans = loans || [];
}

function Partner(id, name, ico, validFrom, validTo, fileData, customers) {
    this.id = id || null;
    this.name = name;
    this.ico = ico;
    this.validFrom = validFrom;
    this.validTo = validTo || null;
    this.fileData = fileData || null;
    this.customers = customers || [];
}