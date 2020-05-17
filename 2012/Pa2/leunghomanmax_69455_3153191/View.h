/*
 * File Created: Mon Mar 16 2020
 * Author: Peng YUN (pyun@ust.hk)
 * You do not need to change this file.
*/

#ifndef VIEW_H_
#define VIEW_H_

#include "Scene.h"
#ifdef _WIN_
#include "./ncurses_lib/curses.h"
#else
#include <curses.h>
#endif
#include <string.h>

#include "Object.h"
#include "Employee.h"
#include "Property.h"
#include "definition.h"

/**
 * This is the View class is the MVC model. It is responsible for rendering screen and to do
 * simple interaction with the user. A sequence of key strokes will be turned to a command or option
 * and return to its caller - the controller.
 *
 * In order to display correct information, the View class also has some model class with it.
 */
class View {
public:

	/**
	 * Constructor.
	 */
	View(Scene*);
	/**
	 * Destructor
	 */
	virtual ~View();
	/**
	 * To display certain message on the screen
	 */
	virtual void displayMessage(std::string);

	/**
	 * During the Navigate state, the View class asks the user to freely navigate on the scene and
	 * select a cell, which can be empty or with a robot. When the cursor moves on a Robot,
	 * it will also render the statistics of the robot.
	 *
	 */
	virtual int handleNavigate(int&, int&);

	/**
	 * During the Command state and the Menu state, the View class asks the user to pick one of the commands
	 * from the command box. It triggers the next stage.
	 */
	virtual int handleCommand(int);

	/**
	 * To display a message and pause for a key stroke
	 *
	 */
	virtual void pause(std::string = "");

	/**
	 * To render all windows
	 */
	virtual void renderAll();

private:
	/**
	 * To render the Scene windows;
	 */ 
	virtual void renderScene();

	/**
	 * To render the Size of a Property.
	 */ 	
	virtual void renderSize(const Property*);

	/**
	 * To render the Stat windows with the selected object
	 */ 
	virtual void renderStat(const Object* = nullptr);

	/**
	 * To render the command list.
	 */
	virtual void renderCommand(int, int);

	/**
	 * The scene model.
	 */
	Scene* scene;

	void moveCursor(int);
	int cursor_x, cursor_y;
	WINDOW *msg_win, *scene_win, *stat_win, *comm_win;
	WINDOW *msg_win_box, *scene_win_box, *stat_win_box, *comm_win_box;
};

#endif /* VIEW_H_ */
