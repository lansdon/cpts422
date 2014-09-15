#include "TransitionFunctionClass.h"
#include "TransitionClass.h"
#include "Direction.h"
#include <string>
#include <iostream>
#include <vector>
using namespace std;

string Transition_Function::Destination_State(int index) const
{
	return Transitions[index].Destination_State();
}

bool Transition_Function::Is_Defined_Transition(string Source_State, 
	char Read_Character, 
	string& Destination_State, 
	char& Write_Character, 
	Direction& Move_Direction) const
{
	for (int index = 0; index < Size(); index++)
	{
		if (Transitions[index].Source_State() == Source_State && Transitions[index].Read_Character() == Read_Character)
		{
			Destination_State = Transitions[index].Destination_State();
			Write_Character = Transitions[index].Write_Character();
			Move_Direction = Transitions[index].Move_Direction();
			return true;
		}
	}
	return false;
}

bool Transition_Function::Is_Source_State(string State) const
{
	return false;
}

void Transition_Function::Load(vector<Transition> transitions)
{
	Transitions = transitions;
}

char Transition_Function::Read_Character(int index) const
{
	return Transitions[index].Read_Character();
}

int Transition_Function::Size() const
{
	return Transitions.size();
}

string Transition_Function::Source_State(int index) const
{
	return Transitions[index].Source_State();
}

void Transition_Function::View() const
{
	
	for (int element = 0; element < Transitions.size(); element++)
	{
		cout<<"Delta"<<"(" << Transitions[element].Source_State()<<", " <<Transitions[element].Read_Character()<<")";
		cout << " = (" << Transitions[element].Destination_State() << ", " << Transitions[element].Write_Character() << ", ";
		cout << Transitions[element].Move_Direction() << ")\n";
	}
	cout << endl;
}

char Transition_Function::Write_Character(int index) const
{
	return Transitions[index].Write_Character();
}
