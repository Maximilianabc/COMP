#ifndef CUSTOMER_H
#define CUSTOMER_H
#include "Person.h"
class Customer:public Person
{
private:
	string email;
	float discount;
public:
	Customer(const string _id, const string _name, const string _email,  float _discount );
	~Customer();
 
	string getEmail() const;
	
	//Todo: Declare getDiscount function here
	float getDiscount();

	//Todo: Declare the print function here
	void print() const override;
};
#endif /* CUSTOMER_H */
