using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSharp
{
    
    public class AppConfiguration 
    {
	    public uint MaxTransitions { get; set; }// = 1; // Max number of transitions to perform at a time (default 1)
	    public bool HelpOn { get; set; }// = false; // Whether or not help messages are provided to the user for all prompt:   default (NO messages)
	    public uint MaxIdChars { get; set; }// = 32;	// Max number of cells to the left and to the right of the tape head to display in instantaneous
	    public string FilenameBase { get; set; }// = "");
    };


    public class Menu
    {
        private AppConfiguration config;
        private TuringMachine tm;
	    private List<string> input_strings;
	    private bool saveInputStringsOnExit;
        private bool shouldExit = false;

        // Constructor
        public Menu(AppConfiguration cfg, ref TuringMachine turingMachine)
        {
	        config = cfg;
	        tm = turingMachine;
	        saveInputStringsOnExit = false;
	        loadInputStrings(cfg.FilenameBase + ".str");	
        }


        // runs the menu system
        public bool menuLoop() {
	        displayMenu();
	        return processUserCommand();
        }


        public bool processUserCommand() {
        //	Console.WriteLine( setfill('_') + setw(20) + "" + setfill(' ') + endl;
	        Console.WriteLine( "Command:" );
	        string cmd;
	        cmd = Console.ReadLine();
	
	        if(cmd.Length > 1) {
		        Console.WriteLine( "Invalid selection" );	// multiple chars is illegal
		        return processUserCommand();
	        } else if(cmd.Length == 1) {		// Single char selected, try to resolve.
		        switch(cmd.ToUpper()[0]) {
			        case 'D': delete_string(); break;
			        case 'E': set(); break;
			        case 'H': help(); break;
			        case 'I': insert(); break;
			        case 'L': list(); break;
			        case 'Q': quit(); break;
			        case 'R': run(); break;
			        case 'T': truncate(); break;
			        case 'V': view(); break;
			        case 'W': show(); break;
			        case 'X': exit(); break;
			        default:
				        Console.WriteLine( "Invalid selection\n" );		// error
				        return processUserCommand();
		        }
	        } else if (cmd.Length == 0) {		// No chars, display command line again
		        return processUserCommand();
	        }
	        return true;
        }


        bool displayMenu() {
	        if (config.HelpOn) {
		        Console.WriteLine( "\n");
		        Console.WriteLine( "Turing Machine - Menu\n");
		        Console.WriteLine( "(H)elp - Displays details about commands\n");
		        Console.WriteLine( "(D)elete - Delete an input string by number\n");
		        Console.WriteLine( "E(x)it - Exit the program\n");
		        Console.WriteLine( "(I)nsert - Append a new input string to the list of input strings\n");
		        Console.WriteLine( "(L)ist - Display list of input strings\n");
		        Console.WriteLine( "(Q)uit - Stop the Turing Machine operation\n");
		        Console.WriteLine( "(R)un - Run the Turing Machine on the chosen input string. Prompted for a string selection if there isn't an active input string.\n");
		        Console.WriteLine( "S(e)t - Set maximum number of transitions to perform in one pass using run command.\n");
		        Console.WriteLine( "Sho(w) - About this app\n");
		        Console.WriteLine( "(T)runcate - Set MAX number of chars per side in ID\n");
		        Console.WriteLine( "(V)iew - Show the current TM Definition\n");
	        } else {
                /*
		        Console.WriteLine( "\n");
		        Console.WriteLine( "Turing Machine - Menu\n");
		        Console.WriteLine( "(H)elp\n");
		        Console.WriteLine( "(D)elete\n");
		        Console.WriteLine( "E(x)it\n");
		        Console.WriteLine( "(I)nsert\n");
		        Console.WriteLine( "(L)ist\n");
		        Console.WriteLine( "(Q)uit\n");
		        Console.WriteLine( "(R)un\n");
		        Console.WriteLine( "S(e)t\n");
		        Console.WriteLine( "Sho(w)\n");
		        Console.WriteLine( "(T)runcate\n");
		        Console.WriteLine( "(V)iew\n");
                 */
	        }
	        return true;
        }

        bool loadInputStrings(string filename) {
            input_strings = new List<string>();

            char[] CRLF = new char[2] { '\n', '\r' };
            try
            {
                TextReader tr = File.OpenText(filename);
                string line;
                while((line = tr.ReadLine()) != null)
                {
		            if(tm.is_valid_input_string(line)) {
			            input_strings.Add(line);
		            } else {
			            Console.WriteLine( "- Invalid input string ignored (\"" + line + "\")\n" );
		            }
                }
            }
            catch (FileNotFoundException e)
            {
			    Console.WriteLine( "\n- Invalid input string file\n" );
                return false;
            }
	
	        Console.WriteLine( "Input String File loaded successfully...\n");
	        return true;
        }



        /*
         (D)elete
         Delete input string from list (one at a time)
         will cause no gap in list. (remove object from list)
         indicating by number
         errors for invalid input
         on success - output validating message
         ENTER = no error, back to main menu
         */
        void delete_string() {
	        Console.WriteLine( "Delete input string[");
	        Console.WriteLine( input_strings.Count > 0 ?  "1-{0}" + input_strings.Count() : "None available");
	        Console.WriteLine( "]: ");
	        string input_string = Console.ReadLine();
	        uint index = Convert.ToUInt32(input_string);
	        if(input_strings.Count() > 0 && index > 0 && input_string.Length == index.ToString().Length && index <= input_strings.Count() ) {
		        input_strings.RemoveAt((int)index-1);
		        saveInputStringsOnExit = true;
		        Console.WriteLine( "\nInput string deleted. (save file queued)\n");
	        } else {
		        if(input_string.Length > 0)
			        Console.WriteLine( "\nError - Invalid selection. \n");
	        }
        }


        /*
         S(e)t
         Set maximum number of transitions to perform
         ex: "Maximum number of transitions[1]: "
         should display current value as part of prompt
         carriage return = NO MSG. back to menu prompt
         must be >= 1,    0 = error!
        */
        bool set() {
	        Console.WriteLine( "Maximum number of transitions[{0}]:", config.MaxTransitions);
	        string input_string = Console.ReadLine();
	        uint new_value = Convert.ToUInt32(input_string);
	        if(input_string.Length == new_value.ToString().Length && new_value > 0) {
		        config.MaxTransitions = new_value;
		        Console.WriteLine( "\nMax transitions changed.\n");
		        return true;
	        }
	        if(input_string.Length > 0)
		        Console.WriteLine( "\nError - Max transitions must be a integer from 1 to " + uint.MaxValue + "\n");

	        return false;
        }


        /*
         (H)elp
         Help User with prompts or not
         change setting for whether or not help messages are 
         provided to user for all prompts input
         defaults to NO.    Toggles yes or no
         */
        void help() {
	        config.HelpOn = !config.HelpOn;
        }


        /*
         (I)nsert
         Insert input string into the list from Sigma* and 
         append it to list of input strings
         Prompt for input string    "Input string: "
         if string is duplicate of existing string or contains 
         an illegal character -> display appropriate error message
        */
        void insert() 
        {
	        Console.WriteLine( "Input string:");
	        string input_string = Console.ReadLine();
	
	        // Search for duplicate strings
		    if(input_strings.Contains( input_string ) )
            {
			    Console.WriteLine( "That string already exists in the list.\n");
			    return;
		    }
	
	        // Validate string and add it.
	        if(tm.is_valid_input_string(input_string)) {
		        input_strings.Add(input_string);
		        saveInputStringsOnExit = true;
		        Console.WriteLine( "\nInput string saved. (save file queued)\n");
	        } else {
		        Console.WriteLine( "\nError - Invalid input string.\n");
	        }
        }


        /*
         (L)ist
         List input strings
         each string on its own line
         each string numbered sequentially starting with 1
         Empty list displays message
         Empty string shown as backslash
         No error if no string is entered.. go back to prompt
         \  is used to input blank string
        */
        void list() {
	        Console.WriteLine( "\nInput Strings:\n");
	
	        // empty list message
	        if(input_strings.Count() == 0) {
		        Console.WriteLine( "<No input strings loaded>\n");
		        return;
	        }
	
	        // Display strings
	        for(int i=0; i<input_strings.Count(); ++i) {
		        if(input_strings[i].Count() > 0)
                {
			        Console.WriteLine( i+1 + ") " + input_strings[i] );
		        } else {
			        Console.WriteLine( i+1 + ") \\" );
		        }
	        }
	        Console.WriteLine( "" );
        }


        /*
         (Q)uit
         Quit operation of ™ on input string before completion
         "Input string AABBAA not accepted or rejected in 200 transitions"
         Error if no string is selected
        */
        void quit() {
	        if(tm.input_string().Length > 0) {
		        tm.terminate_operation();
	        } else {
		        // error no string
		        Console.WriteLine( "Error - No input string selected.\n");
	        }
        }


        /*
         ( R )un
         Run ™ on input string
         prompts for input string if it's not running (including done w/ old string)
         if running, perform next set of transitions
         example:
         command: R
         Input string #: 5
         0 [S0]AABBABBAAA>      (INITIAL ID IS DISPLAYED)
         100. <BXAABYAAAB[s3]XYYABBAABYY>
         command R
         200. ABYX[S2]ABBB
         command: R
         225 ABYX[s5]AX
         Input string   AAABBBBABB accepted in 225 transitions
         command: 
        */
        void run() {
	
	        // Select Input String
	        if(!tm.is_operating()) {
		        Console.WriteLine( "Input String:[");
		        Console.WriteLine( input_strings.Count() > 0 ? "1-" + input_strings.Count() : "None available");
		        Console.WriteLine( "]: ");
		        string input_string = Console.ReadLine();
		        uint index = Convert.ToUInt32(input_string);
		        if(input_strings.Count() > 0 && index > 0 && input_string.Length == index.ToString().Length && index <= input_strings.Count() ) {
			        tm.initialize(input_strings[(int)index-1]);			
			        tm.view_instantaneous_description(config.MaxIdChars);
		        } else {
			        if(input_string.Length > 0)
				        Console.WriteLine( "\nError - Invalid selection. \n");
			        return;
		        }
	        }	
	        tm.perform_transitions(config.MaxTransitions);
	        tm.view_instantaneous_description(config.MaxIdChars);
        }


        /*
         (T)runcate
         Truncate instantaneous descriptions
         command allows user to change setting for max 
         number of chars to left and right of tape head to 
         display in ID during operation of ™ input string
         same rules as SET  (positive ints > 0, same errors)
         */
        bool truncate() {
	        Console.WriteLine( "Maximum ID chars per side[" + config.MaxIdChars + "]: ");
	        string input_string = Console.ReadLine();;
	        uint new_value = Convert.ToUInt32(input_strings);
	        if(input_string.Length == new_value.ToString().Length && new_value > 0) {
		        config.MaxIdChars = new_value;
		        Console.WriteLine( "\nMax ID chars changed.\n");
		        return true;
	        }
	        if(input_string.Length > 0)
		        Console.WriteLine( "\nError - Max ID chars must be a integer from 1 to " + uint.MaxValue + "\n");
	        return false;
        }



        /*
         (V)iew
         View turfing machine (states, transition function, etc)
         Definition of currently loaded machine in a form readable 
         to user (use curly braces, commas, etc etc) but don't 
         display .def file contents.
         Description of ™ from .def file (without alteration)
         Formal specification of ™ M=(Q, sigma, gamma, sigma, q_0, B, F)
         example transition: DELTA(s0, A) = (s1, B, R)
         Don't use keywords from .def file
         use notation from formal specification as much as 
         possible, including equal signs, parenthesis, comma, curly brackets, etc
         See turing machine specification paper handout
        */
        void view() {
	        tm.view_definition();
        }

        /*
         Sho(w)
         Show status of application
         Course, Semester,
         Year, Instructor,
         Author,
         version of application,
         configuration settings(3),
         name of ™ (without .def = the argument passed in),
         status of ™ (3),
         if ™ is currently running, display input string and total number of transitions,
         if ™ has completed operation on input string, display last input string used
         and if completed whether it was accepted, rejected, or terminated by user
         if completed total number of transitions performed.
         status information is not retained when we exit application
         NO ID's (instantaneous Description)
         no true or false… must be "yes or no"
         */
        void show() {
	        Console.WriteLine( "\n*********** About This App *************\n");
	        Console.WriteLine( "Title:" + "Turing Machine\n");
	        Console.WriteLine( "Author:" + "Lansdon Page\n");
	        Console.WriteLine( "Course:" + "CPTS_322\n");
	        Console.WriteLine( "Semester:" + "Spring 2013\n");
	        Console.WriteLine( "Instructor:" + "Neil Corrigan\n");
	        Console.WriteLine( "Version:" + "1.0\n");
	        Console.WriteLine( "Settings:" + "\n");
	        Console.WriteLine( "-- Max Transitions:" + config.MaxTransitions + "\n");
	        Console.WriteLine( "-- Max ID Chars:" + config.MaxIdChars + "\n");
	        Console.WriteLine( "-- Help:" + (config.HelpOn ? "ON" : "OFF") + "\n");
	        Console.WriteLine( "Current File:" + config.FilenameBase + "\n" );
	        Console.WriteLine( "TM -- Running:" +
	        (tm.is_operating() ? "YES" : "NO") + "\n");
	        if(tm.is_operating()) {
		        Console.WriteLine( "-- Input String:" + tm.input_string() + "\n");
		        Console.WriteLine( "-- Total Transitions:" + tm.total_number_of_transitions() + "\n");
	        }
	        Console.WriteLine( "TM -- Used:" +
	        (tm.is_used() ? "YES" : "NO") + "\n");
	        if(tm.is_used()) {
		        if(tm.input_string().Length > 0) {
			        Console.WriteLine( "-- Last Input String:" + tm.input_string() + "\n");
		        } else {
			        Console.WriteLine( "-- Last Input String: \\" + "\n");
		        }
		        Console.WriteLine( "-- Result:");
		        if(tm.is_accepted_input_string()) {
			        Console.WriteLine( "Accepted\n" +
			        "Total Transitions:" + tm.total_number_of_transitions() + "\n" );
		        }
		        else if(tm.is_rejected_input_string()) Console.WriteLine( "Rejected\n");
		        else Console.WriteLine( "Terminated\n");
	        }

        }


        /*
         E(x)it
         Exit application
         No confirmation
         replace input string file if changes occurred using insert/delete.
         If writing file fails, print error message and quit
         on successfully writing file display success message
         */
        void exit() {
	        if(saveInputStringsOnExit) {
		        // Save input string file .str
		        string def = config.FilenameBase + ".str";
                try
                {
		            using(StreamWriter string_file = new StreamWriter(def))
                    {
		                for(int i=0; i<input_strings.Count(); ++i) {
			                string_file.WriteLine( input_strings[i] );
		                }
		                Console.WriteLine( config.FilenameBase + ".str updated successfully.\n");
                    }
                } 
                catch (FileNotFoundException e)
                {
			        Console.WriteLine( "Error opening input string file...\n");
			       shouldExit = true;
                }		
	        }
	        shouldExit = true;
        }

    }
}
