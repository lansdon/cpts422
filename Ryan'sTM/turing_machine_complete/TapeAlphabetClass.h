#ifndef TapeAlphabetClass_h
#define TapeAlphabetClass_h

#include <iostream>
#include <vector>
using namespace std;

class Tape_Alphabet
{
private:
	vector<char> Alphabet;
public:
	//Tape_Alphabet();
	bool Is_Element(char) const;
	void Load(vector<char> alphabet);
	int Size() const;
	void View() const;
};
#endif