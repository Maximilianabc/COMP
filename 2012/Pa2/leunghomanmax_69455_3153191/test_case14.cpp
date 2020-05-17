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
    // Normal Game Manner
    // New a Scene
    Scene* s = new Scene();
    // Create Farmland at 6, 1
    s->addProperty(FARMLAND, 6, 1);
    Property* farmland = dynamic_cast<Property*>(s->getObjectAt(6, 1));
    // Hire 1 farmer 3 employee (1 cannot be hired)
    s->hire(farmland, FARMER);
    s->hire(farmland, EMPLOYEE);
    s->hire(farmland, EMPLOYEE);
    s->hire(farmland, EMPLOYEE);
    int num_obj;
    const Object** obj_list;
    s->getConstObjects(obj_list, num_obj);
    for(int i=0; i<num_obj; i++)
        output_object(obj_list[i], true);
    delete [] obj_list;
    // Upgrade 
    s->upgrade(farmland);
    // Hire 1 employee
    s->hire(farmland, EMPLOYEE);
    // Upgrade 3 times
    s->upgrade(farmland);
    s->upgrade(farmland);
    s->upgrade(farmland);
    s->getConstObjects(obj_list, num_obj);
    for(int i=0; i<num_obj; i++)
        output_object(obj_list[i], true);
    delete [] obj_list;
    // Fire the Employee on (8 2)
    Employee* e_to_fire = dynamic_cast<Employee*>(s->getObjectAt(8, 2));
    s->fire(e_to_fire);
    s->getConstObjects(obj_list, num_obj);
    for(int i=0; i<num_obj; i++)
        output_object(obj_list[i], true);
    delete [] obj_list;
    // Destroy Farmland
    s->removeProperty(farmland);
    s->getConstObjects(obj_list, num_obj);
    for(int i=0; i<num_obj; i++)
        output_object(obj_list[i], true);
    delete [] obj_list;
    // Delete Scene
    delete s;
    // DONE is to test the crash problem
    output("DONE");
}
