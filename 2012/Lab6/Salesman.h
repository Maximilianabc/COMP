#ifndef SALESMAN_H
#define SALESMAN_H
#include "Person.h"
class Salesman: public Person
{

public:
	Salesman(const string _id, const string _name );
	~Salesman();

	//Todo: Declare the print function here 
	void print() const override;
};


#endif /* SALESMAN_H */
