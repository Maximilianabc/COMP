/*
 * File Created: Sun Mar 15 2020
 * Author:
 * SID:
*/

#include "TodoCow.h"

// Cow cost 15 salary 0.
// INF days work & 0 day rest.
// Lifespan 7.
const int COW_COST = 15;
const int COW_SALARY = 0;
const int INF = 99999;
const int COW_NUM_WORK_DAYS = INF;
const int COW_NUM_REST_DAYS = 0;
const int COW_LIFESPAN = 7;

void Cow::writeToStream(ostream& os) const 
{
	Object::writeToStream(os);
    os << "Work Age: " << getWorkAge() << endl;
    os << "Lifespan: " << getLifespan() << endl;
}

// TODO: Start to implement your code.

Cow::Cow() : Employee(COW_COST, COW_SALARY, COW_NUM_WORK_DAYS, COW_NUM_REST_DAYS), m_lifespan(COW_LIFESPAN)
{

}

Cow::~Cow()
{
}