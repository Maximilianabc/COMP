#include "patch.h"

#include <cstring>
#include <iostream>
using namespace std;

/**
* Append a new line to the tail.
*
* @param patch: a patch pointer
* @param line: a line pointer
*/
void append_to_bottom(Patch *patch, Line *line)
{
    Line *last = patch->head;
    line->next = NULL;
    if (patch->head == NULL)
    {
        patch->head = line;
        patch->tail = line;
        return;
    }
    while (last->next != NULL)  last = last->next;
    last->next = line;
    patch->tail = line;
}

/**
* Delete the patch and the resources owned by it.
*
* @param patch, a patch pointer.
*/
void clear(Patch *&patch)
{
    delete patch;
}

/**
* Reverse the linked list of lines.
*
* @param patch, a patch pointer.
*/
void reverse(Patch *patch)
{
    Line* current = patch->head;
    Line *prev = NULL, *next = NULL;

    while (current != NULL)
    {
        next = current->next;
        current->next = prev;
        prev = current;
        current = next;
    }
    patch->head = prev;
}