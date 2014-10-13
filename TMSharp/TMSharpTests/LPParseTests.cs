using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPTest;
using TMSharp;

namespace TMSharpTests
{
    [LPTestClass]
    class LPParseTests
    {
        public LPParseTests() { }


        /// <summary>
        /// 5.2.10 - ParseDefinition_TransFunct_Valid
        /// </summary>
        [LPTestMethod]
        public void ParseDefinition_TransFunct_Valid()
        {
            List<string> definition = new List<string>() 
            {
                "s3 -   s3 - R",
                "INITIAL_STATE: s0",
            };
            string iState = "s3";
            char ch = '-';
            char dir = 'R';
            TransitionFunction tf = new TransitionFunction();
            Assert.IsTrue(tf.load(ref definition), "Failed to parse transition function.");
            Assert.IsTrue(tf.is_defined_transition(iState, ch, ref iState, ref ch, ref dir), "S1 not found");
        }

        /// <summary>
        /// 5.2.11 - ParseDefinition_TransFunct_InvalidFieldCount
        /// </summary>
        [LPTestMethod]
        public void ParseDefinition_TransFunct_InvalidFieldCount()
        {
            List<string> definition = new List<string>() 
            {
                "s3 -   s3",
                "INITIAL_STATE: s0",
            };
            TransitionFunction tf = new TransitionFunction();
            Assert.IsFalse(tf.load(ref definition), "Failed to parse transition function.");
        }

        /// <summary>
        /// 5.2.12 - ParseDefinition_TransFunct_InvalidChar
        /// </summary>
        [LPTestMethod]
        public void ParseDefinition_TransFunct_InvalidChar()
        {
            List<string> definition = new List<string>() 
            {
                "description stuff1",
                "description stuff2",
                "STATES: S1 S2 S3 S4",
                "INPUT_ALPHABET: a b",
                "TAPE_ALPHABET: a b x y -",
                "TRANSITION_FUNCTION:",
                "s0 a   s1 X R",
                "s0 Y   s3 Y R",
                "s1 a   s1 a R",
                "s1 b   s2 Y L",
                "s1 Y   s1 Y R", 
                "s2 a   s2 a L",
                "s2 X   s0 X R",
                "s2 Y   s2 Y L",
                "s3 Y   s3 Y R",
                "s3 -   s4 - R",
                "INITIAL_STATE: s0",
                "BLANK_CHARACTER: -",
                "FINAL_STATES: s4"
            };
            Tape tape = new Tape();
            FinalStates final_states = new FinalStates();
            InputAlphabet input_alphabet = new InputAlphabet();
            States states = new States();
            TapeAlphabet tape_alphabet = new TapeAlphabet();
            TransitionFunction transition_function = new TransitionFunction();
            TuringMachine tm = new TuringMachine("");

            Assert.IsTrue(tm.parseDescription(ref definition), "Failed to parse description");
            Assert.IsTrue(states.load(ref definition), "Failed to parse states.");
            Assert.IsTrue(input_alphabet.load(ref definition), "Failed to parse input alphabet");
            Assert.IsTrue(tape_alphabet.load(ref definition), "Failed to parse tape alphabet");
            Assert.IsFalse(transition_function.load(ref definition), "Should not accept transition with a state not in STATES:");

        }

        /// <summary>
        /// 5.2.13 - ParseDefinition_InitState_TooManyStates
        /// </summary>
        [LPTestMethod]
        public void ParseDefinition_InitState_TooManyStates()
        {
            List<string> definition = new List<string>() 
            {
                "s0 a   s1 X R S1",
                "INITIAL_STATE: s0",
            };
            TransitionFunction tf = new TransitionFunction();
            Assert.IsFalse(tf.load(ref definition), "Should not accept transition with too many fields.");
        }

