#include "utils.h"
#include <iostream>
#include <fstream>
using namespace std;

ifstream &open_file(ifstream &ifs, const string &path)
{
    ifs.open(path);
    if (!ifs)
    {
        throw runtime_error("Could not open file: \"" + path + "\", please check whether your file is placed in the right path.");
    }
    return ifs;
}

void initialize(Patch *patch)
{
    patch->head = nullptr;
    patch->tail = nullptr;
    patch->x = 0;
    patch->y = 0;
}

Patch *read_patch(ifstream &ifs)
{
    string buffer;

    // consume line until +-...-+
    while (getline(ifs, buffer) && buffer[0] != '+')
        ;

    if (buffer[0] != '+')
    {
        return nullptr;
    }

    Patch *patch = new Patch;
    initialize(patch);

    while (getline(ifs, buffer) && buffer[0] != '+')
    {
        while (buffer.back() != '|')
        {
            buffer.pop_back();
        }
        Line *line = new Line;
        line->data = buffer.substr(1, buffer.size() - 2);
        line->next = nullptr;
        append_to_bottom(patch, line);
    }

    ifs >> buffer;
    patch->x = stoi(buffer.substr(2));
    ifs >> patch->y >> buffer;

    return patch;
}

void flip(Patch *patch, bool vertically)
{
    if (patch == nullptr)
        return;

    if (vertically)
    {
        reverse(patch);
    }
    else
    {
        for (Line *cur = patch->head; cur; cur = cur->next)
        {
            string old = cur->data;
            cur->data.clear();
            while (!old.empty())
            {
                cur->data.push_back(old.back());
                old.pop_back();
            }
        }
    }
}

void print(const Patch *patch)
{
    Line *current = patch->head;
    int width = current ? current->data.size() : 0;

    auto hline = [width] {
        cout << '+';
        for (int i = 0; i < width; ++i)
        {
            cout << '-';
        }
        cout << '+' << endl;
    };

    hline();
    while (current != nullptr)
    {
        cout << '|' << current->data << '|' << endl;
        current = current->next;
    }
    hline();
    cout << "@(" << patch->x << ", " << patch->y << ")" << endl;
}

void initialize(Canvas *canvas)
{
    canvas->height = 0;
    canvas->width = 0;
    canvas->top = nullptr;
    canvas->bottom = nullptr;
    canvas->selected = nullptr;
}

Canvas *read_canvas(ifstream &ifs)
{
    Canvas *canvas = new Canvas;
    initialize(canvas);

    ifs >> canvas->height >> canvas->width;

    for (Patch *patch = read_patch(ifs); patch; patch = read_patch(ifs))
    {
        append_to_bottom(canvas, patch);
    }

    return canvas;
}

string literal(bool b)
{
    return b ? "true" : "false";
}

void print(const Canvas *canvas, bool verbose)
{
    char **buffer = render(canvas);

    if (buffer == nullptr)
    {
        cout << "[null canvas]" << endl;
        return;
    }

    if (verbose)
    {
        for (PatchNode *current = canvas->top; current; current = current->below)
        {
            print(current->patch);
        }
    }

    auto hline = [canvas] {
        cout << "+";
        for (int j = 0; j < canvas->width; ++j)
        {
            cout << "-";
        }
        cout << "+" << endl;
    };

    hline();
    for (int i = 0; i < canvas->height; ++i)
    {
        cout << "|";
        for (int j = 0; j < canvas->width; ++j)
        {
            cout << buffer[i][j];
        }
        cout << "|" << endl;
    }
    hline();

    for (int i = 0; i < canvas->height; ++i)
    {
        delete[] buffer[i];
    }
    delete[] buffer;
}

void select_top(Canvas *canvas)
{
    if (canvas)
    {
        canvas->selected = canvas->top;
    }
}

void select_bottom(Canvas *canvas)
{
    if (canvas)
    {
        canvas->selected = canvas->bottom;
    }
}

// select the current node to above one if there is one
void select_above(Canvas *canvas)
{
    if (canvas && canvas->selected && canvas->selected->above)
    {
        canvas->selected = canvas->selected->above;
    }
}

// select the current node to below one if there is one
void select_below(Canvas *canvas)
{
    if (canvas && canvas->selected && canvas->selected->below)
    {
        canvas->selected = canvas->selected->below;
    }
}

void deselect(Canvas *canvas)
{
    if (canvas)
    {
        canvas->selected = nullptr;
    }
}

void shift(Canvas *canvas, int dx, int dy)
{
    if (canvas && canvas->selected && canvas->selected->patch)
    {
        canvas->selected->patch->x += dx;
        canvas->selected->patch->y += dy;
    }
}
