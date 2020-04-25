#include <iostream>

#include "regionx.h"
#include "regiony.h"
#include "who.h"

using namespace std;

int main(int argc, char **argv)
{
	WHO who{"Who"};

	who.watch(new RegionX("A", 80844, 3199));
	who.watch(new RegionX("B", 148, 4));
	who.watch(new RegionY("C", 24747, 1809));
	who.watch(new RegionY("D", 7844, 293));
	who.report();

	return 0;
}
