#ifndef TransitionClass_h
#define TransitionClass_h

#include "Direction.h"
#include <string>
using namespace std;

class Transition
{
private:
	string		Source;
	char		Read;
	string		Destination;
	char		Write;
	Direction	Move;
public:
	Transition(string, char, string, char, Direction);
	string Source_State() const;
	char Read_Character() const;	
	char Write_Character() const;
	string Destination_State() const;
	Direction Move_Direction() const;
};
#endif