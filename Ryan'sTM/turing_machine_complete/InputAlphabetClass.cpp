#include "InputAlphabetClass.h"
#include <iostream>
#include <vector>

void Input_Alphabet::Load(vector<char> alphabet)
{
	Alphabet = alphabet;
}

bool Input_Alphabet::Is_Element(char value) const
{
	bool valid = false;
	for (int element = 0; element < Alphabet.size(); element++)
	{
		if (Alphabet[element] == value)
			valid = true;
	}
	return valid;
}
int Input_Alphabet::Size() const
{
	return Alphabet.size();
}

void Input_Alphabet::View() const
{
	cout << "Input alphabet: ";
	for (int element = 0; element < Alphabet.size(); element++)
	{
		cout << Alphabet[element] << " ";
	}
	cout << "\n\n";
}