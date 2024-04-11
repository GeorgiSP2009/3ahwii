const { fakerDE } = require('@faker-js/faker');
//console.log{'Hello, world!'};

for (let i = 0; i < 10; i++) {
    console.log(fakerDE.finance.iban());
}
