#ifndef ParseClass_h
#define ParseClass_h

#include "TransitionFunctionClass.h"
#include <iostream>
#include <string>
#include <vector>
#include <fstream>
#include <algorithm>
using namespace std;

class Parse
{
private:
	int Description_Parse(ifstream &definition, string &, bool &valid);
	int States_Parse(ifstream &definition, vector<string> &, bool &valid);
	int Input_Alphabet_Parse(ifstream &definition, vector<char> &, bool &valid);
	int Tape_Alphabet_Parse(ifstream &definition, vector<char> &, bool &valid);
	int Transitions_Parse(ifstream &definition, vector<Transition> &, bool &valid);
	int Initial_State_Parse(ifstream &definition, string &, bool &valid);
	int Blank_Character_Parse(ifstream &definition, char &, bool &valid);
	void Final_States_Parse(ifstream &definition, vector<string> &, bool &valid);
	string To_Capital(string &);
	char To_Capital(char &);
public:
	void Input_String_Parse(fstream &definition, vector<string> &);
	bool Turing_Machine_Parse(ifstream &definition, string &description,
		vector<string> &states, vector<char> &input_alphabet,
		vector<char> &tape_alphabet, vector<Transition> &transitions,
		string &initial_state, char &blank_character,
		vector<string> &final_states, bool& valid);
};

#endif //ParseClass_h
