#include "TuringMachineClass.h"
#include "ParseClass.h"
#include <string>
#include <vector>
#include "Crash.h"


void Turing_Machine::load(string name) 
{
  description="";
	name.append(".def");
	def.open(name.c_str());
	Turing_Machine_Parse(def, description, state_names, inputAlpha, tapeAlpha, trans, Initial_State, blankChar, final_state_names, Valid);
	def.close();
	states.Load(state_names);
	
	input_alphabet.Load(inputAlpha);
	tape_alphabet.Load(tapeAlpha);
	for (int character = 0; character < input_alphabet.Size(); character++)
	{
		if (!tape_alphabet.Is_Element(inputAlpha[character]))
		{
			cout << "Input alphabet not a subset of tape alphabet.\n\n";
			Valid = false;
			break;
		}
	}
	transition_function.Load(trans);
	if (!tape_alphabet.Is_Element(blankChar))
	{
		cout << "Blank character not a member of tape alphabet.\n\n";
		Valid = false;
	}
	tape.Load(blankChar);
	for (int element = 0; element < final_state_names.size(); element++)
	{
		if (!states.Is_Element(final_state_names[element]))
		{
			cout << "Final states not a subset of states.\n\n";
			Valid = false;
			break;
		}
	}
	final_states.Load(final_state_names);
	Accepted = Rejected = Operating = Used = false;
	
}

bool Turing_Machine::Is_Valid_Input_String(string value) const
{
	bool valid = true;
	for (int string_element = 0; string_element < value.size(); string_element++)
	{
		if (!input_alphabet.Is_Element(value[string_element]) && value !="\\" && value == "")
			valid = false;
	}
	return valid;
}

bool Turing_Machine::Is_operating() const
{
	return Operating;
}

bool Turing_Machine::Is_Used() const
{
	return Used;
}

bool Turing_Machine::Is_Accepted_Input_String() const
{
	return Accepted;
}

bool Turing_Machine::Is_Rejected_Input_String() const
{
	return Rejected;
}

bool Turing_Machine::Is_Valid_Definition() const
{
	return Valid;
}

void Turing_Machine::View_Definition() const
{
	cout << description << "\n\n";
	states.View();
	input_alphabet.View();
	tape_alphabet.View();
	transition_function.View();
	cout <<"Initial state: "<< Initial_State << "\n\n";
	tape.view();
	final_states.View();
}

void Turing_Machine::View_Instantaneous_Description(int Number_of_cells) const
{
	cout << number_of_transitions << ". " << tape.left(Number_of_cells) << "[";
	cout << Current_State << "]" << tape.right(Number_of_cells) << endl;
}

void Turing_Machine::Initialize(string input_string)
{
	Current_State = Initial_State;
	number_of_transitions = 0;
	Accepted = Rejected = false;
	Original_Input_String = input_string;
	tape.Initialize(input_string);
	Used = true;
	Operating = true;
}

void Turing_Machine::Perform_Transitions(int max_number_of_transitions)
{
	string dest_state;
	char write_char;
	Direction move_dir;
	for (int transitions = 0; transitions < max_number_of_transitions; transitions++)
	{
		if (transition_function.Is_Defined_Transition(Current_State,
			tape.Current_Character(), dest_state, write_char, move_dir))
		{
			Current_State = dest_state;

			try
			{
				tape.Update(write_char, move_dir);
				number_of_transitions++;
			}
			catch (exception ex)
			{
				ex.what();
			}
			if (final_states.Is_Element(Current_State))
			{
				Accepted = true;
				break;
			}
		}
		else
		{
			Rejected = true;
		}
	}
}
string Turing_Machine::input_string() const
{
	return Original_Input_String;
}
int Turing_Machine::total_number_of_transitions() const
{
	return number_of_transitions;
}
void Turing_Machine::Terminate_operation()
{
	Operating = false;
}
