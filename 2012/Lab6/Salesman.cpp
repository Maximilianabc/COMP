#include "Salesman.h"

Salesman::Salesman(const string _id, const string _name ): Person(_id, _name) {

}
Salesman::~Salesman(){
	cout << "Salesman " << name << " data deleted" << endl;
}


//Todo: implement the print function to output the Salesman data as same as result
void Salesman::print() const
{
	cout << "Salesman: " << id << " | " << name << endl;
}