/*
 * File Created: Sun Mar 15 2020
 * Author: Peng YUN (pyun@ust.hk)
 * You do not need to change this file.
*/

#include "Object.h"
#include <string>
using std::string;

void Object::getXY(int& x, int& y) const {
	x = m_x;   y = m_y;
}

void Object::getSize(int& sz_x, int& sz_y) const {
    sz_x = m_sz_x;   sz_y = m_sz_y;
}

void Object::writeToStream(ostream& os) const {
	os << getName() << "(" << getSymbol() << ")" << endl
			<< "State: ";
	switch (m_state) {
	case ObjectState::NORMAL: os << "Normal" << endl; break;
	case ObjectState::WORK: os << "WORK" << endl; break;
	case ObjectState::REST: os << "REST" << endl; break;
	}
}

void Object::setState(ObjectState enumObjectState) {
	m_state =  enumObjectState;
}

ObjectState Object::getState() const {
	return m_state;
}

void Object::setXY(int x, int y) {
	m_x = x;   m_y = y;
}

void Object::setSize(int sz_x, int sz_y) {
	m_sz_x = sz_x;   m_sz_y = sz_y;
}

ostream& operator<< (ostream&os, const Object& o) {
	o.writeToStream(os);
	return os;
}

bool Object::isInRange(const int s_x, const int s_y) const {
    int x, y, sz_x, sz_y;
    getXY(x, y);
    getSize(sz_x, sz_y);
    return s_x >= x && s_x < x + sz_x && s_y >= y && s_y < y + sz_y;
}