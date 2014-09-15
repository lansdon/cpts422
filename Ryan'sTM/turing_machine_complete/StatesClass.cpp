#include "StatesClass.h"
#include <vector>
#include <iostream>
#include <string>

void States::Load(vector<string> names)
{
	Names = names;
}

bool States::Is_Element(string value) const
{
	bool valid = false;
	for (int element = 0; element < Names.size();element++)
	if (Names[element] == value)
		valid = true;
	return valid;
}

void States::View() const
{
	cout << "States: {";
	for (int element = 0; element < Names.size(); element++)
	{
		cout << Names[element];
		if (element != Names.size() - 1)
			cout << ", ";
	}
	cout <<"}\n\n";
}
int States::size() const
{
	return Names.size();
}
