#ifndef TransitionFunctionClass_H
#define TransitionFunctionClass_H

#include <string>
#include <iostream>
#include <vector>
#include "TransitionClass.h"
#include "Direction.h"
using namespace std;

class Transition_Function
{
private:
	vector<Transition> Transitions;
public:
	//Transition_Function();
	string Destination_State(int) const;
	bool Is_Defined_Transition(string, 
		char, 
		string &, 
		char &, 
		Direction &) const;
	bool Is_Source_State(string) const;
	void Load(vector<Transition> transitions);
	char Read_Character(int) const;
	int Size() const;
	string Source_State(int) const;
	void View() const;
	char Write_Character(int) const;
};

#endif