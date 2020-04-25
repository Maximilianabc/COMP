/*
 * File Created: Sun Mar 15 2020
 * Author:
 * SID:
*/

#include "TodoFarmland.h"
#include "Employee.h"
#include <cstdlib>

// Farmland cost 50 upgrade cost 10
// Farmland size 5, 5
// Farmland init max num. of employee is FARMLAND_MAX_NUM_EMPLOYEE_MIN
// Farmland Upgrade:
//      level increases by 1;
//      max num. of employee increases by 1 until FARMLAND_MAX_NUM_EMPLOYEE_MAX
// Farmland Makemoney:
//      getRandInt(0, num. of employees in work x 2 + num. of farmers in work x 6 + level x 3)

const int FARMLAND_COST = 50;
const int FARMLAND_UPGRADE_COST = 10;
const int FARMLAND_SIZE_X = 5; const int FARMLAND_SIZE_Y = 5;
const int FARMLAND_MAX_NUM_EMPLOYEE_MIN = 3;
const int FARMLAND_MAX_NUM_EMPLOYEE_MAX = (FARMLAND_SIZE_X-2) * (FARMLAND_SIZE_Y-2);

#ifdef __HARVEST_MOON_DEBUG__
    inline void debug_getRandInt(string s) {cout << s << endl;};
#else
    inline void debug_getRandInt(string s) {};
#endif

//return random int in [low, high).
int getRandInt(int low, int high) {
    int res = rand() % high + low;
    debug_getRandInt("getRandInt: low is "+to_string(low));
    debug_getRandInt("getRandInt: high is "+to_string(high));
    debug_getRandInt("getRandInt: res is "+to_string(res));
    return res;
}

// TODO: Start to implement your code.

Farmland::Farmland(int x, int y) : Property(FARMLAND_COST, FARMLAND_UPGRADE_COST, FARMLAND_MAX_NUM_EMPLOYEE_MIN)
{
    setXY(x, y);
    setSize(FARMLAND_SIZE_X, FARMLAND_SIZE_Y);
}

void Farmland::upgrade()
{
    Property::upgrade();
    int max = getMaxNumEmployee();
    if (max == FARMLAND_MAX_NUM_EMPLOYEE_MAX) return;
    setMaxNumEmployee(++max);
}

bool Farmland::checkEmployee(Employee*) const
{
    const Employee** list = nullptr;
    getConstEmployeeList(list);
    if (list == nullptr) return false;

    int numeploy = getNumEmployee();
    for (int i = 0; i < numeploy; i++)
    {
        char sym = list[i]->getSymbol();
        if (sym != 'e' && sym != 'r')
        {
            delete[] list;
            return false;
        }
    }
    delete[] list;
    return true;
}

int Farmland::makeMoney() const
{
    const Employee** list = nullptr;
    int numemployee = 0;
    int numfarmer = 0;

    getConstEmployeeList(list);
    if (list != nullptr)
    {
        int numemploy = getNumEmployee();
        for (int i = 0; i < numemploy; i++)
        {
            char sym = list[i]->getSymbol();
            ObjectState state = list[i]->getState();
            if (sym == 'e' && state == ObjectState::WORK) numemployee++;
            else if (sym == 'r' && state == ObjectState::WORK) numfarmer++;
        }
    }
    delete[] list;
    return getRandInt(0, numemployee * 2 + numfarmer * 6 + getLevel() * 3);
}
