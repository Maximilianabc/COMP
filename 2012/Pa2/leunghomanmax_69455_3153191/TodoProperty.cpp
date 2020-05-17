/*
 * File Created: Sun Mar 15 2020
 * Author:
 * SID:
*/

#include "Property.h"
#include "Employee.h"

void Property::writeToStream(ostream& os) const {
	Object::writeToStream(os);
	os << "Cost:" << getCost() << endl;
    os << "Upgrade Cost:" << getUpgradeCost() << endl;
    os << "Level:" << getLevel() << endl;
    os << "# of Employee:" << getNumEmployee() << " / " << getMaxNumEmployee() << endl;
}

void Property::upgrade() {
    m_level++;
}

void Property::getConstEmployeeList(const Employee**& employee) const {
	if (m_num_employee == 0) {
		employee =nullptr;
		return;
	}
	employee = new const Employee*[m_num_employee];
	for (int i = 0; i < m_num_employee; i++)
		employee[i] = m_employee_list[i];
}

// TODO: Start to implement your code.

Property::Property(int cost, int upgrade_cost, int max)
{
	m_cost = cost;
	m_upgrade_cost = upgrade_cost;
	m_max_num_employee = max;
	setState(ObjectState::NORMAL);
}

Property::~Property()
{
	for (int i = 0; i < m_num_employee; i++)
	{
		delete m_employee_list[i];
	}
	delete[] m_employee_list;
}

void Property::setMaxNumEmployee(int max)
{
	m_max_num_employee = max;
}

bool Property::assignEmployee(Employee* employee)
{
	if (employee == nullptr || m_num_employee == m_max_num_employee) return false;

	char sym = this->getSymbol();
	char employsym = employee->getSymbol();
	if (sym == 'R')
	{
		if (employsym != 'r' && employsym != 'e') return false;
	}
	else if (sym == 'C')
	{
		if (employsym != 'c' && employsym != 'd') return false;
	}
	if (m_employee_list == nullptr)
	{
		m_employee_list = new Employee*[1];
		employee->setState(ObjectState::WORK);
		m_employee_list[0] = employee;
		m_num_employee = 1;
		return true;
	}
	else
	{
		for (int i = 0; i < m_num_employee; i++)
		{
			if (employee == m_employee_list[i]) return false;
		}

		Employee** copy = new Employee*[++m_num_employee];
		for (int i = 0; i < m_num_employee - 1; i++)
		{
			copy[i] = m_employee_list[i];
		}
		employee->setState(ObjectState::WORK);
		copy[m_num_employee - 1] = employee;
		delete[] m_employee_list;
		m_employee_list = copy;
	}

	int x, y;
	getXY(x, y);
	int width, height;
	getSize(width, height);

	for (int i = 0; i < m_num_employee; i++)
	{
		m_employee_list[i]->setXY(x + i / (width - 2) + 1, y + i % (width - 2) + 1);
	}
	return true;
}

bool Property::fireEmployee(Employee* employee)
{	
	if (employee == nullptr || m_num_employee == 0) return false;

	int index = -1;
	for (int i = 0; i < m_num_employee; i++)
	{
		if (m_employee_list[i] == employee)
		{
			index = i;
			break;
		}
	}
	if (index == -1) return false;

	Employee** copy = new Employee*[--m_num_employee];
	for (int i = 0; i < m_num_employee; i++)
	{
		copy[i] = i < index ? m_employee_list[i] : m_employee_list[i + 1];
	}
	
	delete employee;
	delete[] m_employee_list;
	m_employee_list = copy;

	int x, y;
	getXY(x, y);
	int width, height;
	getSize(width, height);

	for (int i = 0; i < m_num_employee; i++)
	{
		m_employee_list[i]->setXY(x + i / (width - 2) + 1, y + i % (width - 2) + 1);
	}
	return true;
}


int Property::getCost() const {
    return m_cost;
}

int Property::getUpgradeCost() const {
    return m_upgrade_cost;
}

int Property::getLevel() const {
    return m_level;
}

int Property::getMaxNumEmployee() const {
    return m_max_num_employee;
}
int Property::getNumEmployee() const {
    return m_num_employee;
}