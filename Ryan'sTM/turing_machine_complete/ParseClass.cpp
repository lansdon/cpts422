#include "ParseClass.h"


int Parse::Blank_Character_Parse(ifstream &definition, char &blank_character, bool &valid)
{
	string value;
	if ((definition >> value) && (value.length() == 1) &&
		(value[0] != '\\') && (value[0] != '<') && (value[0] != '>') && (value[0] != '[') &&
		(value[0] != ']') && (value[0] >= '!') && (value[0] <= '~'))
	{
		blank_character = value[0];
	}
	else
	{
		cout << "Illigal blank character.\n";
		valid = false;
	}
	if ((!(definition >> value)) ||
		(To_Capital(value) != "FINAL_STATES:"))
	{
		cout << "Missing keyword after blank character, or more than one character in blank character field.\n";
		valid = false;
		return 7;
	}
	return 6;
}

int Parse::Description_Parse(ifstream &definition, string &description, bool &valid)
{
	unsigned long keyword_start = string::npos;
	unsigned long keyword_Input_start = string::npos;
	unsigned long keyword_Tape_start = string::npos;
	unsigned long keyword_Transition_start = string::npos;
	unsigned long keyword_Initial_start = string::npos;
	unsigned long keyword_Blank_start = string::npos;
	unsigned long keyword_Final_start = string::npos;
	bool description_complete = false;
	description="";
	while (definition.good() && !description_complete && valid)
	{
		string line = "";
		if (getline(definition, line))
		{
			string upper_line = To_Capital(line);
			string final_line = "";

			keyword_start = upper_line.find("STATES:");
			keyword_Input_start = upper_line.find("INPUT_ALPHABET:");
			keyword_Tape_start = upper_line.find("TAPE_ALPHABET:");
			keyword_Transition_start = upper_line.find("TRANSITION_FUNCTION:");
			keyword_Initial_start = upper_line.find("INITIAL_STATE:");
			keyword_Blank_start = upper_line.find("BLANK_CHARACTER:");
			keyword_Final_start = upper_line.find("FINAL_STATES:");

			while (keyword_start != string::npos && !description_complete)
			{
			  
				string desc_substr = line.substr(0, keyword_start);
				string keyword = line.substr(keyword_start, 7);
				string rest_of_line = line.substr(keyword_start + 7, string::npos);

				if ((rest_of_line.length() > 0 && rest_of_line[0] == ' ') || rest_of_line.length() == 0)
				{
					final_line.append(desc_substr);
					description_complete = true;
					for (unsigned long i = rest_of_line.length(); i > 0; --i)
					{
						definition.unget();
					}
					if (final_line.length() > 0)
					{
						description += final_line;
					}
					cout<<description<<endl;
					return 0;
				}
				else
				{
					final_line.append(desc_substr);
					final_line.append(line.substr(keyword_start, 7));
					line = rest_of_line;
					upper_line = To_Capital(rest_of_line);
				}
				keyword_start = upper_line.find("STATES:");

			}
			if (final_line.length() > 0)
			{
				description.append(final_line);
			}
			else
			{
			 
				description += line;
			}
			cout<<description<<endl;
			if (keyword_Input_start != string::npos)
			{
				string rest_of_line = line.substr(keyword_Input_start + 15, string::npos);
				for (unsigned long i = rest_of_line.length(); i > 0; --i)
				{
					definition.unget();
				}
				return 1;
			}
			if (keyword_Tape_start != string::npos)
			{
				string rest_of_line = line.substr(keyword_Tape_start + 14, string::npos);
				for (unsigned long i = rest_of_line.length(); i > 0; --i)
				{
					definition.unget();
				}
				return 2;
			}
			if (keyword_Transition_start != string::npos)
			{
				string rest_of_line = line.substr(keyword_Transition_start + 20, string::npos);
				for (unsigned long i = rest_of_line.length(); i > 0; --i)
				{
					definition.unget();
				}
				return 3;
			}

			if (keyword_Initial_start != string::npos)
			{
				string rest_of_line = line.substr(keyword_Initial_start + 14, string::npos);
				for (unsigned long i = rest_of_line.length(); i > 0; --i)
				{
					definition.unget();
				}
				return 4;
			}
			if (keyword_Blank_start != string::npos)
			{
				string rest_of_line = line.substr(keyword_Blank_start + 16, string::npos);
				for (unsigned long i = rest_of_line.length(); i > 0; --i)
				{
					definition.unget();
				}
				return 5;
			}
			if (keyword_Final_start != string::npos)
			{
				string rest_of_line = line.substr(keyword_Final_start + 13, string::npos);
				for (unsigned long i = rest_of_line.length(); i > 0; --i)
				{
					definition.unget();
				}
				return 6;
			}
		}
		else
		{
			cout << "Error reading description in definition file.\n";
			valid = false;
		}
	}
	if (!description_complete)
		cout << "Error - STATES: keywork not found\n";
	else
	return 0;
}

