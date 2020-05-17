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
    // Test a Normal Game Manner with correction
    // New a Scene
    Scene* s = new Scene;
    s->setMoney(5000);
    // Create a CattleFarm
    s->addProperty(CATTLEFARM, 2, 1);
    Cattlefarm* cattlefarm = dynamic_cast<Cattlefarm*>(s->getObjectAt(2, 1));
    // Hire One Employee Three Feeder Three Cow
    s->hire(cattlefarm, EMPLOYEE);
    s->hire(cattlefarm, FEEDER);
    s->hire(cattlefarm, COW);
    s->hire(cattlefarm, FEEDER);
    s->hire(cattlefarm, COW);
    s->hire(cattlefarm, FEEDER);
    s->hire(cattlefarm, COW);
    // Check CattleFarm
    output_object(cattlefarm, true);
    // Next Round 9 Times
    for(int i=0; i<9; i++) {
        s->nextRound();
        output_object(cattlefarm, true);
        // output(to_string(s->getMoney()));
    }
    // Hire Three Cows
    s->hire(cattlefarm, COW);
    s->hire(cattlefarm, COW);
    s->hire(cattlefarm, COW);
    s->setMoney(15);
    // Upgrade (check True / False)
    output(s->upgrade(cattlefarm)? "TRUE":"FALSE");
    s->setMoney(5000);
    // Next Round 3 Times
    for(int i=0; i<3; i++) {
        s->nextRound();
        output_object(cattlefarm, true);
        // output(to_string(s->getMoney()));
    }
    // Upgrade 3 Times
    output(s->upgrade(cattlefarm)? "TRUE":"FALSE");
    output(s->upgrade(cattlefarm)? "TRUE":"FALSE");
    output(s->upgrade(cattlefarm)? "TRUE":"FALSE");
    // Next Round 4 Times
    for(int i=0; i<4; i++) {
        s->nextRound();
        output_object(cattlefarm, true);
        // output(to_string(s->getMoney()));
    }
    // Build a Farmland
    s->addProperty(FARMLAND, 13, 1);
    Farmland* farmland = dynamic_cast<Farmland*>(s->getObjectAt(13, 1));
    // output(to_string(s->getMoney()));
    // Hire One Employee One Farmer One Feeder One Cow
    // Check Farmland
    s->hire(farmland, EMPLOYEE);
    s->hire(farmland, FARMER);
    s->hire(farmland, FEEDER);
    s->hire(farmland, COW);
    output_object(farmland);
    // output(to_string(s->getMoney()));
    // Next Round
    s->nextRound();
    // Destroy CattleFarm
    // Destroy Farmland
    s->removeProperty(cattlefarm);
    s->removeProperty(farmland);
    delete s;
    // DONE is to test the crash problem
    output("DONE");
}

