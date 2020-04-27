#include "Product.h"
#include <string>
Product::Product(const string _id, const string _name,  int _unit,  float _unitPrice )
{
	id = _id;
	name = _name;
	unit = _unit;
	unitPrice = _unitPrice;
}
Product::~Product(){
}

string Product::getID() {
	return id;
}
string Product::getName() {
	return name;
}

int Product::getUnit(){
	return unit;
}
void Product::setUnit(int _unit){
	unit = _unit;
}
float Product::getUnitPrice(){
	return unitPrice;
}
void Product::setUnitPrice(float _unitPrice){
	unitPrice = _unitPrice;
}
void Product::print(){
	cout << "Product: "<< id << " | " << name << " | Stock Unit: " << std::to_string(unit) << endl;
}
