/*
 * File Created: Mon Mar 16 2020
 * Author:
 * SID:
*/

#include "Scene.h"
#include "Property.h"
#include "TodoFarmland.h"
#include "TodoCattlefarm.h"
#include "Employee.h"
#include "TodoFarmer.h"
#include "TodoFeeder.h"
#include "TodoCow.h"

const int SCENE_W = 45;
const int SCENE_H = 10;
const int PROPERTY_X = 35;
const int SCENE_INIT_MONEY = 200;

Scene::Scene()
: m_objects(nullptr), m_num_objects(0), m_money(SCENE_INIT_MONEY) {
    nextRound();
}

Scene::~Scene() {
    //shallowremove employees
    for(int i = m_num_objects-1; i >= 0; i--) {
        if (m_objects[i]->getObjectType()==ObjectType::EMPLOYEE)
            shallowRemoveEmployee(dynamic_cast<Employee*>(m_objects[i]));
    }
    // remove properties
    for(int i = m_num_objects-1; i >= 0; i--) {
        if (m_objects[i]->getObjectType()==ObjectType::PROPERTY)
            delete m_objects[i];
    }
    delete[] m_objects;
}

void Scene::shallowRemoveEmployee(Employee* e) {
    if (m_num_objects <= 0) return;
    if (m_num_objects == 1 && e == m_objects[0]) {
        m_num_objects = 0;
        m_objects = nullptr;
        return;
    }
    int i;
	for (i = 0; i < m_num_objects; i++) {
		if (m_objects[i] == e)
			break;
	}
    Object ** newObjects = new Object*[m_num_objects--];
	for (int j = 0; j < m_num_objects; j++)
		newObjects[j] = m_objects[j];
	if (m_num_objects != i)
		newObjects[i] = m_objects[m_num_objects];
    delete [] m_objects;
    m_objects = newObjects;
}

bool Scene::checkOverlap(const Property* newproperty) {
    int x, y, sz_x, sz_y;
    newproperty->getXY(x, y);
    newproperty->getSize(sz_x, sz_y);
    for(int xx=x; xx<x+sz_x; xx++)
        for(int yy=y; yy<y+sz_y; yy++)
            if(getObjectAt(xx, yy) != nullptr) return true;
    return false;
}

void Scene::addProperty(int property, int x, int y) {
    Property* newProperty = nullptr;
    switch(property) {
    case FARMLAND:
        newProperty = new Farmland(x,y);
        break;
    case CATTLEFARM:
        newProperty = new Cattlefarm(x, y);
        break;
    }
    if (newProperty == nullptr) return;
    if (newProperty->getCost() > m_money || checkOverlap(newProperty)) {
        delete newProperty;
        return;
    }
    addObject(newProperty);
    m_money -= newProperty->getCost();
}

bool Scene::hire(Property* p, int employee) {
    Employee* newEmployee = nullptr;
    switch(employee){
    case EMPLOYEE:
        newEmployee = new Employee();
        break;
    case FARMER:
        newEmployee = new Farmer();
        break;
    case FEEDER:
        newEmployee = new Feeder();
        break;
    case COW:
        newEmployee = new Cow();
        break;
    }
    if (newEmployee == nullptr) return false;
    if (newEmployee->getCost() > m_money || ! p->assignEmployee(newEmployee)) {
        delete newEmployee;
        return false;
    }
    addObject(newEmployee);
    m_money -= newEmployee->getCost();
    return true;
}

Object* Scene::getObjectAt(int s_x, int s_y) const {
	int x, y, sz_x, sz_y;
    // If employee is at s_x, s_y, get employee
    // else, get property
    // otherwise return null
	for (int i = 0; i < m_num_objects; i++) {
        if (m_objects[i]->getObjectType() == ObjectType::PROPERTY)
            continue;
		m_objects[i]->getXY(x,y);
        m_objects[i]->getSize(sz_x,sz_y);
        if ( s_x >= x && s_x < x + sz_x && s_y >= y && s_y < y + sz_y)
			return m_objects[i];
	}
	for (int i = 0; i < m_num_objects; i++) {
        if (m_objects[i]->getObjectType() == ObjectType::EMPLOYEE)
            continue;
		m_objects[i]->getXY(x,y);
        m_objects[i]->getSize(sz_x,sz_y);
        if ( s_x >= x && s_x < x + sz_x && s_y >= y && s_y < y + sz_y)
			return m_objects[i];
	}
	return nullptr;
}

