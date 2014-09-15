#ifndef StatesClass_h
#define StatesClass_h

#include <string>
#include <vector>
#include <iostream>
using namespace std;

class States
{
private:
	vector<string> Names;
public:
	//States();
	bool Is_Element(string) const;
	void Load(vector<string> names);
	void View() const;
	int size() const;
};
#endif