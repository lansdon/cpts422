#include "InputStringClass.h"

#include <vector>
#include <string>
#include <iostream>

int Input_String::size() const
{
	return input_string_list.size();
}
void Input_String::insert_string(string value)
{
	
	bool add = true;
	
	if (!input_string_list.empty())
	for (int element = 0; element < input_string_list.size(); element++)
	{
		if (input_string_list[element] == value)
		{
			add = false;
			break;
		}
	}
	if (add)
		input_string_list.push_back(value);
	else
		cout << "Input string already exists in list.\n";
}

void Input_String::view() const
{
	cout << endl;
	for (int element = 0; element < input_string_list.size(); element++)
	{
		cout << element + 1 << ". "<<input_string_list[element] << endl;
	}
	cout << endl;
}
string Input_String::retrieve_input_string(int element) const
{
	return input_string_list[element];
}

void Input_String::load(string name)
{
	name.append(".str");
	input_File.open(name.c_str(),ios::in);
	if (input_File.is_open())
		Input_String_Parse(input_File, input_string_list);
	input_File.close();
}
bool Input_String::save_list(string file_name)
{
	bool saved = false;
	file_name.append(".str");
	input_File.open(file_name.c_str(), ios::trunc | ios::out);
	if (input_File.is_open())
	{
		for (int elements = 0; elements < input_string_list.size(); elements++)
		{
			input_File << input_string_list[elements];
				if(elements<input_string_list.size()-1)
					input_File<< endl;
		}
		saved = true;
	}
	input_File.close();
	if(input_string_list.size() == 0)
	  remove(file_name.c_str());
	return saved;
}

void Input_String::delete_string(int value)
{
	input_string_list.erase(input_string_list.begin()+(value-1));
}
