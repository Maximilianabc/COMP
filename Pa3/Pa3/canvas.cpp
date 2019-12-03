#include "canvas.h"

#include <iostream>

using namespace std;

void initialize(PatchNode* patchnode)
{
    patchnode->above = nullptr;
    patchnode->below = nullptr;
    patchnode->patch = nullptr;
}
/**
* Append to the bottom.
*
* @param canvas: a canvas pointer
* @param patch: a patch pointer
*/
void append_to_bottom(Canvas *canvas, Patch *patch)
{
    PatchNode *last = canvas->top;
    if (canvas->top == NULL)
    {
        canvas->top = new PatchNode;
        initialize(canvas->top);
        canvas->top->patch = patch;
        return;
    }
    while (last->below != NULL)
    {
        last->below->above = last;
        last = last->below;
    }
    last->below = new PatchNode;
    initialize(last->below);
    last->below->above = last;
    last->below->patch = patch;
    if (canvas->bottom == NULL)
    {
        canvas->bottom = new PatchNode;
        initialize(canvas->bottom);
    }
    canvas->bottom = last->below;
}

/**
* Delete the canvas and the resources owned by it.
*
* @param canvas, a canvas pointer.
*/
void clear(Canvas *&canvas)
{
    delete canvas;
}

/**
* Render the canvas to a buffer without border. Selected patch will be hightlighted as '@'.
*
* @param canvas, a canvas pointer.
* @return buffer, a height x width 2d array, required by the provided print function.
*/
char **render(const Canvas *canvas)
{
    char** buffer = new char*[canvas->height];
    for (int i = 0; i < canvas->height; i++)
    {
        buffer[i] = new char[canvas->width];
    }
    for (int i = 0; i < canvas->height; i++)
    {
        for (int j = 0; j < canvas->width; j++)
        {
            buffer[i][j] = ' ';
        }
    }
    PatchNode* lastpatch = canvas->top;
    while (lastpatch != NULL)
    {
        Line* lastline = lastpatch->patch->head;
        int x = lastpatch->patch->x;
        while (lastline != NULL)
        {
            char* cstr = new char[lastline->data.size() + 1];
            strcpy_s(cstr, lastline->data.size() + 1, lastline->data.c_str());
            if (lastpatch == canvas->selected)
            {
                for (int k = 0; cstr[k] != NULL; k++)
                {
                    if (cstr[k] != ' ') cstr[k] = '@';
                }
            }
            for (int y = 0; y < lastline->data.size(); y++)
            {
                if (y >= canvas->width) break;
                if (buffer[x][y + lastpatch->patch->y] == ' ')
                {
                    buffer[x][y + lastpatch->patch->y] = cstr[y];
                }
            }
            x++;
            if (x >= canvas->height) break;
            lastline = lastline->next;
        }
        lastpatch = lastpatch->below;
    }
    return buffer;
}

/**
* Bring the selected patch node above by one node, do nothing if not applicable.
*
* @param canvas, a canvas pointer.
*/
void bring_selected_above(Canvas *canvas)
{
    if (canvas->selected == canvas->top) return;
    canvas->selected->above->below = canvas->selected->below;
    canvas->selected->below->above = canvas->selected->above;
    canvas->selected->above = canvas->selected->above->above;
    canvas->selected->below = canvas->selected->above;
}

/**
* Send the selected patch node below by one node, do nothing if not applicable.
*
* @param canvas, a canvas pointer.
*/
void send_selected_below(Canvas *canvas)
{
    if (canvas->selected == canvas->bottom) return;
    if (canvas->selected == canvas->top)
    {
        /* 
        canvas->selected->above = canvas->selected->below;
        canvas->selected->below = canvas->selected->below->below;

        canvas->selected->above->above = NULL;
        canvas->selected->above->below = canvas->selected;
        canvas->selected->below->above = canvas->selected;
        canvas->top = canvas->selected->above; */
        canvas->selected->above = canvas->selected->below;


        canvas->selected->below = canvas->selected->below->below;
        canvas->selected->below->below = canvas->selected;
    }
    else
    {

    }
}

/**
* Select the top patch among the patches that covers (x, y). A patch conver a position means it has an non-transparant character on it. Selection of out of bound position should be allowed, despite that the position is not rendered.
*
* @param canvas, a canvas pointer.
* @param x, canvas row to select.
* @param y, canvas column to select.
*/
void select_at(Canvas *canvas, int x, int y)
{
}

/**
* Erase all characters at (x, y) until there is no non-transparant character on (x, y). If a patch node contains no non-transparant character after the erasing, it will be deleted. Erasing of out of bound position should be allowed, despite that the position is not rendered.
*
* @param canvas, a canvas pointer.
* @param x, canvas row to erase.
* @param y, canvas column to erase.
*/
void erase_pixel_at(Canvas *canvas, int x, int y)
{
}