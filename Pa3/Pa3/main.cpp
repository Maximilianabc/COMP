#include <iostream>
#include <fstream>
#include "utils.h"

using namespace std;

void task1(const string &path)
{
    cout << "TASK 1" << endl;

    ifstream ifs;
    try
    {
        open_file(ifs, path);
    }
    catch (const runtime_error &e)
    {
        cerr << e.what() << endl;
        return;
    }

    Patch *patch = read_patch(ifs);
    cout << "[Example case: show patch.]" << endl;
    {
        print(patch);
    }

    cout << "[Example: flip horizontally.]" << endl;
    {
        flip(patch, false);
        print(patch);
    }

    cout << "[Example: flip verticallyly.]" << endl;
    {
        flip(patch, true);
        print(patch);
    }

    clear(patch);
}

void task2(const string &path)
{
    cout << "TASK 2" << endl;

    ifstream ifs;
    try
    {
        open_file(ifs, path);
    }
    catch (const runtime_error &e)
    {
        cerr << e.what() << endl;
        return;
    }

    Canvas *canvas = read_canvas(ifs);

    cout << "[Example: print verbosely.]" << endl;
    {
        print(canvas, true);
    }

    cout << "[Example: select top, move down then print.]" << endl;
    {
        select_top(canvas);
        shift(canvas, 1, 0);
        print(canvas, false);
    }
    cout << "[Example: move right then print.]" << endl;
    {
        shift(canvas, 0, 1);
        print(canvas, false);
    }
    cout << "[Example: deselect and move left (failed) then print.]" << endl;
    {
        deselect(canvas);
        shift(canvas, 0, -1);
        print(canvas, false);
    }

    clear(canvas);
}

void task3(const string &path)
{
    cout << "TASK 3" << endl;

    ifstream ifs;
    try
    {
        open_file(ifs, path);
    }
    catch (const runtime_error &e)
    {
        cerr << e.what() << endl;
        return;
    }

    Canvas *canvas = read_canvas(ifs);

    cout << "[Example: print verbosely.]" << endl;
    {
        print(canvas, true);
    }

    cout << "[Example: send the top to the bottom.]" << endl;
    {
        select_top(canvas);
        print(canvas);
        while (canvas->selected != canvas->bottom)
        {
            send_selected_below(canvas);
            print(canvas);
        }
        // print with highlight the selection
        cout << "deselect (remove highlight):" << endl;
        deselect(canvas);
        print(canvas);
    }

    cout << "[Example: bring the bottom to the top.]" << endl;
    {
        select_bottom(canvas);
        print(canvas);
        while (canvas->selected != canvas->top)
        {
            bring_selected_above(canvas);
            print(canvas);
        }
        // print with highlight the selection
        cout << "deselect (remove highlight):" << endl;
        deselect(canvas);
        print(canvas);
    }

    // update: clear for task 3 is added.
    clear(canvas);
}

void task4(const string &path)
{
    cout << "TASK 4" << endl;

    ifstream ifs;
    try
    {
        open_file(ifs, path);
    }
    catch (const runtime_error &e)
    {
        cerr << e.what() << endl;
        return;
    }

    Canvas *canvas = read_canvas(ifs);

    cout << "[Example: selections.]" << endl;
    {
        auto select_and_print = [canvas](int x, int y) {
            cout << "select (" << x << ", " << y << ") and get:" << endl;
            select_at(canvas, x, y);
            print(canvas);
        };

        print(canvas);
        select_and_print(1, 1);
        select_and_print(3, 1);
        select_and_print(3, 2);
        select_and_print(canvas->height - 1, canvas->width - 1);
        select_and_print(canvas->height, canvas->width);
    }

    cout << "[Example: erase.]" << endl;
    {
        auto erase_and_print = [canvas](int x, int y) {
            erase_pixel_at(canvas, x, y);
            cout << "erase (" << x << ", " << y << ")" << endl;
        };

        cout << "Before erasing:" << endl;
        print(canvas, true);
        cout << "Start erasing:" << endl;
        erase_and_print(3, 1);
        erase_and_print(3, 2);
        erase_and_print(4, 3);
        erase_and_print(canvas->height - 1, canvas->width - 1);
        erase_and_print(canvas->height - 1, canvas->width + 1);
        cout << "After erasing, get:" << endl;
        print(canvas, true);
    }

    clear(canvas);
}

int main()
{
    // uncomment the conrespoding line when you finished one task
    // task1("example/input/task1.txt");
    // task2("example/input/task2.txt");
    // task3("example/input/task3.txt");
    task4("example/input/task4.txt");
    return 0;
}
