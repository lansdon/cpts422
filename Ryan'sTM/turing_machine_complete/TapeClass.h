#ifndef TapeClass_h
#define TapeClass_h

#include <string>
#include <fstream>
#include "Direction.h"
using namespace std;

class Tape
{
private:
	char Blank;
	string Cells;
	int Current_Cell;
public:
	Tape();
	char Blank_Character() const;
	char Current_Character() const;
	void Initialize(string input_string);
	bool Is_FirstCell() const;
	void Load(char blank);
	void Update(char write_character, Direction move_direction);
	void view() const;
	string left(int maximum_number_of_cells) const;
	string right(int maximum_number_of_cells) const;
};
#endif