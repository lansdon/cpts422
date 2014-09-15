#ifndef CONFIGURATION_H
#define CONFIGURATION_H
#include <iostream>
#include <string>

using namespace std;

class Configuration
{
private:
	string File_Name;
	bool Help;
	int Maximum_Cells, Transitions_To_Perform;
public:
	bool get_help() const;
	void load(string file_name);
	Configuration();
	int Get_Transitions() const;
	int Get_Truncation() const;
	string help();
	string set_transitions(int value);
	string set_truncation(int value);
	string file_name() const;
};

#endif