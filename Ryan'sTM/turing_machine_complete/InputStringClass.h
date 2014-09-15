#ifndef INPUTSTRINGCLASS_H
#define INPUTSTRINGCLASS_H
#include "ParseClass.h"
#include <vector>
#include <string>
#include <iostream>
#include <fstream>

using namespace std;

class Input_String : public Parse
{
private:
	fstream input_File;
	vector<string> input_string_list;
public:
	void delete_string(int value);
	int size() const;
	void insert_string(string value);
	void view() const;
	void load(string name);
	string retrieve_input_string(int element) const;
	bool save_list(string file_name);
};
#endif