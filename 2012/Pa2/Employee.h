/*
 * File Created: Sun Mar 15 2020
 * Author: Peng YUN (pyun@ust.hk)
 * You do not need to modify this file.
*/

#ifndef EMPLOYEE_H_
#define EMPLOYEE_H_

#include "Object.h"

class Employee: public Object 
{
public:
    Employee();

    //The first parameter is the cost, i.e. the cost once hiring the employee.
    //The second parameter is the salary.
    //The third parameter is the number of work days.
    //The fourth parameter is the number of rest days.
    //Each employee should be initialized as ObjectState::NORMAL State.
    //After being assigned to a property, its state should be changed to WORK/REST.
    Employee(int, int, int, int);

    virtual ~Employee();

    //Return the cost of the employee.
    int getCost() const;

    //Return the number of work days of the employee.
    int getNumWorkDays() const;

    //Return the number of rest days of the employee.
    int getNumRestDays() const;

    //Return the work age of the employee.
    int getWorkAge() const;

	//Return the salary according to the specification.
	virtual int getSalary() const;

    //Update the state: Work/Rest according to the m_work_age and the specification
    //If the employee has not been assigned to a property,
    //Its state is always NORMAL.
    //If the employee has been assigned.
    //Its state changes between WORK(first)/REST.
    void updateState();

    //Increase the workage by one
    void updateWorkAge();

	//return ObjectType::EMPLOYEE to identify that is an EMPLOYEE type.
	virtual ObjectType getObjectType() const override {return ObjectType::EMPLOYEE;}

    //return the "Employee"
	string getName() const override;
    
    //return the "e" (Captain Sensitive)
	char getSymbol() const override;

protected:
	//override base class function.
	virtual void writeToStream(ostream& os) const override;

private:
    int m_cost;
    int m_salary;
    int m_ndays_work;
    int m_ndays_rest;
    int m_work_age=0; // number of rounds the employee has finished.
};
#endif /*EMPLOYEE_H_*/