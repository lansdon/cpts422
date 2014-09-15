#include "CommandClass.h"
#include "DisplayClass.h"
#include "InputStringClass.h"
#include "TuringMachineClass.h"
#include "ConfigurationClass.h"
#include <string>
#include <iostream>

using namespace std;

CommandClass::CommandClass(string File_Name)
{
	input_string_altered = false;
	config.load(File_Name);
	input_string.load(File_Name);
	TM.load(File_Name);
	if (TM.Is_Valid_Definition())
	{
		for (int index = 0; index < input_string.size(); index++)
		{
		  if (!TM.Is_Valid_Input_String(input_string.retrieve_input_string(index)) || input_string.retrieve_input_string(index) == "" )
			{
				input_string.delete_string(index+1);
			}
			
		}
		cout<<"Turing Machine loaded successfully\n\n";
		commandLoop();

	}
}

void CommandClass::commandLoop()
{
	
char input=0;
    while(input != -1)
    {

		if (config.get_help())
			h();
		p();
		 input = get_user_input();


        switch(toupper(input))
        {
            case 'I':
				if (config.get_help())
					i();
				insert_input_string();
            break;
            case 'X':
                exit();
                input = -1;
            break;
            case 'D':
				if (config.get_help())
					d();
                delete_input_string();
            break;
            case 'L':
                list();
            break;
            case 'H':
                help();
            break;
            case 'Q':
				if (config.get_help())
					q();
                quit_turing_machine();
            break;
            case 'R':
				run_turnig_machine();
            break;
            case 'E':
				if (config.get_help())
					e();
                set_transitions();
            break;
            case 'W':
                show();
            break;
            case 'T':
				if (config.get_help())
					t();
                truncate_description();
            break;
            case 'V':
				if (config.get_help())
					v();
                view();
            break;
	case NULL:
	  break;
            default:
                _default();
            break;
        }
    }

}

void CommandClass::exit()
{
	if (input_string_altered)
	{
		if (input_string.save_list(config.file_name()))
			cout << "\nInput string list saved.\n";
		else
			cout << "\nError writing to file.\n";
	}
		
}

void CommandClass::insert_input_string()
{
	string value;
	cout << "Enter input string: ";
	getline(cin, value);
	if (!value.empty())
	if (TM.Is_Valid_Input_String(value) || value == "\\")
	{
		cout << "String " << value << " entered.\n\n";
		input_string.insert_string(value);
		input_string_altered = true;
	}
	else
		cout << "Not a valid input string.\n";
	cout << endl;
}

void CommandClass::delete_input_string()
{
	string temp;
	cout << "Enter the string number to delete: ";
	getline(cin, temp);
	if (!temp.empty())
	if (isdigit(temp[0]))
	{
		int temp_int = atoi(temp.c_str());
		if (temp_int <= input_string.size())
		{
			cout << "The string \"" << input_string.retrieve_input_string(temp_int-1) << "\" has been deleted.\n\n";
			input_string.delete_string(temp_int);
			input_string_altered = true; 
		}
		else
			cout << "That number is not in the list.\n\n";
	}
	else
		cout << "Invalid number, please enter the number associated with the string.\n\n";
	cout << endl;
}

char CommandClass::get_user_input()
{
	string temp;
    getline(cin,temp);
    return temp[0];
}

void CommandClass::_default()
{
	cout << "\nInvalid command." << endl;
}

void CommandClass::help()
{
	cout<<config.help();
}

void CommandClass::show()
{
	cout << "\nCourse: CPTS 322 spring 2014\nInstructor name: Neil Corrigan\nAuthor name: Ryan Wilson\nVersion 1.01\n\n";
	cout << "Max number of transitions set to: " << config.Get_Transitions();
	cout << "\nHelp is ";
	if (config.get_help())
		cout << "enabled.\n";
	else
		cout << "disabled.\n";
	cout << "Showing " << config.Get_Truncation() << " characters to the left and right of instantanious discription.\n\n";
	cout << "Turing Machine \"" << config.file_name() << "\"";
	if (TM.Is_Used())
	{
		if (TM.Is_operating())
		{
			cout << " is running on input string " << TM.input_string();
		}
		else
		{
			cout << " finished operating on " << TM.input_string();
			if (TM.Is_Accepted_Input_String())
				cout << ".\nInput string was accepted.\n";
			if (TM.Is_Rejected_Input_String())
				cout << ".\nInput string was rejected.\n";
			if (!TM.Is_Accepted_Input_String() && !TM.Is_Rejected_Input_String())
				cout << ".\nUser quit. String not accepted or rejected.\n";
		}
		cout << "\nTotal number of transitions : " << TM.total_number_of_transitions() << ".\n" << endl;

	}
	else
		cout << " has never been used.\n\n";
}

void CommandClass::list()
{
	input_string.view();
}

void CommandClass::view()
{
	TM.View_Definition();
}

void CommandClass::truncate_description()
{
	string temp;
	cout << "\nSet max number of characters current[" << config.Get_Truncation() << "]: ";
	getline(cin, temp);
	if (!temp.empty())
		cout << config.set_truncation(atoi(temp.c_str())) << endl;
	cout << endl;
}

void CommandClass::run_turnig_machine()
{
  bool loaded = true;
	
	if (!TM.Is_operating())
	{
	  int tempNumber;
	  if (config.get_help())
			r_NotOperating();
		string temp;
		cout << "Enter the string number to run: ";
		getline(cin, temp);
		
		if (!temp.empty())
		  { tempNumber = atoi(temp.c_str());
		    if(tempNumber <= input_string.size() &&tempNumber > 0)
		      {

			TM.Initialize(input_string.retrieve_input_string((atoi(temp.c_str())-1)));
			cout << "Input string \"" << input_string.retrieve_input_string((atoi(temp.c_str())-1));
			cout << "\" loaded.\n";
		      }
		    else
		      {
			cout<<"That is not a valid number.\n\n";
			loaded = false;
		      }
		}
		
	}
	else
	{
		if (config.get_help())
			r_Operating();
		
		TM.Perform_Transitions(config.Get_Transitions());

	}
	if(loaded)
	  {
	    TM.View_Instantaneous_Description(config.Get_Truncation());
	cout << endl;
	if (TM.Is_Accepted_Input_String())
	{
		cout << "String \"" << TM.input_string() << "\" accepted in " << TM.total_number_of_transitions() << " transitions.\n";
		TM.Terminate_operation();
	}
	if (TM.Is_Rejected_Input_String())
	{
		cout << "String \"" << TM.input_string() << "\" rejected in " << TM.total_number_of_transitions() << " transitions.\n";
		TM.Terminate_operation();
	}
	  }

}
void CommandClass::quit_turing_machine()
{
	if (TM.Is_operating())
		cout << "\nUser quit operation.\n\n";
	else
		cout << "\nTurning Machine not operating on string.\n\n";
	TM.Terminate_operation();
}
void CommandClass::set_transitions()
{
	string temp;
	cout << "\nSet max number of trasitions current[" << config.Get_Transitions() << "]: ";
	getline(cin,temp);
	if (!temp.empty())
		cout << config.set_transitions(atoi(temp.c_str())) << endl;
	cout << endl;
}
