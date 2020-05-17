/*
 * File Created: Mon Mar 23 2020
 * Author: Peng YUN (pyun@ust.hk)
 * You do not need to change this file.
*/

#include "Controller.h"
#include <string>
#include <sstream>
#include "definition.h"

using namespace std;

Controller::Controller() :
		state(NAVIGATE), selectObject(nullptr), cursor_x(0), cursor_y(0) {
	scene = new Scene();
	view = new View(scene);
	view->renderAll();
}

Controller::~Controller() {
	delete view;
	delete scene;
}

// Return true if in the boundary
bool checkBoundary(int building, int cursor_x, int cursor_y)
{
	int x, y;
	switch(building){
	case FARMLAND:
		x = FARMLAND_SIZE_X;
		y = FARMLAND_SIZE_Y;
		break;
	case CATTLEFARM:
		x = CATTLEFARM_SIZE_X;
		y = CATTLEFARM_SIZE_Y;
		break;
	}
	return (x + cursor_x <= PROPERTY_X) && (y + cursor_y <= SCENE_H);
}

void Controller::run() {
	bool running = true;
	view->pause(
			"This is PA2. \nF1 or Q - exit; Arrow keys, space/enter to control.");
	while (running) {
		running = handleInput();
	}
}

bool Controller::handleInput() {
	int x = 0, y = 0;

	switch (state) {
	case NAVIGATE:
		if (scene->isGameOver()) {
			view->pause("Game Over");
			return false;
		}

		switch (view->handleNavigate(x, y)) {
		case EXIT:
			return false;
		case OKAY:
		default:
			Object* r = scene->getObjectAt(x, y);
			if (r != nullptr && r->getObjectType() == ObjectType::PROPERTY) {
				selectObject = r;
				state = PROPERTY_COMMAND;
			}
			else if (r != nullptr && r->getObjectType() == ObjectType::EMPLOYEE) {
				selectObject = r;
				state = EMPLOYEE_COMMAND;
			}
			else {

				int code = view->handleCommand(MENU_LIST);
				if (code == M_END_TURN) {
					view->pause(string("Make money and Pay for employees. Next round starts"));
					scene->nextRound();
				} else if (code == M_BUILD) {
					if (scene->getObjectAt(x,y) == nullptr) {
						state = BUILDING;
						cursor_x = x;
						cursor_y = y;
					}
				}

			}
		}
		break;

	case BUILDING:
	{
		int building = view->handleCommand(BUILD_LIST);
		if (building == CANCEL_P)
			state = NAVIGATE;
		else 
		{
			if (!checkBoundary(building, cursor_x, cursor_y))
				view->pause("Out of Boundary");
			else
			{
				view->pause("Purchase a Property!");
				scene->addProperty(building, cursor_x, cursor_y);
			}
			state = NAVIGATE;
		}
	}
		break;

	case PROPERTY_COMMAND:
		switch (view->handleCommand(CMD_LIST)) {
		case EXIT:
			return false;
		case P_DESTROY:
			scene->removeProperty(dynamic_cast<Property*>(selectObject));
			selectObject = nullptr;
			state = NAVIGATE;
			break;
		case P_UPGRADE:
			state = NAVIGATE;
			if (!scene->upgrade(static_cast<Property*>(selectObject)))
				view->displayMessage("Insufficient Money to upgrade");
			break;
		case P_HIRE:
		{
			int employee = view->handleCommand(HIRE_LIST);
			if (employee == CANCEL_E)
				state = NAVIGATE;
			else 
			{
				view->pause("Hire an Employee!");
				if(!scene->hire(dynamic_cast<Property*>(selectObject), employee))
					view->pause("Hire Failed Due to Insufficient Money, No Vacancies or Unsuitable Employee!");
				state = NAVIGATE;
			}
		}
			break;
		case P_CANCEL:
			state = NAVIGATE;
			selectObject = nullptr;
			break;
		}
		break;

		case EMPLOYEE_COMMAND:
			switch (view->handleCommand(CMDE_LIST)) {
			case EXIT:
				return false;
			case E_FIRE:
				scene->fire(dynamic_cast<Employee*>(selectObject));
				selectObject = nullptr;
				state = NAVIGATE;
				break;
			case E_CANCEL:
				state = NAVIGATE;
				selectObject = nullptr;
				break;
			}
			break;
	}

	return true;
}