//perform shallow copying
void Scene::addObject(Object* newobj) {
	Object** newobjects = new Object*[m_num_objects + 1];
	for (int i = 0; i < m_num_objects; i++)
		newobjects[i] = m_objects[i];
	newobjects[m_num_objects++] = newobj;
	if (m_num_objects != 1)
		delete [] m_objects;
	m_objects = newobjects;
}

void Scene::getConstObjects(const Object**& obj, int& count) const {
	count = m_num_objects;
	if (m_num_objects == 0) {
		obj =nullptr;
		return;
	}
	obj = new const Object*[m_num_objects];
	for (int i = 0; i < m_num_objects; i++)
		obj[i] = m_objects[i];
}

// TODO: Start to implement your code.

bool Scene::isGameOver() const { return m_money < 0; }

void Scene::removeProperty(Property* prop)
{
    int index = -1;
    for (int i = 0; i < m_num_objects; i++)
    {
        if (m_objects[i] == prop)
        {
            index = i;
        }
    }
    if (index == -1) return;

    Object** copy = new Object*[--m_num_objects];
    for (int i = 0; i < m_num_objects; i++)
    {
        copy[i] = i < index ? m_objects[i] : m_objects[i + 1];
    }
    delete[] m_objects;
    m_objects = copy;

    m_num_objects -= prop->getNumEmployee();
    delete prop;
}

void Scene::nextRound()
{
    int totalsalary = 0;
    int totalincome = 0;

    for (int i = 0; i < m_num_objects; i++)
    {
        ObjectType type = m_objects[i]->getObjectType();
        if (type == ObjectType::EMPLOYEE)
        {
            Employee* employee = const_cast<Employee*>(static_cast<const Employee*>(m_objects[i]));
            employee->updateWorkAge();
            employee->updateState();
            totalsalary += employee->getSalary();
        }
        else if (type == ObjectType::PROPERTY)
        {
            Property* prop = const_cast<Property*>(static_cast<const Property*>(m_objects[i]));
            totalincome += prop->makeMoney();
            if (prop->getSymbol() == 'C')
            {
                Cattlefarm* farm = static_cast<Cattlefarm*>(prop);
                const Employee** employees = nullptr;
                farm->getConstEmployeeList(employees);
                if (employees == nullptr) continue;

                int numeploy = farm->getNumEmployee();
                for (int i = 0; i < numeploy; i++)
                {
                    if (employees[i]->getSymbol() == 'c')
                    {
                        if (!(static_cast<const Cow*>(employees[i])->isAlive()))
                            fire(const_cast<Employee*>(employees[i]));
                    }
                }
                delete[] employees;
            }
        }
    }
    m_money += totalincome - totalsalary;
}

bool Scene::upgrade(Property* prop)
{
    int cost = prop->getUpgradeCost();
    if (prop == nullptr || getMoney() < cost) return false;
    prop->upgrade();
    m_money -= cost;
    return true;
}

bool Scene::fire(Employee* employee)
{
    if (employee == nullptr) return false;
    const Object** objs = nullptr;

    getConstObjects(objs, m_num_objects);
    if (objs == nullptr) return false;

    for (int i = 0; i < m_num_objects; i++)
    {
        if (objs[i]->getObjectType() == ObjectType::PROPERTY)
        {
            const Employee** employees = nullptr;
            const Property* prop = static_cast<const Property*>(objs[i]);
            prop->getConstEmployeeList(employees);
            if (employees == nullptr) continue;

            int numemploy = prop->getNumEmployee();
            for (int j = 0; j < numemploy; j++)
            {
                if (employees[j] == employee)
                {
                    int index = -1;
                    for (int i = 0; i < m_num_objects; i++)
                    {
                        if (m_objects[i] == employee)
                        {
                            index = i;
                        }
                    }
                    if (index == -1) return false;

                    Object** copy = new Object*[--m_num_objects];
                    for (int i = 0; i < m_num_objects; i++)
                    {
                        copy[i] = i < index ? m_objects[i] : m_objects[i + 1];
                    }
                    delete[] m_objects;
                    m_objects = copy;

                    const_cast<Property*>(prop)->fireEmployee(employee);
                    delete[] objs;
                    delete[] employees;
                    return true;
                }
            }
            delete[] employees;
        }
    }
    delete[] objs;
    return false;
}

