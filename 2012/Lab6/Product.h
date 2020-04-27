#ifndef PRODUCT_H_
#define PRODUCT_H_
#include <iostream>
using namespace std;
class Product
{

private:
	string id, name;
	int unit;
	float unitPrice;
public:
	Product(const string _id, const string _name,  int unit,  float unitPrice);
	~Product();

	string getID();
	string getName();
 
	int getUnit();
	void setUnit(int _unit);
	float getUnitPrice();
	void setUnitPrice(float _unitPrice); 
	void print();
};
#endif /* PRODUCT_H_ */
