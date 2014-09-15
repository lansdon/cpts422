#include "TransitionClass.h"
#include "Direction.h"
#include <string>
using namespace std;

Transition::Transition(string Source_state, 
	char Read_character, 
	string Destination_state, 
	char Write_character, 
	Direction Move_direction):
			Source(Source_state), 
			Read(Read_character), 
			Destination(Destination_state), 
			Write(Write_character), 
			Move(Move_direction){}

string Transition::Source_State() const
{
	return Source;
}

char Transition::Read_Character() const
{
	return Read;
}

string Transition::Destination_State() const
{
	return Destination;
}

char Transition::Write_Character() const
{
	return Write;
}

Direction Transition::Move_Direction() const
{
	return Move;
}