/*
 * File Created: Sun Mar 15 2020
 * Author:
 * SID:
*/

#include "Employee.h"

// Employee cost 5 salary 1.
// Employee 1 day work & 1 day rest.
const int EMPLOYEE_COST = 5;
const int EMPLOYEE_SALARY = 1; // Get paied when in both work and rest states.
const int EMPLOYEE_NUM_WORK_DAYS = 1;
const int EMPLOYEE_NUM_REST_DAYS = 1;
// All types of Employees have size 1.
const int ALLEMPLOYEE_SIZE_X = 1; const int ALLEMPLOYEE_SIZE_Y = 1;

Employee::Employee()
: Employee(EMPLOYEE_COST, EMPLOYEE_SALARY, EMPLOYEE_NUM_WORK_DAYS, EMPLOYEE_NUM_REST_DAYS) {
}

Employee::Employee(int cost, int salary, int ndays_work, int ndays_rest)
: m_cost(cost), m_salary(salary), m_ndays_work(ndays_work), m_ndays_rest(ndays_rest) {
    setSize(ALLEMPLOYEE_SIZE_X, ALLEMPLOYEE_SIZE_Y);
}

Employee::~Employee() {
    //Auto-generated destructor stub
}

void Employee::writeToStream(ostream& os) const {
    Object::writeToStream(os);
    os << "Work Age: " << getWorkAge() << endl;
    os << "Salary: " << getSalary() << endl;
}

// TODO: Start to implement your code.

int Employee::getCost() const { return m_cost; }

int Employee::getNumWorkDays() const { return m_ndays_work; }

int Employee::getNumRestDays() const { return m_ndays_rest; }

int Employee::getWorkAge() const { return m_work_age; }

int Employee::getSalary() const { return m_salary; }

void Employee::updateState()
{
    if (m_work_age == 0)
    {
        setState(ObjectState::NORMAL);
        return;
    }
    int mod = m_work_age % (m_ndays_work + m_ndays_rest);
    if (mod == m_ndays_work)
        setState(ObjectState::REST);
    else if (mod == 0)
        setState(ObjectState::WORK);
}

void Employee::updateWorkAge() { m_work_age++; }

string Employee::getName() const { return "Employee"; }

char Employee::getSymbol() const { return 'e'; }