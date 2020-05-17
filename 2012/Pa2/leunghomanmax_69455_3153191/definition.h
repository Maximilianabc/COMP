/*
 * File Created: Mon Mar 23 2020
 * Author: Peng YUN (pyun@ust.hk)
 * You do not need to change this file.
*/

#ifndef DEFINITION_H_
#define DEFINITION_H_

// Uncomment the line for DEBUG mode. 
// In paticular, it can give you some details of getRandInt() in TodoFarmland.cpp.
// #define __HARVEST_MOON_DEBUG__

/**
 * C++11 has introduced enum classes (also called scoped enumerations).
 * "enum class" declares a scoped enumeration type whose underlying type is int.
 * Here it helps us avoid the "redefinition-error" in the compilation.
 */
enum class ObjectType {EMPLOYEE, PROPERTY};
enum class ObjectState {NORMAL, WORK, REST, LAST=REST};
enum class Command{
    ERROR = -1,
    SPACE_BUILD, END_TURN,
    PROPERTY_UPGRADE, PROPERTY_DESTROY
};

/**
 * enum that used in Controller and View class only
 * 
 */

enum gamestate {NAVIGATE, BUILDING, PROPERTY_COMMAND, EMPLOYEE_COMMAND};
enum {P_DESTROY, P_HIRE, P_UPGRADE, P_CANCEL, LAST_OPTION=P_CANCEL, FIRST_OPTION=P_DESTROY};
enum {E_FIRE, E_CANCEL, CMDE_LAST_OPTION=E_CANCEL, CMDE_FIRST_OPTION=E_FIRE};
enum {ABORT = -2, EXIT = -1, OKAY};
enum {CMD_LIST, CMDE_LIST, MENU_LIST, BUILD_LIST, HIRE_LIST};
enum {M_END_TURN, M_BUILD, M_CANCEL, M_LAST_OPTION=M_CANCEL, M_FIRST_OPTION=M_END_TURN};
enum {FARMLAND, CATTLEFARM, CANCEL_P, P_FIRST_OPTION=FARMLAND, P_LAST_OPTION=CANCEL_P};
enum {EMPLOYEE, FARMER, FEEDER, COW, CANCEL_E, E_FIRST_OPTION=EMPLOYEE, E_LAST_OPTION=CANCEL_E};

#endif
