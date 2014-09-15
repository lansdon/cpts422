#ifndef TuringMachineClass_h
#define TuringMachineClass_h

#include <string>
#include <vector>
#include "ParseClass.h"
#include "TapeClass.h"
#include "InputAlphabetClass.h"
#include "TransitionFunctionClass.h"
#include "TapeAlphabetClass.h"
#include "StatesClass.h"
#include "FinalStatesClass.h"

using namespace std;

class Turing_Machine : Parse
{
private:
	Tape tape;
	Input_Alphabet input_alphabet;
	Tape_Alphabet tape_alphabet;
	Transition_Function transition_function;
	States states;
	Final_States final_states;
	int number_of_transitions;
	bool Accepted, Rejected, Used, Valid, Operating;
	string Current_State, Initial_State, Original_Input_String, description;
	
	vector<string> state_names, final_state_names;
	vector<char> inputAlpha, tapeAlpha;
	vector<Transition> trans;
	char blankChar;
	ifstream def;
	

public:
	void load(string name);
	void Initialize(string);///
	bool Is_Accepted_Input_String() const;//
	bool Is_operating() const;//
	bool Is_Rejected_Input_String() const;//
	bool Is_Used() const;//
	bool Is_Valid_Definition() const;//
	bool Is_Valid_Input_String(string) const;//
	void Perform_Transitions(int);//
	void Terminate_operation();
	//Turing_Machine();//
	void View_Definition() const;//
	void View_Instantaneous_Description(int) const;//
	string input_string() const;//
	int total_number_of_transitions() const;//

};
#endif