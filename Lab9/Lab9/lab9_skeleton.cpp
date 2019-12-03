/*
 * COMP2011 2019F
 * Lab 9: Game development using dynamic memory allocation
 *
 * Source file: lab9_skeleton.cpp
 */

#include "lab9.h"

using namespace std;


// Task 1
// Create a monster using dynamic memory allocation "new"
// The monster should be an instance of the Character structure
// Randomly set the power of monster and location
// Initialize all the pointers of the "bags" to nullptr
// Return the monster you create
Character* create_monster()
{
    Character* monster = new Character();
    monster->type = MONSTER;
    monster->power = rand() % 10;
    monster->location.col = rand() % 5;
    monster->location.row = rand() % 5;
    monster->num_collected = 0;
    for (int i = 0; i < 10; i++)
    {
        monster->bag[i] = nullptr;
    }
    return monster;
}


// Task 2
// Move the hero relatively by (move_row, move_col) 
// (e.g. if move_row is 0, move_col is -1, the hero is moving towards the left.)
// You should be careful about the border situation when your hero hits the wall
// If hero beats a monster, "collect" it to his "bag" and remove it from the 2D "board"
// Returns false if the hero got beaten by the monster; 
// otherwise true
// (Note: you may also need to check if the hero can or cannot move in the specified direction)
bool move_hero(GameBoard* game, int move_row, int move_col)
{
    game->board[game->hero.location.row][game->hero.location.col] = nullptr;
    game->hero.location.col += move_col;
    game->hero.location.row += move_row;
    game->hero.location.col = game->hero.location.col < 0 ? 0 : game->hero.location.col;
    game->hero.location.row = game->hero.location.row < 0 ? 0 : game->hero.location.row;
    game->hero.location.col = game->hero.location.col > 4 ? 4 : game->hero.location.col;
    game->hero.location.row = game->hero.location.row > 4 ? 4 : game->hero.location.row;

    if (game->board[game->hero.location.row][game->hero.location.col] == nullptr)
    {
        game->board[game->hero.location.row][game->hero.location.col] = &game->hero;
        return true;
    }
    if (game->board[game->hero.location.row][game->hero.location.col]->type == MONSTER)
    {
        if (game->hero.power < game->board[game->hero.location.row][game->hero.location.col]->power)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                if (game->hero.bag[i] == nullptr)
                {
                    game->hero.bag[i] = game->board[game->hero.location.row][game->hero.location.col];
                    break;
                }
            }
            game->hero.power += game->board[game->hero.location.row][game->hero.location.col]->power;
            game->hero.num_collected++;
            game->num_monsters--;
            game->board[game->hero.location.row][game->hero.location.col] = &game->hero;
            return true;
        }
    }
}


// Task 3
// Delete all the monsters you have created using "delete" operation
// which contain both "alive" and "dead" ones
// Note: the hero (i.e. hero) is a static object, no need to "delete".
// Note: remember to reset all pointers to nullptr
void delete_all_monsters(GameBoard* game)
{
    for (int i = 0; i < MAZE_SIZE; i++)
    {
        for (int j = 0; j < MAZE_SIZE; j++)
        {
            if (game->board[i][j] != nullptr && game->board[i][j]->type == MONSTER)
            {
                delete game->board[i][j];
                game->board[i][j] = nullptr;
            }
        }
    }

}

/*** The following functions are give. ***/
// Initialize the hero (hero)
void init_hero(Character* hero, int p, Location loc)
{
    // Set location member of hero to loc, type to HERO, power to p
    hero->location = loc;
    hero->type = HERO;
    hero->power = p;
    // Initialize the bag to nullptrs & num_collected to 0
    hero->num_collected = 0;
    for (int i = 0; i < MAX_NUM_MONSTERS; i++)
        hero->bag[i] = nullptr;
}

// Initialize the 2D Board
void init_game(GameBoard* game, int num)
{
    // Initialize the board by setting all pointers of board to nullptr
    for (int i = 0; i < MAZE_SIZE; i++)
        for (int j = 0; j < MAZE_SIZE; j++)
            game->board[i][j] = nullptr;

    // Initialize the hero member of game
    Location l = { 0, 0 };
    init_hero(&(game->hero), 3, l);
    // Have the pointer at (0, 0) points to the hero member
    game->board[l.row][l.col] = &(game->hero);

    // Set the num_monsters member of game to num, and create the monsters accordingly
    game->num_monsters = num;

    // To garantee all monsters are at different locations on the board
    Character* temp = nullptr;
    for (int i = 0; i < game->num_monsters; i++)
    {
        temp = create_monster();
        while (game->board[temp->location.row][temp->location.col] != nullptr)
        {
            delete temp;
            temp = nullptr;
            temp = create_monster();
        }
        game->board[temp->location.row][temp->location.col] = temp;
    }
}

// Draw the board showing where the hero and monsters are, and the current status
void draw_game(const GameBoard* game)
{
    // Draw the boarder
    for (int i = 0; i <= MAZE_SIZE + 1; i++)
        cout << "=";
    cout << endl;
    for (int i = 0; i < MAZE_SIZE; i++)
    {
        // Draw the boarder
        cout << "|";
        for (int j = 0; j < MAZE_SIZE; j++)
        {
            // Print a space if no Character object is there
            if (game->board[i][j] == nullptr)
                cout << " ";
            else {
                // Print the hero
                if (game->board[i][j]->type == HERO)
                    cout << HERO_SYMBOL;
                else
                    // Print the monster 
                    cout << game->board[i][j]->power;
            }
        }
        // Draw the boarder
        cout << "|" << endl;
    }
    // Draw the boarder
    for (int i = 0; i <= MAZE_SIZE + 1; i++)
        cout << "=";
    cout << endl;

    // Display the status of the hero member
    cout << "Hero's current power: " << game->hero.power << endl;
    cout << "Hero has collected: ";
    for (int i = 0; i < game->hero.num_collected; i++)
        cout << game->hero.bag[i]->power << ", ";
    cout << endl;
}