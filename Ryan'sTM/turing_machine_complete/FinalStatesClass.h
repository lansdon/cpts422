#ifndef FinalStatesClass_h
#define FinalStatesClass_h

#include <vector>
#include <string>
#include <iostream>
using namespace std;

class Final_States
{
private:
	vector<string> Names;
public:
	//Final_States();
	string Element(int) const;
	bool Is_Element(string) const;
	void Load(vector<string> names);
	int Size() const;
	void View() const;
};

#endif