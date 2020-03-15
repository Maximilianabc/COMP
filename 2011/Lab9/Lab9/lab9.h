/*
 * COMP2011 2019F
 * Lab 9: Game development using dynamic memory allocation
 *
 * Header file: lab9.h
 */

#include <iostream>
#include <ctime>
#include <cstdlib>
#include <cstring>

 // Maximum power of monsters (i.e. power can be 0 .. 9)
const int MAX_POWER = 10;
// Maximum number of monsters
const int MAX_NUM_MONSTERS = 10;
// The size of the GameBoard (2D game)
const int MAZE_SIZE = 5;

enum Type { HERO, MONSTER };
const char HERO_SYMBOL = '@';

// Struct definition
struct Location {
    int row;
    int col;
};

// Struct definition for the character (i.e. hero or monster)
struct Character {
    Type type;  // HERO or MONSTER
    int power;
    int num_collected; // HERO: number of monsters beaten; MONSTER: 0
    Character* bag[MAX_NUM_MONSTERS]; // pointers pointing to the monsters beaten
    Location location; // location (row & column) of the hero/monster
};

// Struct definition for the game (game game) 
struct GameBoard {
    Character hero; // hero
    int num_monsters; // number of monsters alived on the game game
    Character* board[MAZE_SIZE][MAZE_SIZE]; // 2D game pointing to the Character objects (hero or monsters),
                                            // otherwise nullptr 
};

// To be completed

// create and return a monster using dynamic memory allocation 
Character* create_monster();
// move the hero relatively by (move_row, move_col)
bool move_hero(GameBoard* game, int move_row, int move_col);
// delete all the monsters you have created using "delete" operation
void delete_all_monsters(GameBoard* game);

// Given

// initialize the hero (a Character object)
void init_hero(Character* hero, int p, Location loc);
// initialize the GameBoard object, game, with # of monsters equal to num
void init_game(GameBoard* game, int num);
// draw the board showing where the hero and monsters are, and the current status
void draw_game(const GameBoard* game);

