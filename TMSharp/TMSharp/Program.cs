using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSharp
{
    class Program
    {
        static void Main(string[] args)
        {

	        // Acquire filename passed as argument
	        string filename = "test";
	        if (args.Count() == 2) {
		        filename = args[1];
	        } else {
//		        Console.WriteLine( "\nYou must pass a single filename argument.\nExample: <application> <filename>\n" );
        //		exit(1);
	        }
	
	        // Setup configuration
	        AppConfiguration config = new AppConfiguration();
	        config.HelpOn = false;	// default (NO messages)
	        config.MaxTransitions = 1; // Max number of transitions to perform at a time (default 1)
	        config.MaxIdChars = 32; // Max number of cells to the left and to the right of the tape head to display in instantaneous description. (default: 32 in each direction)
	        config.FilenameBase = filename;
	
        //	cout << setfill('_') << setw(28) << "" << endl;
        //	cout << "Hello, Turing Machine World!\n";
		
	        // Load Turing Machine Object
	        TuringMachine tm = new TuringMachine(filename);

	        // Validate loaded definition
	        if (!tm.is_valid_definition()) 
            {
		        Console.WriteLine(  "Invalid definition: Shutting down.\n" );
		        return;
	        }
	
	        // Load Menu System
	        Menu menu = new Menu(config, ref tm);
	
	        // Application Loop
	        while (menu.menuLoop()) {
				
	        }
	
            return;            
        }
    }
}
