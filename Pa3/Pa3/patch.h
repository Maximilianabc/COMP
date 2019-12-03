#ifndef PATCH_H
#define PATCH_H

#include <string>

using namespace std;

struct Line
{
    string data;
    Line *next;
};

struct Patch
{
    int x, y;
    Line *head;
    Line *tail;
};

// task 1
void append_to_bottom(Patch *patch, Line *line);
void clear(Patch *&patch);
void reverse(Patch *patch);

#endif