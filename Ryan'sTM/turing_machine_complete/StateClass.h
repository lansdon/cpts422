#ifndef State_H
#define State_H
#include "string_pointer.h"
#include <string>

using namespace std;
class State
{
private:
	string_pointer name;
	static int number_of_states;
public:
	State();
	State(string State_Name);
	State(const State& state);
	virtual ~State();
	State& operator=(const State& state);
	void get_name(string& state_name) const;
	void set_name(string state_name);
	static int Total_Number_Of_States();
};
typedef State *State_Pointer;
#endif