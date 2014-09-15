#ifndef COMMANDCLASS_H
#define COMMANDCLASS_H
#include "InputStringClass.h"
#include "TuringMachineClass.h"
#include "ConfigurationClass.h"
#include "DisplayClass.h"
#include <string>
#include <iostream>

using namespace std;
class CommandClass : public DisplayClass
{
private:
	void commandLoop();
	Turing_Machine TM;
	Input_String input_string;
	Configuration config;
	void exit();
	bool input_string_altered;
	void delete_input_string();
	char get_user_input();
	void help();
	void insert_input_string();
	void list();
	void quit_turing_machine();
	void run_command(char user_input);
	void run_turnig_machine();
	void set_transitions();
	void show();
	void truncate_description();
	void view();
	void _default();

public:
	CommandClass();
	CommandClass(string File_Name);
       
   
    
};

#endif // COMMANDCLASS_H
