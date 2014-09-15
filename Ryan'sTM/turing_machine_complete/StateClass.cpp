#include "StateClass.h"
#include "string_pointer.h"
#include <string>
using namespace std;

int State::number_of_states = 0;

State::State()
{
	name = new string;
	number_of_states++;
}

State::State(string state_name)
{
	name = new string(state_name);
	number_of_states++;
}

State::State(const State& state)
{
	name = new string(*state.name);	// <-----deep copy
	//name = state.name <------ shallow copy
	number_of_states++;
}

State::~State()
{
	delete name;
	number_of_states--;	
}
State &State::operator=(const State& state)
{
	if (this != &state)
		*name = *state.name;
	return *this;
}
void State::get_name(string& state_name) const
{
	state_name = *name;
}
void State::set_name(string state_name)
{
	*name = state_name;
}
int State::Total_Number_Of_States(){ return number_of_states; }