void Parse::Final_States_Parse(ifstream &definition, vector<string> &final_states, bool &valid)
{
	bool next_keyword = false;
	while (definition.good() && !definition.eof())
	{
		string temp_state;
		if (definition >> temp_state)
		{
			
		 
		    final_states.push_back(temp_state);
		 
			
		}
		
	}
}

int Parse::Initial_State_Parse(ifstream &definition, string &initial_state, bool &valid)
{
	
		string temp_state;
		if (definition >> temp_state)
		{
			if (To_Capital(temp_state) != "BLANK_CHARACTER:")
			{
				
				if (To_Capital(temp_state) == "FINAL_STATES:")
					return 6;
				initial_state = (temp_state);
				
			}
			else
			{
				cout << "Error Reading state from definition file.\n";
				
				valid = false;
				return 5;
			}
		}
		while (definition >> temp_state)
		{
			if (To_Capital(temp_state) != "BLANK_CHARACTER:")
			{

				if (To_Capital(temp_state) == "FINAL_STATES:")
					return 6;
				valid = false;
			}
			else
				return 5;
		}
		return 5;
}

int Parse::Input_Alphabet_Parse(ifstream &definition, vector<char> &input_alphabet, bool &valid)
{
	bool next_keyword = false;
	while (definition.good() && !next_keyword)
	{
		string temp_char;
		if (!(definition >> temp_char))
		{
			valid = false;
			cout << "error reading input alphabet.\n";
		}
		else
		{
			if (To_Capital(temp_char) != "TAPE_ALPHABET:")
			{
				if (To_Capital(temp_char) == "TRANSITION_FUNCTION:")
					return 3;
				if (To_Capital(temp_char) == "INITIAL_STATE:")
					return 4;
				if (To_Capital(temp_char) == "BLANK_CHARACTER:")
					return 5;
				if (To_Capital(temp_char) == "FINAL_STATES:")
					return 6;
				if ((temp_char.length() == 1) && (temp_char[0] != '\\') && (temp_char[0] != '<') && (temp_char[0] != ']'))
				{
					input_alphabet.push_back(temp_char[0]);
				}
				else
				{
					cout << "Illegal Input_Alphabet\n";
					valid = false;
				}
			}
			else
			{
				next_keyword = true;
			}
		}
	}
	return 2;
}

int Parse::States_Parse(ifstream &definition, vector<string> &states, bool &valid)
{
	bool next_keyword = false;
	while (definition.good() && !next_keyword)
	{
		string temp_state;
		if (definition >> temp_state)
		{
			if (To_Capital(temp_state) != "INPUT_ALPHABET:")
			{
				if (To_Capital(temp_state) == "TAPE_ALPHABET:")
					return 2;
				if (To_Capital(temp_state) == "TRANSITION_FUNCTION:")
					return 3;
				if (To_Capital(temp_state) == "INITIAL_STATE:")
					return 4;
				if (To_Capital(temp_state) == "BLANK_CHARACTER:")
					return 5;
				if (To_Capital(temp_state) == "FINAL_STATES:")
					return 6;
				states.push_back(temp_state);
			}
			else
			{
				next_keyword = true;
			}
		}
		else
		{
			cout << "Error Reading states from definition file.\n";
			valid = false;
		}
	}
	return 1;
}

int Parse::Tape_Alphabet_Parse(ifstream &definition, vector<char> &tape_alphabet, bool &valid)
{
	bool next_keyword = false;
	while (definition.good() && !next_keyword)
	{
		string temp_char;
		if (!(definition >> temp_char))
		{
			valid = false;
			cout << "error reading tape alphabet.\n";
		}
		else
		{
			if (To_Capital(temp_char) != "TRANSITION_FUNCTION:")
			{
				if (To_Capital(temp_char) == "INITIAL_STATE:")
					return 4;
				if (To_Capital(temp_char) == "BLANK_CHARACTER:")
					return 5;
				if (To_Capital(temp_char) == "FINAL_STATES:")
					return 6;
				if ((temp_char.length() == 1) && (temp_char[0] != '\\') && (temp_char[0] != '<') && (temp_char[0] != ']'))
				{
					tape_alphabet.push_back(temp_char[0]);
				}
				else
				{
					cout << "Illegal Tape_Alphabet\n";
					valid = false;
				}
			}
			else
			{
				next_keyword = true;
			}
		}
	}
	return 3;
}

