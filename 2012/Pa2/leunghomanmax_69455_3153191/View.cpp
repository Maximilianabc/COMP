/*
 * File Created: Mon Mar 16 2020
 * Author: Peng YUN (pyun@ust.hk)
 * You do not need to change this file.
*/

#include "View.h"
#include "Property.h"
#include <iostream>
#include <cstring>
#include <sstream>

using namespace std;

/**
 * Reason for not moving the following parameters to definition.h is to allow
 * further extension of the view class. For example one may like to extend a GUI
 * view class using Qt!
 *
 * But the size of SCENE does matter as it is governing the logic of scene. So these
 * parameters are actually defined somewhere else.
 */

extern const int SCENE_W;
extern const int SCENE_H;
extern const int PROPERTY_X;

const int SCENE_BOX_W = SCENE_W + 2;
const int SCENE_BOX_H = SCENE_H + 2;

const int MSG_X = 0;
const int MSG_Y = 0; //scene_BOX_H + SCENE_Y;
const int MSG_W = SCENE_BOX_W;
const int MSG_H = 5;

const int SCENE_X = 0;
const int SCENE_Y = MSG_H;

const int STAT_X = 0;
const int STAT_Y = SCENE_Y + SCENE_BOX_H;
const int STAT_H = 12;
const int STAT_W = 25;

const int COMM_Y = STAT_Y;
const int COMM_X = STAT_X + STAT_W;
const int COMM_H = STAT_H;
const int COMM_W = SCENE_BOX_W - STAT_W;

const char* PROPERTY_COMMANDS[] = { "Destroy", "Hire", "Upgrade", "Cancel" };
const char* EMPLOYEE_COMMANDS[] = { "Fire", "Cancel" };
const char* MENUS[] = { "End Main Turns", "Build", "Cancel" };
const char* PROPERTIES[] = {"Farmland", "CattleFarm", "Cancel"};
const char* EMPLOYEES[] = {"Employee", "Farmer", "Feeder", "Cow", "Cancel"};
enum {UP, DOWN, LEFT, RIGHT};


View::View(Scene* sScene) : scene(sScene)
{
	initscr();
	clear();
	noecho();
	curs_set(0);
	cbreak(); /* Line buffering disabled. pass on everything */

	keypad(stdscr, TRUE);
	raw();
	nonl();

	cursor_x = 0;
	cursor_y = 0;

	refresh();

	msg_win_box = newwin(MSG_H, MSG_W, MSG_Y, MSG_X);
	box(msg_win_box, 0, 0);
	wrefresh(msg_win_box);
	scene_win_box = newwin(SCENE_BOX_H, SCENE_BOX_W, SCENE_Y, SCENE_X);
	box(scene_win_box, 0, 0);
	wrefresh(scene_win_box);
	stat_win_box = newwin(STAT_H, STAT_W, STAT_Y, STAT_X);
	box(stat_win_box, 0, 0);
	wrefresh(stat_win_box);
	comm_win_box = newwin(COMM_H, COMM_W, COMM_Y, COMM_X);
	box(comm_win_box, 0, 0);
	wrefresh(comm_win_box);

	msg_win = newwin(MSG_H - 2, MSG_W - 2, MSG_Y + 1, MSG_X + 1);
	wrefresh(msg_win);
	scene_win = newwin(SCENE_H, SCENE_W, SCENE_Y + 1, SCENE_X + 1);
	wrefresh(scene_win);
	stat_win = newwin(STAT_H - 2, STAT_W - 2, STAT_Y + 1, STAT_X + 1);
	wrefresh(stat_win);
	comm_win = newwin(COMM_H - 2, COMM_W - 2, COMM_Y + 1, COMM_X + 1);
	wrefresh(comm_win);
}

View::~View() {
	clrtoeol();
	refresh();
	endwin();
	delwin(msg_win);
	delwin(comm_win);
	delwin(stat_win);
	delwin(scene_win);
	delwin(msg_win_box);
	delwin(scene_win_box);
	delwin(stat_win_box);
	delwin(comm_win_box);
}


