#ifndef PERSON_H
#define PERSON_H
#include <iostream>
using namespace std;
class Person
{
protected:
	string id;
	string name;

public:
	Person(){};
	Person(const string _id,  const string _name );
	virtual ~Person();

	string getName() const;
	string getID() const;

	virtual void print()  const = 0;
}; 
#endif /* PERSON_H */
