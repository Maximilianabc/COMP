/*
 * File Created: Tue Mar 17 2020
 * Author: Peng YUN (pyun@ust.hk)
 * You do not need to change this file.
*/

#ifndef SCENE_H_
#define SCENE_H_

extern const int SCENE_W;
extern const int SCENE_H;
extern const int PROPERTY_X;

#include "Object.h"
#include "Property.h"
/**
 * The Scene class
 * It manages the coordinate of the game board and maintains the
 * size of the Scene (Scene_W / Scene_H) as defined in Scene.cpp
 *
 * Please find the specification of the function in Scene.cpp
 */

class Scene {
public:
	Scene();

	virtual ~Scene();

	// If m_money < 0, return true.
	bool isGameOver() const;

	// Add an object(Employee or Property) to the scene
	void addObject(Object*);

	// Remove the Property in the scene
	void removeProperty(Property*);

	// Return objects and num_objects.
	void getConstObjects(const Object**&, int&) const;

	// Get the object at the specific aX and aY.
	// If employee at (aX, aY), directly select it.
	// If no employee selected, check whether (aX, aY) is in the range of a property.
	// If no employee or property get selected, return nullptr.
	Object* getObjectAt(int aX, int aY) const;

	// Operations to finish this round before entering the next round.
	// - Make money
	// - Compute salary
	// - Update employee work age
	// - Update employee status
	// - Remove died cows
	void nextRound();

	// Add a property at (x, y)
	// The first parameter is property type. FARMLAND, CATTLEFARM.
	// The second parameter is x.
	// The third parameter is y.
	void addProperty(int, int, int);

	// Upgrade property
	bool upgrade(Property*);

	// Property hires an employee.
	// The first parameter is a Property pointer.
	// The second parameter is an employee type. EMPLOYEE, FARMER, FEEDER, COW.
	bool hire(Property*, int);

	// Property fires an employee.
	// The first parameter is an Employee pointer.
	// You should find its property and fire the employee from the property.
	bool fire(Employee*);

	// Get current money.
	int getMoney() const {return m_money;}

private:
	// Return true if the parameter property has overlap with any property in the m_objects.
	bool checkOverlap(const Property*);

	// Shallow remove an employee from the m_objects.
	void shallowRemoveEmployee(Employee*);

	Object** m_objects;
	int m_num_objects;
	int m_money;
};

#endif /* SCENE_H_ */