int Parse::Transitions_Parse(ifstream &definition, vector<Transition> &transitions, bool &valid)
{
	bool next_keyword = false;
	while (definition.good() && !next_keyword)
	{
		string source, current_char, dest, write, move;
		if (definition >> source)
		{
			if (To_Capital(source) == "INITIAL_STATE:")
			{
				next_keyword = true;
				break;
			}
			else
			{
				if (To_Capital(source) == "BLANK_CHARACTER:")
					return 5;
				if (To_Capital(source) == "FINAL_STATES:")
					return 6;
			}
		}
		else
		{
			valid = false;
			cout << "error reading transition.\n";
		}

		if (!((definition >> current_char) && (definition >> dest) && (definition >> write) && (definition >> move)))
		{
			valid = false;
			cout << "error reading transition.\n";
		
		}
		else
		{
			if ((To_Capital(current_char) != "INITIAL_STATE:") &&
				(To_Capital(dest) != "INITIAL_STATE:") &&
				(To_Capital(write) != "INITIAL_STATE:") &&
				(To_Capital(move) != "INITIAL_STATE:") &&
				(current_char.length() == 1) &&
				(write.length() == 1) &&
				(move.length() == 1) &&
				(To_Capital(move)[0] == 'L' || To_Capital(move)[0] == 'R'))
			{
				if (To_Capital(dest) == "BLANK_CHARACTER:")
					return 5;
				if (To_Capital(dest) == "FINAL_STATES:")
					return 6;
				transitions.push_back(Transition(source, current_char[0], dest, write[0], move[0]));
			}
			else
			{
				if (To_Capital(dest) == "INITIAL_STATE:" || To_Capital(current_char) == "INITIAL_STATE:" ||
					To_Capital(write) == "INITIAL_STATE:" || To_Capital(move) == "INITIAL_STATE:")
					return 4;
				if (To_Capital(dest) == "BLANK_CHARACTER:" || To_Capital(current_char) == "BLANK_CHARACTER:" ||
					To_Capital(write) == "BLANK_CHARACTER:" || To_Capital(move) == "BLANK_CHARACTER:")
					return 5;
				if (To_Capital(dest) == "FINAL_STATES:" || To_Capital(current_char) == "FINAL_STATES:" ||
					To_Capital(write) == "FINAL_STATES:" || To_Capital(move) == "FINAL_STATES:")
					return 6;
				cout << "invalid transition found.\n";
				valid = false;
			}
		}
		
	}
	return 4;
}

bool Parse::Turing_Machine_Parse(ifstream &definition, string &description,
	vector<string> &states, vector<char> &input_alphabet,
	vector<char> &tape_alphabet, vector<Transition> &transitions,
	string &initial_state, char &blank_character,
	vector<string> &final_states, bool& valid)
{
	int check;
	valid = true;
	if (definition.is_open())
	{
		
		check = Description_Parse(definition, description, valid);
		if (!definition.eof() && check == 0)
		{
			check = States_Parse(definition, states, valid);
		}
		else { 
			cout << "Error could not locate STATES: keyword.\n";
		valid = false; }
		if (!definition.eof() && check == 1)
		{
			check = Input_Alphabet_Parse(definition, input_alphabet, valid);
		}
		else { 
			cout << "Error could not locate INPUT_ALPHABET: keyword.\n";
				valid = false; }
		if (!definition.eof() && check == 2)
		{
			check = Tape_Alphabet_Parse(definition, tape_alphabet, valid);
		}
		else { 
			cout << "Error could not locate TAPE_ALPHABET: keyword.\n";
			valid = false;
		}
		if (!definition.eof() && check == 3)
		{
			check = Transitions_Parse(definition, transitions, valid);
		}
		else { 
			cout << "Error could not locate TRANSITION_FUNCTIONS: keyword.\n";
			valid = false;
		}
		if (!definition.eof() && check == 4)
		{
			check = Initial_State_Parse(definition, initial_state, valid);
		}
		else { 
			cout << "Error could not locate INITIAL_STATES: keyword.\n";
			valid = false;
		}
		if (!definition.eof() && check == 5)
		{
			check = Blank_Character_Parse(definition, blank_character, valid);
		}
		else { 
			cout << "Error could not locate BLANK_CHARACTER: keyword.\n";
			valid = false;
		}
		if (!definition.eof() && check == 6)
		{
			Final_States_Parse(definition, final_states, valid);
		}
		else { 
			cout << "Error could not locate FINAL_STATES: keyword.\n";
		valid = false; }
	}
	else
	{
		cout << "Error open file.\n";
		valid = false;
	}
	return valid;
}
char Parse::To_Capital(char &value)
{
	return toupper(value);
}
string Parse::To_Capital(string &value)
{
	string result;
	transform(value.begin(), value.end(), back_inserter(result), ::toupper);
	return result;
}

void Parse::Input_String_Parse(fstream &Input_String_File, vector<string> &Input_String_List)
{
	
	while (!Input_String_File.eof())
	{
		bool add = true;
		string temp;
		getline(Input_String_File, temp);
		if (!Input_String_List.empty())
		for (int i = 0; i < Input_String_List.size(); i++)
		{
			if (Input_String_List[i] == temp)
			{
				add = false;
				break;
			}
				
		}
		if (add)
			Input_String_List.push_back(temp);
	}
}
