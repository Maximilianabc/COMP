/*
 * COMP2011 2019F
 * Lab 9: Game development using dynamic memory allocation
 * Driver program: main.cpp
 */

#include <iostream>
#include <ctime>
#include <cstdlib>
#include <cstring>
#include "lab9.h"

using namespace std;

int main()
{
    GameBoard game;
    char cmd, reset_cmd;
    int num;
    bool status = true;

    srand(time(0));

    do
    {
        cout << "Please enter the number of enemeies: ";
        cin >> num;
    } while (num > MAX_NUM_MONSTERS);

    cout << "Let's enter the mysterious zone!" << endl;

    // Initialize the game
    init_game(&game, num);

    do
    {
        draw_game(&game);
        cout << "Enter the cmd ('u' for up, 'd' for down, 'l' for left, 'r' for right, 'q' for quit):";
        cin >> cmd;
        // move the hero in one of the 4 directions
        switch (cmd)
        {
        case 'u': // move up 
            status = move_hero(&game, -1, 0);
            break;
        case 'd': // move down
            status = move_hero(&game, 1, 0);
            break;
        case 'l': // move left
            status = move_hero(&game, 0, -1);
            break;
        case 'r': // move right
            status = move_hero(&game, 0, 1);
        }
        if (game.num_monsters == 0)
        {
            cout << "You have eaten all monsters!" << endl;
            status = false;
        }
        if (!status)
        {
            cout << "Start another game? ('y'/'n'):";
            cin >> reset_cmd;
            if (reset_cmd == 'y')
            {
                delete_all_monsters(&game);
                init_game(&game, num);
                status = true;
            }
        }
    } while ((cmd != 'q') && (status == true));

    // deallocate all dynamic monsters   
    delete_all_monsters(&game);
    return 0;
}