        /// <summary>
        /// 5.2.14 - ParseDefinition_BlankChar_NotInAlphabet
        /// </summary>
        [LPTestMethod]
        public void ParseDefinition_BlankChar_NotInAlphabet()
        {
            List<string> definition = new List<string>() 
            {
                "a b x y -",
                "TRANSITION_FUNCTION:",
                "s0 a   s1 X R",
                "s0 Y   s3 Y R",
                "s1 a   s1 a R",
                "s1 b   s2 Y L",
                "s1 Y   s1 Y R", 
                "s2 a   s2 a L",
                "s2 X   s0 X R",
                "s2 Y   s2 Y L",
                "s3 Y   s3 Y R",
                "s3 -   s4 - R",
                "INITIAL_STATE: s0",
                "BLANK_CHARACTER: +",
                "FINAL_STATES: s4"
            };
            Tape tape = new Tape();
            FinalStates final_states = new FinalStates();
            InputAlphabet input_alphabet = new InputAlphabet();
            States states = new States();
            TapeAlphabet tape_alphabet = new TapeAlphabet();
            TransitionFunction transition_function = new TransitionFunction();
            TuringMachine tm = new TuringMachine("");

            Assert.IsTrue(tape_alphabet.load(ref definition), "Failed to parse tape alphabet");
            Assert.IsTrue(transition_function.load(ref definition), "Failed to parse transition function.");
            Assert.IsTrue(tm.LoadInitialState(ref definition), "Failed to parse initial state.");
            Assert.IsFalse(tape.load(ref definition), "Should not load a blank char that's not in tape alphabet.");
        }

        /// <summary>
        /// 5.2.15 - ParseDefinition_FinalStates_NoStates
        /// </summary>
        [LPTestMethod]
        public void ParseDefinition_FinalStates_NoStates()
        {
            List<string> definition = new List<string>() 
            {
                " "
            };
            FinalStates fStates = new FinalStates();
            Assert.IsFalse(fStates.load(ref definition), "Should fail with no final states.");
        }

        /// <summary>
        /// 5.2.16 - ParseDefinition_FinalStates_NotInStates
        /// </summary>
        [LPTestMethod]
        public void ParseDefinition_FinalStates_NotInStates()
        {
            List<string> definition = new List<string>() 
            {
                 "description stuff1",
                "description stuff2",
                "STATES: S1 S2 S3 S4",
                "INPUT_ALPHABET: a b",
                "TAPE_ALPHABET: a b x y -",
                "TRANSITION_FUNCTION:",
                "s0 a   s1 X R",
                "s0 Y   s3 Y R",
                "s1 a   s1 a R",
                "s1 b   s2 Y L",
                "s1 Y   s1 Y R", 
                "s2 a   s2 a L",
                "s2 X   s0 X R",
                "s2 Y   s2 Y L",
                "s3 Y   s3 Y R",
                "s3 -   s4 - R",
                "INITIAL_STATE: s0",
                "BLANK_CHARACTER: -",
                "FINAL_STATES: BOGUS"
            };
            Tape tape = new Tape();
            FinalStates final_states = new FinalStates();
            InputAlphabet input_alphabet = new InputAlphabet();
            States states = new States();
            TapeAlphabet tape_alphabet = new TapeAlphabet();
            TransitionFunction transition_function = new TransitionFunction();
            TuringMachine tm = new TuringMachine("");

            Assert.IsTrue(tm.parseDescription(ref definition), "Failed to parse description");
            Assert.IsTrue(states.load(ref definition), "Failed to parse states.");
            Assert.IsTrue(input_alphabet.load(ref definition), "Failed to parse input alphabet");
            Assert.IsTrue(tape_alphabet.load(ref definition), "Failed to parse tape alphabet");
            Assert.IsTrue(transition_function.load(ref definition), "Failed to parse transition function.");
            Assert.IsTrue(tm.LoadInitialState(ref definition), "Failed to parse initial state.");
            Assert.IsTrue(tape.load(ref definition), "Failed to load tape.");
            Assert.IsFalse(final_states.load(ref definition), "Should fail to parse final state that is not in states.");
        }
    }
}
