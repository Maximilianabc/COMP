#ifndef CANVAS_H
#define CANVAS_H

#include "patch.h"

struct PatchNode
{
    Patch *patch;
    PatchNode *above;
    PatchNode *below;
};

struct Canvas
{
    int height;
    int width;
    PatchNode *top;
    PatchNode *bottom;
    PatchNode *selected;
};

// task 2
void append_to_bottom(Canvas *canvas, Patch *patch);
void clear(Canvas *&canvas);
char **render(const Canvas *canvas);

// task3
void bring_selected_above(Canvas *canvas);
void send_selected_below(Canvas *canvas);

// task 4
void select_at(Canvas *canvas, int x, int y);
void erase_pixel_at(Canvas *canvas, int x, int y);

#endif