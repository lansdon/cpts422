#include "TapeAlphabetClass.h"
#include <vector>
#include <iostream>

void Tape_Alphabet::Load(vector<char> alphabet)
{
	Alphabet = alphabet;
}

bool Tape_Alphabet::Is_Element(char value) const
{
	bool valid = false;
	for (int element = 0; element < Alphabet.size(); element++)
		if (Alphabet[element] == value)
			valid = true;
		return valid;
}

int Tape_Alphabet::Size() const
{
	return Alphabet.size();
}

void Tape_Alphabet::View() const
{
	cout << "Tape alphabet: ";
	for (int element = 0; element < Alphabet.size(); element++)
	{
		cout << Alphabet[element] << " ";
	}
	cout << "\n\n";
}