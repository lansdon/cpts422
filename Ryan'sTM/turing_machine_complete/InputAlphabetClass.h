#ifndef InputAlphabetClass_h
#define InputAlphabetClass_h

#include <iostream>
#include <vector>
using namespace std;

class Input_Alphabet
{
private:
	vector<char> Alphabet;
public:
	//Input_Alphabet();
	bool Is_Element(char) const;
	void Load(vector<char> alphabet);
	int Size() const;
	void View() const;
};
#endif