
#include "Person.h"
Person::Person(const string _id, const string _name ): id(_id),name(_name)
{
}

Person::~Person(){
	cout << "Person " << name << " data deleted" << endl;
};

string Person::getID() const{
	return id;
}
string Person::getName() const {
	return name;
}

