/*
 * File Created: Sun Mar 15 2020
 * Author:
 * SID:
*/

#ifndef COW_H_
#define COW_H_

#include "Employee.h"
// TODO: Start to implement your code.

class Cow : public Employee
{
public:
    Cow();
    ~Cow();
    string getName() const override { return "Cow"; };
    char getSymbol() const override { return 'c'; };
    int getLifespan() const { return m_lifespan; };
    bool isAlive() const { return getWorkAge() < m_lifespan; };
    void writeToStream(ostream& os) const override;
private:
    const int m_lifespan;
};
#endif /*COW_H_*/