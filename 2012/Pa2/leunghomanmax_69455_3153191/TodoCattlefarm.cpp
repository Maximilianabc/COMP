/*
 * File Created: Sun Mar 15 2020
 * Author:
 * SID:
*/

#include "TodoCattlefarm.h"
#include "Employee.h"
#include "TodoCow.h"
#include <cstdlib>

// Cattlefarm cost 80 upgrade cost 16
// Cattlefarm size 6, 6
// Cattlefarm init max num. of employee is CATTLEFARM_MAX_NUM_EMPLOYEE_MIN
// Cattlefarm Upgrade:
//      level increases by 1;
//      max num. of employees increases by 1 until CATTLEFARM_MAX_NUM_EMPLOYEE_MAX
// Cattlefarm Makemoney:
//      min(num. of cows, num. of feeders in work) x level x 10;

const int CATTLEFARM_COST = 80;
const int CATTLEFARM_UPGRADE_COST = 16;
const int CATTLEFARM_SIZE_X = 6; const int CATTLEFARM_SIZE_Y = 6;
const int CATTLEFARM_MAX_NUM_EMPLOYEE_MIN = 6;
const int CATTLEFARM_MAX_NUM_EMPLOYEE_MAX = (CATTLEFARM_SIZE_X-2) * (CATTLEFARM_SIZE_Y-2);

// TODO: Start to implement your code.

Cattlefarm::Cattlefarm(int x, int y) : Property(CATTLEFARM_COST, CATTLEFARM_UPGRADE_COST, CATTLEFARM_MAX_NUM_EMPLOYEE_MIN)
{
    setXY(x, y);
    setSize(CATTLEFARM_SIZE_X, CATTLEFARM_SIZE_Y);
}

void Cattlefarm::upgrade()
{
    Property::upgrade();
    int max = getMaxNumEmployee();
    if (max == CATTLEFARM_MAX_NUM_EMPLOYEE_MAX) return;
    setMaxNumEmployee(++max);
}

bool Cattlefarm::checkEmployee(Employee*) const
{
    const Employee** list = nullptr;
    getConstEmployeeList(list);
    if (list == nullptr) return false;

    int numeploy = getNumEmployee();
    for (int i = 0; i < numeploy; i++)
    {
        char sym = list[i]->getSymbol();
        if (sym != 'c' && sym != 'd')
        {
            delete[] list;
            return false;
        }
    }
    delete[] list;
    return true;
}

int Cattlefarm::makeMoney() const
{
    const Employee** list = nullptr;
    int numcow = 0;
    int numfeeder = 0;

    getConstEmployeeList(list);
    if (list != nullptr)
    {
        int numemploy = getNumEmployee();
        for (int i = 0; i < numemploy; i++)
        {
            char sym = list[i]->getSymbol();
            ObjectState state = list[i]->getState();
            if (sym == 'c' && state == ObjectState::WORK) numcow++;
            else if (sym == 'd' && state == ObjectState::WORK) numfeeder++;
        }
    }
    delete[] list;
    return (numcow <= numfeeder ? numcow : numfeeder) * getLevel() * 10;
}

void Cattlefarm::removeDiedCow()
{
    const Employee** list = nullptr;
    getConstEmployeeList(list);
    if (list == nullptr) return;

    int numeploy = getNumEmployee();
    for (int i = 0; i < numeploy; i++)
    {
        if (list[i]->getSymbol() == 'c')
        {
            if (!(static_cast<const Cow*>(list[i])->isAlive())) 
                fireEmployee(const_cast<Employee*>(list[i]));
        }
    }
    delete[] list;
}
