/*
 * File Created: Mon Mar 23 2020
 * Author: Peng YUN (pyun@ust.hk)
 * You do not need to change this file.
*/

#ifndef CONTROLLER_H_
#define CONTROLLER_H_

#include "Scene.h"
#include "View.h"
#include "definition.h"
#include "TodoFarmland.h"
extern const int FARMLAND_SIZE_X;
extern const int FARMLAND_SIZE_Y;
extern const int CATTLEFARM_SIZE_X;
extern const int CATTLEFARM_SIZE_Y;
/**
 * The Controller in the MVC model. It acts like the big brother to dispatch jobs and ask
 * the View to render the screen.
 */
class Controller {
public:
	/**
	 * Default constructor
	 */
	Controller();
	/**
	 * Destructor
	 */
	virtual ~Controller();
	/**
	 * The program entry to start the game
	 */
	virtual void run();
private:
	/**
	 * The main looping function that handles inputs from the user. 
	 * Rather than taking a concrete key stroke, this function handles 
	 * only the option and the coordinate that a user has chosen
	 * from the View class.
	 */
	virtual bool handleInput();


	/**
	 * An model class
	 */
	Scene *scene;
	/**
	 * A view class
	 */
	View *view;
	/**
	 * The state of the game
	 */
	gamestate state;
	/**
	 * To record which Object is selected by the user
	 */
	Object *selectObject;


	int cursor_x, cursor_y;

};

#endif /* CONTROLLER_H_ */
