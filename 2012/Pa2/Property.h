/*
 * File Created: Sun Mar 15 2020
 * Author: Peng YUN (pyun@ust.hk)
 * You do not need to change this file.
*/

#ifndef PROPERTY_H_
#define PROPERTY_H_

#include "Object.h"

class Employee;

/**
 * Property is an abstract base class of other types of properties.
 * The member functions are explained as below.
 *
 */

class Property : public Object {
public:
    // The first parameter is the cost to build the property.
    // The second parameter is the cost to upgrade the property.
    // The third parameter is the max number of employees.
    Property(int, int, int);

    virtual ~Property();

    // Return the building cost of the property.
    int getCost() const { return m_cost; };

    // Return the upgrade cost of the property.
    int getUpgradeCost() const { return m_upgrade_cost; };

    // Return the level of the property.
    int getLevel() const { return m_level; };

    // Return the number of the employees assigned to the property.
    int getNumEmployee() const { return m_num_employee; };

    // Return the max number of the employees.
    int getMaxNumEmployee() const { return m_max_num_employee; };

    // Set the max number of the employees.
    void setMaxNumEmployee(int);

    // Return the const employee list.
    void getConstEmployeeList(const Employee**&) const;

    // Upgrade the property.
    virtual void upgrade();

    // Check whether the employee is suitable for this property.
    // Farmland: Employee, Farmer
    // Cattlefarm: Feeder, Cow
    virtual bool checkEmployee(Employee*) const = 0;

    // Assign an employee to the property.
    // You should update the m_employee_list (with shallow copying), m_num_employee
    // and set the x,y of Employee.
    // All the employees should always be inside the range of their property.
    // If the Property, e.g.Farmland(R), is at (0, 3) with size (5, 5),
    // the first employee should at (1, 4);
    // the second employee should at (1, 5);
    //  012346x
    // 3RRRRR
    // 4R147R
    // 5R258R
    // 6R369R
    // 7RRRRR
    // y
    // If reaches m_max_num_employee, it fails to assign.
    // If the employee already in the list, it fails to assign.
    // If the employee is unsuitable to the property, it fails to assign.
    // Return true, if assign successfully.
    // Otherwise, return false.
    bool assignEmployee(Employee*);

    // Fire an employee.
    // You should update the m_employee_list, m_num_employee
    // and completely delete the employee.
    // Assumption: You can assume the input parameter is in the m_employee_list if m_employee_list is not nullptr.
    // Hint: You have to take care of the position of other employees after deleting one.
    // Otherwise, it may cause a problem when you assigning new employees.
    // Please note all the employees should always be inside their property.
    // We gave some examples to clarify the "inside" and "outside" on the webpage.
    // Return true, if assign successfully.
    bool fireEmployee(Employee*);

    // compute the money in each round according to the specification.
	virtual int makeMoney() const = 0;

    //return ObjectType::PROPERTY to identify that is an EMPLOYEE type.
    virtual ObjectType getObjectType() const override {return ObjectType::PROPERTY;}

protected:
	//To output its details to an ostream. i.e. cout << aPropertyObject << endl;
	virtual void writeToStream(ostream& os) const override;

private:
    int m_cost;
    int m_upgrade_cost;
    int m_level = 1;
    Employee** m_employee_list = nullptr;
    int m_num_employee = 0;
    int m_max_num_employee;
};
#endif /*PROPERTY_H_*/