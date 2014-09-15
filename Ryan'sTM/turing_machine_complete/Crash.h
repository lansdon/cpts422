#ifndef Crash_h
#define Crash_h
#include <stdexcept>
#include <string>
using namespace std;

class Crash : public runtime_error
{
public:
	Crash(string reason);
};
#endif