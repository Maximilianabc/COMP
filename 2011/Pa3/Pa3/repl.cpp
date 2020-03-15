#include <iostream>
#include <fstream>
#include "patch.h"
#include "utils.h"
using namespace std;

void clear_screen()
{
    cout << string(100, '\n');
}

void repl()
{
    string op, msg;
    Canvas *canvas = nullptr;

    string help = R"HELP(
l: load
d: deselect
^: select above
v: select below
^!: select top
v!: select bottom
^^: bring above
vv: send below
m^: shift up
mv: shift down
m<: shift left
m>: shift right
s: select
f: flip selected horizontally
F: flip selected vertically
e: erase
q: quit)HELP";

    while (true)
    {
        clear_screen();
        print(canvas, true);
        cout << "Message: " << msg << endl;
        cout << "Please specify the operation (h for help): " << endl;
        if (!getline(cin, op))
        {
            break;
        }

        if (op == "\n")
        {
            continue;
        }
        else if (op == "l")
        {
            string path;
            cout << "Please specify a path (default: example/input/task4.txt): ";
            getline(cin, path);
            if (path.size() == 0)
            {
                path = "example/input/task4.txt";
            }

            ifstream ifs;
            try
            {
                open_file(ifs, path);
            }
            catch (const runtime_error &e)
            {
                msg = e.what();
                continue;
            }

            // deleting without checking null is bad, thanks to the considerate implementation of clear, this is not a problem here.
            clear(canvas);
            canvas = read_canvas(ifs);
        }
        else if (op == "d")
        {
            deselect(canvas);
        }
        else if (op == "^")
        {
            select_above(canvas);
        }
        else if (op == "v")
        {
            select_below(canvas);
        }
        else if (op == "^^")
        {
            bring_selected_above(canvas);
        }
        else if (op == "vv")
        {
            send_selected_below(canvas);
        }
        else if (op == "^!")
        {
            select_top(canvas);
        }
        else if (op == "v!")
        {
            select_bottom(canvas);
        }
        else if (op == "mv")
        {
            shift(canvas, 1, 0);
        }
        else if (op == "m>")
        {
            shift(canvas, 0, 1);
        }
        else if (op == "m<")
        {
            shift(canvas, 0, -1);
        }
        else if (op == "m^")
        {
            shift(canvas, -1, 0);
        }
        else if (op == "s")
        {
            int x, y;
            cout << "Please specify a coordinate (x, y) to select: ";
            cin >> x >> y;
            select_at(canvas, x, y);
        }
        else if (op == "f")
        {
            if (canvas && canvas->selected && canvas->selected->patch)
            {
                flip(canvas->selected->patch, false);
            }
        }
        else if (op == "F")
        {
            if (canvas && canvas->selected && canvas->selected->patch)
            {
                flip(canvas->selected->patch, true);
            }
        }
        else if (op == "e")
        {
            int x, y;
            cout << "Please specify a coordinate (x, y) to erase: ";
            cin >> x >> y;
            erase_pixel_at(canvas, x, y);
        }
        else if (op == "h")
        {
            clear_screen();
            string tmp;
            cout << help << endl;
            getline(cin, tmp);
        }
        else if (op == "q")
        {
            break;
        }
        else if (op == "")
        {
            msg = "Please input a command.";
        }
        else
        {
            msg = "Error: unknown command '" + op + "'";
        }
    }
    // deleting without checking null is bad, thanks to the considerate implementation of clear, this is not a problem here.
    clear(canvas);
    cout << "Have a nice day :)." << endl;
}

int main()
{
    repl();
    return 0;
}
