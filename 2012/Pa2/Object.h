/*
 * File Created: Sun Mar 15 2020
 * Author: Peng YUN (pyun@ust.hk)
 * You do not need to change this file.
*/

#ifndef OBJECT_H_
#define OBJECT_H_
#include <iostream>
#include <string>
#include "definition.h"
using namespace std;

class Object{
    friend ostream& operator<< (ostream& os, const Object& o);

public:
    virtual ~Object() = default;
    Object() = default;

	//Return a symbol of the object to be printed on screen.
	//Employee: e; Farmer: r; Feeder: d; Cow: c
	//Farmland: R; Cattlefarm: C
	virtual char getSymbol() const = 0;

	//Return the class name of the object to be printed on message box.
	virtual string getName() const = 0;

	//Return the coordinate of the object.
	void getXY(int&, int&) const;
    
    //Return the size of the object.
    void getSize(int&, int&) const;

	//Return the type of the object, can be either ObjectType::PROPERTY or ObjectType::EMPLOYEE
	virtual ObjectType getObjectType() const = 0;

	//Set the object State.
	void setState(ObjectState);

	//Return the object State
	ObjectState getState() const;

	//Set the coordinate of the object.
	void setXY(int, int);

    //Set the size of the object.
    void setSize(int, int);

    // check whether (x, y) is in the range of this property.
    bool isInRange(const int x,const int y) const;

protected:
	//write the content to ostream e.g. cout << anObject << endl; 
	virtual void writeToStream(ostream& os) const;

private:
	int m_x = -1; // x coordinate of an object
	int m_y = -1; // y coordinate of an object
    int m_sz_x = -1; // size of an object along the x-axis
    int m_sz_y = -1; // size of an object along the y-axis
	ObjectState m_state = ObjectState::NORMAL; // state of an object

};
#endif /*OBJECT_H_*/