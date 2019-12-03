#include <iostream>
#include <fstream>
#include <stdexcept>
#include "patch.h"
#include "canvas.h"

using namespace std;

// TA has helped to implement the following functions for you.
// You may take a look at them since some of them are calling your functions.

ifstream &open_file(ifstream &ifs, const string &path);

Patch *read_patch(ifstream &ifs);
void print(const Patch *patch);
void flip(Patch *patch, bool vertically);

Canvas *read_canvas(ifstream &ifs);
void print(const Canvas *canvas, bool verbose = false);

void deselect(Canvas *canvas);
void select_top(Canvas *canvas);
void select_bottom(Canvas *canvas);
void select_above(Canvas *canvas);
void select_below(Canvas *canvas);
void shift(Canvas *canvas, int dx, int dy);