void View::renderScene() {
	const Object** objects;
	int num_objects;

	bool cursor_display = false;
	wclear(scene_win);

	scene->getConstObjects(objects, num_objects);
	// Render Property First to avoid overlapping
	for (int i = 0; i < num_objects; i++) {
		int x, y;
        int sz_x, sz_y;
		const Object* r = objects[i];
		if(r->getObjectType() == ObjectType::EMPLOYEE)
			continue;
		r->getXY(x, y);
        r->getSize(sz_x, sz_y);
		char c = r->getSymbol();
		wattron(scene_win, A_BOLD);
		if (cursor_x >= x && cursor_x < x + sz_x && cursor_y >= y && cursor_y < y + sz_y) {
			wattron(scene_win, A_REVERSE);
			for(int xx = x; xx < x+sz_x; xx++)
				for(int yy = y; yy < y+sz_y; yy++)
					mvwaddch(scene_win, yy, xx, c);
			wattroff(scene_win, A_REVERSE);
			cursor_display = true;
		} else {
			for(int xx = x; xx < x+sz_x; xx++)
				for(int yy = y; yy < y+sz_y; yy++)
					mvwaddch(scene_win, yy, xx, c);
		}
		wattroff(scene_win, A_BOLD);
	}
	// Then Render Employees
	for (int i = 0; i < num_objects; i++) {
		int x, y;
        int sz_x, sz_y;
		const Object* r = objects[i];
		if(r->getObjectType() == ObjectType::PROPERTY)
			continue;
		r->getXY(x, y);
        r->getSize(sz_x, sz_y);
		char c = r->getSymbol();
		wattron(scene_win, A_BOLD);
		if (cursor_x >= x && cursor_x < x + sz_x && cursor_y >= y && cursor_y < y + sz_y) {
			wattron(scene_win, A_REVERSE);
			for(int xx = x; xx < x+sz_x; xx++)
				for(int yy = y; yy < y+sz_y; yy++)
					mvwaddch(scene_win, yy, xx, c);
			wattroff(scene_win, A_REVERSE);
			cursor_display = true;
		} else {
			for(int xx = x; xx < x+sz_x; xx++)
				for(int yy = y; yy < y+sz_y; yy++)
					mvwaddch(scene_win, yy, xx, c);
		}
		wattroff(scene_win, A_BOLD);
	}
	delete [] objects;
	if (!cursor_display) {
		wattron(scene_win, A_BLINK);
		mvwaddch(scene_win, cursor_y, cursor_x, '@');
		wattroff(scene_win, A_BLINK);
	}
	for (int i = 0; i < SCENE_H; i++)
		mvwaddch(scene_win, i, PROPERTY_X, '<' );

	mvwprintw(scene_win, 3, PROPERTY_X + 1, " Harvest");
	mvwprintw(scene_win, 5, PROPERTY_X + 1, "  Moon");
	wrefresh(scene_win);

}

void View::renderSize(const Property* p) {
	wattron(scene_win, A_REVERSE);

	for (int i = 0; i < SCENE_W; i++)
		for (int j = 0; j < SCENE_H; j++) {
			//print the highlighted map except for existing Object, cursor, and unreachable area
			if (scene->getObjectAt(i, j) == nullptr && p->isInRange(i, j)
					&& !(i == cursor_x && j == cursor_y)) {
				mvwaddch(scene_win, j, i, ' ');
			}
		}

	wattroff(scene_win, A_REVERSE);
	wrefresh(scene_win);
}

void View::displayMessage(string message) {
	wclear(msg_win);
	mvwprintw(msg_win, 0, 0, message.c_str());
	wrefresh(msg_win);
}


void View::renderStat(const Object* obj) {
	wclear(stat_win);
	wrefresh(stat_win);
	int line = 1;
	stringstream m;

	if (obj == nullptr && (obj = scene->getObjectAt(cursor_x, cursor_y)) == nullptr) {
		m << "Current Money: " << scene->getMoney() << endl;
		mvwprintw(stat_win, line++, 0, m.str().c_str());
	} else {
		m << "Name: " << obj->getName();
		mvwprintw(stat_win, line++, 0, m.str().c_str());
		m.str("");
		m.clear();

		m << (*obj);
		mvwprintw(stat_win, line++, 0, m.str().c_str());
		m.str("");
		m.clear();

		if (obj->getObjectType() == ObjectType::PROPERTY)
			renderSize(static_cast<const Property*>(obj));
	}

	wrefresh(stat_win);
}

