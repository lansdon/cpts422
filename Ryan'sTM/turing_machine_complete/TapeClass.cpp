#include "TapeClass.h"
#include "Direction.h"
#include <string>
#include <fstream>
#include <iostream>
#include <algorithm>
#include "Crash.h"

Tape::Tape() : Blank(' '), Current_Cell(0), Cells(""){};

char Tape::Current_Character() const
{
	return Cells[Current_Cell];
}

void Tape::Initialize(string input_string)
{
	if (input_string == "\\")
		input_string = "";
	Cells = input_string + Blank;
	Current_Cell = 0;
}

bool Tape::Is_FirstCell() const
{
	return (Current_Cell == 0);
}

char Tape::Blank_Character() const
{
	return Blank;
}

void Tape::Load(char blank)
{
	Blank = blank;
}

void Tape::Update(char write_character, Direction move_direction)
{
	Cells[Current_Cell] = write_character;
	if ((move_direction == 'L') && (Current_Cell == 0))
		throw Crash("left move from first cell");
	if ((move_direction == 'R') && (Current_Cell == Cells.length() - 1))
		Cells += Blank;
	Cells[Current_Cell] = write_character;
	if (move_direction == 'L')
		Current_Cell--;
	else
		Current_Cell++;
}

void Tape::view() const
{
	cout << "B = " << Blank << "\n\n";
}

string Tape::left(int maximum_number_of_cells) const
{
	int firstCell= max(0,Current_Cell-maximum_number_of_cells);
	string value=Cells.substr(firstCell,Current_Cell-firstCell);
	if (value.length() < Current_Cell)
		value.insert(0, "<");
	return value;
}

string Tape::right(int maximum_number_of_cells) const
{
	int endCell = Cells.length() - 1;
	while ((endCell >= Current_Cell) && (Cells[endCell]) == Blank)
		endCell--;
	int lastCell = min(endCell, Current_Cell + maximum_number_of_cells-1);
	string value = Cells.substr(Current_Cell, lastCell-Current_Cell+1);
	if (value.length() < endCell-Current_Cell+1)
		value.append(">");
	return value;
}
