#ifndef INVOICE_H
#define INVOICE_H

#include <iostream>
#include <string>
#include "Customer.h"
#include "Salesman.h"
#include "Product.h"
using namespace std;
class Invoice
{

private:
	int quantity;
	string id, date;
	float cost;
	Person* buyer; //This Person* point to a Customer object
	Person* seller; //This Person* point to a Salesman object
	Product* productSale;

public:
	Invoice(const string _id, const string _date,   Person* _buyer,  Person* _Seller,  Product* _productSale);
	~Invoice();

	//Todo: Declare the print function here
	void print() const;

	void createTransaction(int _qty);

};



#endif /* INVOICE_H */
