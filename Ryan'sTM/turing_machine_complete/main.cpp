#include "TuringMachineClass.h"
#include "CommandClass.h"
#include "ConfigurationClass.h"
#include "InputStringClass.h"
#include <string>
#include <iostream>
using namespace std;


int main(int argc,char **argv)
{
	const int seccess(0);
	if(argc!=2)
	  {
	    cout<<"Error usage ["<<argv[0]<<"] <name of definition file>\n\n";
	    return -1;	  
}
	CommandClass command(argv[1]);

	return seccess;
}
