#include "ConfigurationClass.h"
#include <sstream>
#include <iostream>
#include <string>

Configuration::Configuration() : File_Name(""),Help(false),Maximum_Cells(32),Transitions_To_Perform(1){}

bool Configuration::get_help() const
{
	return Help;
}
void Configuration::load(string file_name)
{
	File_Name = file_name;
}
string Configuration::file_name() const
{
	return File_Name;
}
int Configuration::Get_Transitions() const 
{
	return Transitions_To_Perform;
}

int Configuration::Get_Truncation() const
{
	return Maximum_Cells;
}

string Configuration::help()
{
	string temp = "\nHelp ";
	if (Help)
	{
		Help = false;
		temp.append("disabled.\n\n");
	}
	else
	{
		Help = true;
		temp.append("enabled.\n\n");
	}
	return temp;
}

string Configuration::set_transitions(int value)
{
  string sTemp;
  ostringstream ss;
  ss << value;
string temp = "Transitions to perform set to ";
	Transitions_To_Perform = value;
	temp.append(ss.str());
	temp.append(".");
	return temp;
}

string Configuration::set_truncation(int value)
{
  ostringstream ss;
  ss << value;
	string temp = "Maximum cell to display set to ";
	Maximum_Cells = value;
	temp.append(ss.str());
	temp.append(".");
	return temp;
}
