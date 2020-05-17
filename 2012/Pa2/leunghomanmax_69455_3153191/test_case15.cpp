/*
 * File Created: Mon Mar 23 2020
 * Author: Peng YUN (pyun@ust.hk)
 * You can change this file to add more test cases.
*/

#include "Employee.h"
#include "TodoFarmer.h"
#include "TodoFeeder.h"
#include "TodoCow.h"
#include "Property.h"
#include "TodoFarmland.h"
#include "TodoCattlefarm.h"
#include "Scene.h"
#include <string>
#include <sstream>
#include <fstream>

using namespace std;

std::string output_path = "";

void runTestCase();
void output(string s) {
    static int conter = 0;
    ofstream f(output_path, std::ios_base::app);
    f << ++conter << " : " << s << endl;
    f.close();
};
void output_object(const Object* obj, bool position=false) {
    static int conter = 0;
    ofstream f(output_path, std::ios_base::app);
    f << "OBJ" << ++conter << " : " << *obj;
    // print position here.
    if (position) {
        int x, y, sz_x, sz_y;
        obj->getXY(x, y);
        obj->getSize(sz_x, sz_y);
        f << "Position" <<"(" <<x<<" "<<y <<")";
    }
    f << endl;
    f.close();
}

int main(int argc, char *argv[]) {
    output_path = argv[1];
    runTestCase();
    return 0;
}

void runTestCase() {
    // Test Hire & Fire
    // New a Scene
    Scene* s = new Scene;
    // Create a Farmland at (10, 2)
    s->addProperty(FARMLAND, 10, 2);
    Farmland* farmland = dynamic_cast<Farmland*>(s->getObjectAt(10, 2));
    output(to_string(s->getMoney()));
    // Update 6 times
    for(int i=0; i<6; i++) 
        s->upgrade(farmland);
    output_object(farmland, true);
    output(to_string(s->getMoney()));
    // Hire 9 Employees
    for(int i=0; i<9; i++)
        s->hire(farmland, EMPLOYEE);
    output_object(farmland, true);
    output(to_string(s->getMoney()));
    // Fire the one in the middle
    Employee* e_to_fire = dynamic_cast<Employee*>(s->getObjectAt(10, 5));
    s->fire(e_to_fire);
    output(to_string(s->getMoney()));
    // Test whether all inside the FarmLand
    const Employee** employee_list;
    farmland->getConstEmployeeList(employee_list);
    for(int i=0; i<farmland->getNumEmployee(); i++) {
        int x, y;
        employee_list[i]->getXY(x, y);
        bool is_inside = farmland->isInside(x, y);
        output(is_inside? "True" : "False");
    }
    delete [] employee_list;
    // Hire one Farmer
    s->hire(farmland, FARMER);
    output(to_string(s->getMoney()));
    // Test whether all inside the Farmland
    farmland->getConstEmployeeList(employee_list);
    for(int i=0; i<farmland->getNumEmployee(); i++) {
        int x, y;
        employee_list[i]->getXY(x, y);
        bool is_inside = farmland->isInside(x, y);
        output(is_inside? "True" : "False");
    }
    delete [] employee_list;
    delete s;
    // DONE is to test the crash problem
    output("DONE");
}
