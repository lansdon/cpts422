using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSharp
{
    public class TuringMachine
    {
        #region properties
        // Components
        private Tape tape;
        private FinalStates final_states;
        private InputAlphabet input_alphabet;
        private States states;
        private TapeAlphabet tape_alphabet;
        private TransitionFunction transition_function;
	
	    // TM Variables
	    private List<string> description;       // a vector of strings (lines) representing the description of the Turing Machine Definition
        private string initial_state;           //	the initial state of the current definition
        private string current_state;           //	The current state of the TM
        private string original_input_string;   //	The original input string. A reference is kept in case the input string is accepted and the original string needs to be displayed.
        private int number_of_transitions;      //	The number of transitions that have been performed.
        private bool valid;		                //	The current validity of the TM based on transitions performed.
        private bool used;		                //	Determines if TM has finished on a string
        private bool operating;                 //	True when TM is running on a string
        private bool accepted;                  //	True if the input string was accepted
        private bool rejected;                  //	True if the input string was rejected

        #endregion

        

        ///////////////////////////////////
        // Constructor. Takes a string argument which is the general name of
        // the definition Turing Machine Definition without any file extensions.
        // Both the definition file and the input string file must have this same
        // exact name with .def and .str extensions respectively.
        ///////////////////////////////////
        public TuringMachine(string definitionfilename)
        {
            initial_state = "";
	        current_state = "";
	        original_input_string = "";
	        number_of_transitions = 0;
	        valid = false;
	        used = false;
	        operating = false;
	        accepted = false;
	        rejected = false;

            tape = new Tape();
            final_states = new FinalStates();
            input_alphabet = new InputAlphabet();
            states = new States();
            tape_alphabet = new TapeAlphabet();
            transition_function = new TransitionFunction();

	        if(loadDefinition(definitionfilename + ".def")) 
            {
		        // Additional Setup
	        } 
            else 
            {
//		        Exit(1);
	        }
        }

        public bool is_operating()
        {
            return operating;
        }

        public bool is_used()
        {
            return used;
        }

        public bool is_accepted_input_string()
        {
            return accepted;
        }

        public bool is_rejected_input_string()
        {
            return rejected;
        }

        ///////////////////////////////////////////
        // Utility function for extracting description
        // - Maintains whitespace
        /////////////////////////////////////////////
        public bool parseDescription(ref List<string> definitionLines)
        {
            description = new List<string>();
            int keyword_start = -1;
	        bool description_complete = false;
	        bool valid = true;
	
            string line = null;
            while (!description_complete && valid)
            {
                try
                {
                     line = definitionLines.ElementAt(0);
                } 
                catch
                {
                    Console.WriteLine("Error reading description in definition file.\n");
                    valid = false;
                    break;
                }

			    string upper_line = line.ToUpper();
                keyword_start = upper_line.IndexOf("STATES:");
                string desc_substr = line;
                string keyword = "";
                string rest_of_line = "";
                if (keyword_start >= 0)
                {
                    desc_substr = line.Substring(0, keyword_start);
                    keyword = line.Substring(keyword_start, 7);
                    if(line.Length > desc_substr.Length + keyword.Length)
                        rest_of_line = line.Substring(keyword_start + 7, line.Length - (keyword_start + 7));
                }			

			    // search for first VALID keyword.
			    if( (keyword_start > 0 && line[keyword_start-1] == ' ')  ||       // keyword found in middle of string with a leading space
                    (keyword_start == 0 && (line.Length < keyword_start+7 || line[keyword_start + 7] == ' ') ) )        // Keyword at beginning with trailing space
                {
					description_complete = true;		
					description.Add(desc_substr);					
                    // replace the current line in the list with only the text after the keyword we found.
                    if (rest_of_line.Length > 0)
                        definitionLines[0] = rest_of_line;
                    else definitionLines.RemoveAt(0);
                }
			    else 
                {
                    description.Add(line);
                    definitionLines.RemoveAt(0);    // Remove the entire line
                }	
		    }

            if (!description_complete) Console.WriteLine("Error - STATES: keyword not found\n");

            return (valid && description_complete);
        }

        public bool LoadInitialState(ref List<string> definitionLines)
        {
            List<string> results;
            if(TMParse.ParseDefinitionValues(ref definitionLines, "BLANK_CHARACTER:", out results))
            {
                if (states.is_element(results[0]))
                {
                    initial_state = results[0];
                    return true;
                }
            }
            Console.WriteLine("Error reading initial state.\n");
            return false;
        }

        private bool LoadBlankChar(ref List<string> definitionLines)
        {
            List<string> results;
            if (TMParse.ParseDefinitionValues(ref definitionLines, "FINAL_STATES:", out results))
            {
                return tape.load(ref definitionLines);
            }
            Console.WriteLine("Error reading initial state.\n");
            return false;
        }

        /////////////////////////////////////////////////
        //o	Loads definition file and calls load for every component.
        /////////////////////////////////////////////////
        public bool loadDefinition(string filename)
        {
            if (filename == null || filename.Length <= 4)
                return false;

            bool valid = true;

            List<string> definitionLines = new List<string>();
            try
            {
                char[] CRLF = new char[2] { '\n', '\r' };
                TextReader tr = File.OpenText(filename);
                definitionLines = new List<string>(tr.ReadToEnd().Split(CRLF));
            }
            catch (Exception e)
            {
                Console.WriteLine("TM Definition failed to load.\n");
                valid = false;
            }

            // Load definition .def file
            if (
                (definitionLines.Count() > 0) && 
                parseDescription(ref definitionLines) &&
                states.load(ref definitionLines) &&
                input_alphabet.load(ref definitionLines) && 
                tape_alphabet.load(ref definitionLines) &&
                transition_function.load(ref definitionLines) &&
                LoadInitialState(ref definitionLines) &&
                tape.load(ref definitionLines) && 
                final_states.load(ref definitionLines) )

            {
                Console.WriteLine("TM Definition loaded successfully...\n");
                valid = true;
            } 
            else
            {
                Console.WriteLine("TM Definition failed to load.\n");
                valid = false;
            }
            return valid;
        }

        ///////////////////////////////////
        // displays the TM definition in the console
        ///////////////////////////////////
        public void view_definition()
        {
            Console.WriteLine( "\n*********** TM Definition: **************");
            Console.WriteLine( "Description:" );
            foreach(string line in description) {
                Console.WriteLine( line );
            }
            Console.WriteLine( "*****************************************");
            states.view();
            input_alphabet.view();
            tape_alphabet.view();
            transition_function.view();
            Console.WriteLine( "Initial State = " + initial_state );
            Console.WriteLine( "Blank Symbol = " + tape.BlankCharacter );
            final_states.view();
            Console.WriteLine( "*****************************************");
            Console.WriteLine("\n\n");
        }

        ///////////////////////////////////
        // displays the id in the console formatted with
        // the cells left of the tape head followed by the current state and
        // lastly the current cell and cells to the right of the tape
        // head.
        ///////////////////////////////////
        public void view_instantaneous_description(uint maximum_number_of_cells) 
        {
            Console.WriteLine( number_of_transitions + " " +
                    tape.left(maximum_number_of_cells) +
                    "[" + current_state + "]" +
                    tape.right(maximum_number_of_cells));
        }

        ///////////////////////////////////
        // Initializes TM with the passed input string
        ///////////////////////////////////
        public void initialize(string input_string)
        {
            if(is_valid_input_string(input_string)) {
                tape.initialize(input_string);
                original_input_string = input_string;
                number_of_transitions = 0;
                current_state = initial_state;
                valid = true;
                used = false;
                operating = true;
                accepted = false;
                rejected = false;		
            } else {
                Console.WriteLine( "\nError - TM Initializated with invalid input string.\n");
            }
        }


        ///////////////////////////////////
        // This is the main operation of the TM.
        // The TM will perform transitions on the current TM definition, up to
        // the max_number_of_transitions. During this operation, the the input
        // string may be rejected if an error occurs, may be accepted if a final
        // state is reached, or simply finish the max_number_of_transitions and
        // stop.
        ///////////////////////////////////
        public void perform_transitions(uint maximum_number_of_transitions) 
        {
            // Primary Loop - Max Iterations or Crash
            for(int i=0; i<maximum_number_of_transitions; ++i) 
            {
        //		Console.WriteLine( "perform transition:" << i << endl;
                // Search for transition
                if(transition_function.is_source_state(current_state)) 
                {
                    for(uint transition=0; transition < transition_function.size(); ++transition) 
                    {
                        if((transition_function.read_character(transition) == tape.CurrentChar) &&
                           (transition_function.source_state(transition) == current_state)) 
                        {
                            // Found a transition!
                            tape.update(transition_function.write_character(transition), transition_function.move_direction(transition));
                            current_state = transition_function.destination_state(transition);
                            ++number_of_transitions;
                            break;
                        } 
                        else if (transition == transition_function.size()-1) 
                        {
                            // No transition found !!
                            // error no transition found
                            rejected = true;
                            accepted = false;
                            operating = false;
                            used = true;
                        }
                    }
                } 
                else 
                {
                    // error no transition found
                    rejected = true;
                    accepted = false;
                    operating = false;
                    used = true;
                    break;
                }                                                                                                                                                                                                           

                if(final_states.is_element(current_state)) 
                {
                    // WIN!
                    Console.WriteLine( "\nSuccess - Input string accepted.\n");
                    rejected = false;
                    accepted = true;
                    operating = false;
                    used = true;
                    break;
                } 
                else if (!operating) 
                {
                    Console.WriteLine( "\nCRASH - No transition found.\n");
                    break;
                }
            }
        }

        ///////////////////////////////////
        // The user can opt to terminate operation without reaching conclusive
        // condition.
        ///////////////////////////////////
        public void terminate_operation()
        {
            Console.WriteLine( "\nInput string " + original_input_string + " not accepted or rejected in " + number_of_transitions + " transitions\n");
            operating = false;
            accepted = false;
            rejected = false;
            used = true;
        }

        ///////////////////////////////////
        // Returns the current input string
        ///////////////////////////////////
        public string input_string() 
        {
            return original_input_string;
        }

        ///////////////////////////////////
        // Returns the current number of transitions that have been performed.
        ///////////////////////////////////
        public int total_number_of_transitions() 
        {
            return number_of_transitions;
        }

        ///////////////////////////////////
        // Returns true if the input_string is valid.
        ///////////////////////////////////
        public bool is_valid_input_string(string value) 
        {
            foreach(char c in value)
            {
                if(!input_alphabet.is_element(c))
                    return false;
            }
            return true;
        }

        ///////////////////////////////////
        // Run a validation algorithm on loaded TM
        ///////////////////////////////////
        public bool is_valid_definition() 
        {
            // input alphabet is subset of tape alphabet
            for(uint i=0; i<input_alphabet.size(); ++i) {
                if(!tape_alphabet.is_element( input_alphabet.element(i))) {
                    Console.WriteLine( "Invalid definition - Input alphabet is not a subset of tape alphabet.\n");
                    return false;
                }
            }
	
            // blank char is in tape alphabet and not in input alphabet
            if(!(tape_alphabet.is_element(tape.BlankCharacter) && !input_alphabet.is_element(tape.BlankCharacter))) {
                Console.WriteLine( "Invalid definition - Blank char cannot be part of input alphabet and must be part of the tape alphabet.\n");
                return false;
            }
	
            // No transition out of final states
            for(uint i=0; i<final_states.size; ++i) {
                if(transition_function.is_source_state(final_states.element(i))) {
                    Console.WriteLine( "Invalid definition - Final states cannot be source states for transitions.\n");
                    return false;
                }
                // Final states - must be subset of states
                if(!states.is_element(final_states.element(i))) {
                    Console.WriteLine( "Invalid definition - Final states must be subset of States\n");
                    return false;
                }
            }
	
            // Transition states must exist in states
            for(uint i=0; i < transition_function.size();++i) {
                if(!(states.is_element(transition_function.source_state(i)) && states.is_element(transition_function.destination_state(i)))) {
                    Console.WriteLine( "Invalid definition - Transition function source and destination states must exist in States\n");
                    return false;
                }
                // validate transition read/write chars as subset of tape alphabet
                if(!(tape_alphabet.is_element(transition_function.read_character(i))) &&
                   (tape_alphabet.is_element(transition_function.write_character(i)))) {
                    Console.WriteLine( "Invalid definition - Transition function source and destination states must exist in States\n");
                    return false;
                }
            }
	
            // initial state is in states
            if(!states.is_element(initial_state)) {
                Console.WriteLine( "Invalid definition - Initial state must exist in States\n");
                return false;
            }
	
            // Blank char not in input alphabet
            if(input_alphabet.is_element(tape.BlankCharacter)) {
                Console.WriteLine( "Invalid definition - Blank char cannot be in input alphabet\n");
                return false;
            }
	
            return true;	// Passed all the tests!
        }

    }
}
