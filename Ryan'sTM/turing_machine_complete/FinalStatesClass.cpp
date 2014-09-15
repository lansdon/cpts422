#include "FinalStatesClass.h"
#include <vector>
#include <string>
#include <iostream>

//Final_States::Final_States() :Names(){}

void Final_States::Load(vector<string> names)
{
	Names = names;
}

string Final_States::Element(int value) const
{
	return Names[value];
}

bool Final_States::Is_Element(string value) const
{
	bool valid = false;
	for (int element = 0; element < Names.size();element++)
		if (Names[element] == value)
			valid = true;
	return valid;
}

int Final_States::Size() const
{
	return Names.size();
}

void Final_States::View() const
{
	cout << "Final states: {";
	for (int element = 0; element < Names.size(); element++)
	{
		cout << Names[element];
		if (element != Names.size() - 1)
			cout << ", ";
	}
	cout << "}\n\n";
}
