#include <iostream>
#include "Customer.h"
#include "Product.h"
#include "Invoice.h"

using namespace std;

int main()
{
	Person *salesman = new Salesman("s001", "Sam");
	salesman->print();
	Person *customer = new Customer("c001","Candy", "candy@gmail.com", 1.1f);
	customer->print();
	Product laptop("p001","Laptop", 50, 5000);
	laptop.print();
	Invoice buylaptop("i001", "1/4/2020", customer, salesman, &laptop);
	buylaptop.createTransaction(10);

	delete salesman;
	delete customer;
	salesman = new Salesman("s002", "David");
	salesman->print();
	customer = new Customer("c002","Peter", "Peter@gmail.com", 0.7f);
	customer->print();
	Product printer("p002","Printer", 20, 1000);
	printer.print();
	Invoice buyPrinter("i002", "2/4/2020",customer,salesman, &printer);
	buyPrinter.createTransaction(25);
	buyPrinter.print();
	buyPrinter.createTransaction(10);
	buyPrinter.print();
	buyPrinter.createTransaction(5);
	buyPrinter.print();

	delete salesman;
	delete customer;
    return 0;
}