void View::renderCommand(int cmd, int opt) {
	wclear(comm_win);
	int lastopt;
	const char** cmdText;

	switch (cmd) {
	case CMD_LIST:
		lastopt = LAST_OPTION;
		cmdText = PROPERTY_COMMANDS;
		break;
	case MENU_LIST:
		lastopt = M_LAST_OPTION;
		cmdText = MENUS;
		break;
	case BUILD_LIST:
		lastopt = P_LAST_OPTION;
		cmdText = PROPERTIES;
		break;
	case HIRE_LIST:
		lastopt = E_LAST_OPTION;
		cmdText = EMPLOYEES;
		break;
	case CMDE_LIST:
		lastopt = CMDE_LAST_OPTION;
		cmdText = EMPLOYEE_COMMANDS;
	}

	for (int i = 0; i <= lastopt; i++) {
		if (opt == i) { //highlight that one
			wattron(comm_win, A_REVERSE);
			mvwprintw(comm_win, i, 0, cmdText[i]);
			wattroff(comm_win, A_REVERSE);
		} else
			mvwprintw(comm_win, i, 0, cmdText[i]);
	}

	wrefresh(comm_win);
}

void View::renderAll() {
	renderScene();
	const Object* r = scene->getObjectAt(cursor_x, cursor_y);

	if (r != nullptr)
		renderStat(r);
}

/*************************************************************************
 *
 * Input handlers
 *
 *
 *************************************************************************/

void View::pause(string message) {
	message += "\nHit any key to continue..";
	displayMessage(message);
	getch();
	wclear(msg_win);
	wrefresh(msg_win);
}



int View::handleCommand(int cmd) {

	int ch;

	int lastopt;
	switch (cmd) {
	case CMD_LIST: lastopt = LAST_OPTION; break;
	case CMDE_LIST: lastopt = CMDE_LAST_OPTION; break;
	case MENU_LIST: lastopt = M_LAST_OPTION; break;
	case BUILD_LIST: lastopt = P_LAST_OPTION; break;
	case HIRE_LIST: lastopt = E_LAST_OPTION; break;
	}
	int option = 0; // to return this value after all;

	renderCommand(cmd, option);
	displayMessage(string("Use Up/Down arrows and Enter/Space to select"));
	while ((ch = getch()) != KEY_F(1) && ch != 'q' && ch != 'Q') {
		switch (ch) {
		case KEY_UP:
			option--;
			if (option == -1)
				option = lastopt;
			break;
		case KEY_DOWN:
			option++;
			if (option > lastopt)
				option = 0;
			break;
		case '\n':case KEY_ENTER: case '\r':
		case ' ':
			return option;
		}

		renderCommand(cmd, option);
	}

	return EXIT;
}


void View::moveCursor(int d) {
	switch (d) {
	case UP:
		if (cursor_y > 0)
			cursor_y--;
		break;
	case DOWN:
		if (cursor_y < SCENE_H - 1)  // from 0 to SCENE_H - 1, height = SCENE_H
			cursor_y++;
		break;
	case LEFT:
		if (cursor_x > 0)
			cursor_x--;
		break;
	case RIGHT:
		if (cursor_x < SCENE_W - 1)
			cursor_x++;
		break;
	}
}

int View::handleNavigate(int& x, int&y) {
	string message = "Use Up/Down/Left/Right arrows to navigate and Enter/Space to select. Press F1 or Q to exit";
	int ch;

	displayMessage(message);
	renderStat();
	renderScene();
	wclear(comm_win);
	wrefresh(comm_win);

	while ((ch = getch()) != KEY_F(1) && ch != 'q' && ch != 'Q') {
		switch (ch) {

		case KEY_UP:
			message += "UP KEY";
			moveCursor(UP);
			break;
		case KEY_DOWN:
			message += "DOWN KEY";
			moveCursor(DOWN);
			break;
		case KEY_LEFT:
			message += "LEFT KEY";
			moveCursor(LEFT);
			break;
		case KEY_RIGHT:
			message += "RIGHT KEY";
			moveCursor(RIGHT);
			break;
		case '\n':case KEY_ENTER: case '\r':
		case ' ':
			x = cursor_x;
			y = cursor_y;
			return OKAY;
		}

		renderScene();
		renderStat();
	}

	return EXIT;
}